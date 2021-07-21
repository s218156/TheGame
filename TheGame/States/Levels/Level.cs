using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Content.Items;
using TheGame.Inventory;
using TheGame.Items;
using TheGame.Mics;
using TheGame.Mics.GUI_components;
using TheGame.SoundControllers;
using TheGame.Sprites;
using TheGame.States.Levels;
using TheGame.States.Menu;

namespace TheGame.States
{
    public abstract class Level : State
    {
        public int levelId;
        protected int nextLevelId;
        protected TileMap map;
        public Player player;
        public List<Sprite> sprites;
        protected Camera _camera;
        protected List<CheckPoint> _checkpoints;
        protected GhostSprite ghostSprite;
        protected List<Paralax> _paralaxes, _startParalaxes;
        public List<Item> items;
        public Vector2 spawnPoint;
        protected GameUI gameUI;
        protected Rectangle EndPoint;
        public List<MovableItem> movableItems;
        protected List<FallableObject> fallableObjects;
        protected int pointAtTheBegining;
        public GameMaster gameMaster;
        protected List<string>messageList;
        protected List<Spring> springs;
        private State nextGameState;
        Random random;
        private List<WaterArea> waterAreas;

        private Texture2D mask;
        protected Effect effect;
        RenderTarget2D lightMask;
        RenderTarget2D gameFrame;
        protected bool isLightShader;
        public Level(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session,int levelId, int nextLevelId):base(game,graphics,content, session)
        {
            this.levelId = levelId;
            this.nextLevelId = nextLevelId;
            random = new Random();
            isLightShader = false;
            pointAtTheBegining = session.GetPlayerPoints();
            _paralaxes = new List<Paralax>();
        }

        public void GeneratePlayerAndBackground()
        {
            player = new Player(content.Load<Texture2D>("Sprites/playerAnimation"), spawnPoint, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), session.GetPlayerLives());

            foreach (Paralax tmp in _paralaxes)
                tmp.Initialize();

            ghostSprite = new GhostSprite(player);
            sprites.Insert(0, player);
            sprites.Add(ghostSprite);
        }

        public override void Initialize()
        {
            gameFrame = new RenderTarget2D(graphics, graphics.Viewport.Width, graphics.Viewport.Height);
            if (isLightShader)
            {
                lightMask = new RenderTarget2D(graphics, graphics.Viewport.Width, graphics.Viewport.Height);
                mask = content.Load<Texture2D>("shaders/lightmask");
            }
                

            session.SetPlayerPoints(pointAtTheBegining);
            _startParalaxes = new List<Paralax>(_paralaxes);
            _camera = new Camera();
            sprites = new List<Sprite>();
            items = new List<Item>();
            _checkpoints = new List<CheckPoint>();
            movableItems = new List<MovableItem>();
            gameUI = new GameUI(content);
            fallableObjects = new List<FallableObject>();
            springs = new List<Spring>();
            waterAreas = new List<WaterArea>();

            GenerateObjects();
        }

        protected abstract void LoadMap();

        private void GenerateObjects()
        {
            CoinSoundController coinSound = new CoinSoundController(content.Load<Song>("Audio/handleCoins"));
            spawnPoint = map.spawnPosition;

            GeneratePlayerAndBackground();
            
            foreach (Rectangle obj in map.movableObjects)
                movableItems.Add(new MovableItem(content.Load<Texture2D>("Items/chest"), obj));
            
            foreach (Rectangle obj in map.fallableObjects)
            {
                FallableObject temp = new FallableObject(content.Load<Texture2D>("Items/bridge"), obj);
                fallableObjects.Add(temp);
                map.mapObjects.Add(temp.rectangle);
            }

            foreach (Rectangle tmp in map.springs)
                springs.Add(new Spring(content.Load<Texture2D>("Items/spring"), tmp));

            foreach (Rectangle tmp in map.waterArea)
                waterAreas.Add(new WaterArea(content.Load<Texture2D>("TileMaps/textures/water_texture"), tmp));

            foreach (var tmp in map.checkPoints)
            {
                var newItem = new CheckPoint(tmp, content.Load<Texture2D>("Items/checkpoint"), content.Load<Texture2D>("Items/checkpointanim"));
                _checkpoints.Add(newItem);
                items.Add(newItem);
            }

            foreach (var tmp in map.levers)
            {
                List<Platform> platforms = new List<Platform>();
                foreach(PlatformInstanceForMap platform in map.platforms)
                {
                    if (platform.leverNumber == tmp.leverNumber)
                    {
                        platforms.Add(new Platform(content.Load<Texture2D>("items/platform"),platform.rectangle));
                    }
                }

                items.Add(new Lever(content.Load<Texture2D>("items/lever"), tmp.rectangle, map,platforms));
            }
                
                
            foreach (var tmp in map.GetCoins())
                items.Add(new Coin(content.Load<Texture2D>("Items/coinAnimation"), new Rectangle((int)tmp.X, (int)tmp.Y, 50, 50), 1, coinSound));
            
            foreach (var tmp in map.GetLadders())
                items.Add(new Ladder(tmp));

            foreach (var tmp in map.tourches)
                items.Add(new Tourch(content.Load<Texture2D>("items/tourch"),tmp));

            foreach (var tmp in map.snails)
                sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/snailAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 100, 1));
            
            foreach (var tmp in map.mouse)
                sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/mouseAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 300,3));
            
            foreach (var tmp in map.worms)
                sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/greenWormAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 150,2));

            foreach (var tmp in map.flyingBugs)
                sprites.Add(new Fly(content.Load<Texture2D>("Sprites/flyAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 400, 2));

            EndPoint = map.endPosition;
            foreach (var tmp in map.powerups)
            {
                if (tmp.Height == 1)
                {
                    InventoryItem tmpItem = new InventoryItem(tmp.Height, 20, content.Load<Texture2D>("jetpack"), false);
                    items.Add(new PickableItem(content.Load<Texture2D>("jetpack"), new Rectangle(tmp.X, tmp.Y, tmp.Width, tmp.Width), tmpItem));
                }
            }
            gameMaster = new GameMaster(content, new Vector2(graphics.Viewport.Width,graphics.Viewport.Height), map.gameMasterSpawn,messageList);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(isLightShader)
                UpdateLightMask(spriteBatch);


            graphics.SetRenderTarget(gameFrame);
            spriteBatch.Begin();
            foreach(var paralax in _paralaxes)
                paralax.Draw(gameTime, spriteBatch);
            
            spriteBatch.End();
            map.Draw(_camera.Transform);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: _camera.Transform);
            foreach (Sprite sprite in sprites)
                sprite.Draw(gameTime, spriteBatch);
            
            foreach(Item item in items)
                item.Draw(gameTime, spriteBatch);

            foreach (Spring tmp in springs)
                tmp.Draw(gameTime, spriteBatch);
            
            foreach (MovableItem item in movableItems)
                item.Draw(gameTime, spriteBatch);
            
            foreach (FallableObject item in fallableObjects)
                item.Draw(gameTime, spriteBatch);

            foreach (WaterArea item in waterAreas)
                item.Draw(gameTime, spriteBatch);
            spriteBatch.End();


            graphics.SetRenderTarget(null);
            
            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            if (isLightShader)
            {
                graphics.Clear(Color.Black);
                effect.Parameters["lightMask"].SetValue(lightMask);
                effect.CurrentTechnique.Passes[0].Apply();
            }
            spriteBatch.Draw(gameFrame, Vector2.Zero, Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            gameUI.Draw(gameTime, spriteBatch, session);
            gameMaster.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public void UpdateSessionData()
        {
            session.UpdatePlayerPoints(player.points);
            player.points = 0;

            if (session.GetPlayerLives() > player.lifes)
            {
                session.SetPlayerLives(player.lifes);
                if (player.lifes <= 0)
                    game.ChangeState(new MainMenuState(game, graphics, content, null));
                else
                {
                    sprites.Remove(player);
                    sprites.Remove(ghostSprite);
                    GeneratePlayerAndBackground();
                }
            }
        }


        public override void Update(GameTime gameTime)
        {
            gameMaster.Update(gameTime,player, map);
            UpdateSessionData();
            if (!gameMaster.isActive)
            {
                foreach (Sprite sprite in sprites)
                    sprite.Update(gameTime, player, map, movableItems);
                
                map.Update(gameTime);
                _camera.Follow(ghostSprite,!isLightShader);
                foreach (var paralax in _paralaxes)
                    paralax.Update(ghostSprite, graphics);
                
                foreach (Item item in items)
                    item.Update(gameTime, player, map);
                
                foreach (MovableItem item in movableItems)
                    item.Update(gameTime, player, map, movableItems);


                List<FallableObject> newFallableList = new List<FallableObject>();
                foreach (FallableObject item in fallableObjects)
                {
                    item.Update(gameTime, player, map);
                    if (!item.isOnGround)
                        newFallableList.Add(item);
                }
                fallableObjects = newFallableList;

                foreach (WaterArea tmp in waterAreas)
                    tmp.Update(gameTime);
                
                foreach (CheckPoint checkPoint in _checkpoints)
                {
                    if ((checkPoint.isChecked) & (!checkPoint.wasChecked))
                        spawnPoint = new Vector2(checkPoint.rectangle.X, checkPoint.rectangle.Y - 50);
                }
                foreach (Spring tmp in springs)
                    tmp.Update(gameTime, player, map);


                gameUI.Update(gameTime);
                CheckEndLevel();
            }
        }
        public void CheckEndLevel()
        {
            if(player.rectangle.Intersects(EndPoint))
            {
                LevelFactory lv = new LevelFactory();
                game.ChangeState(lv.PickLevelById(nextLevelId, game,graphics,content,session));
            }
                
        }

        public override void SaveDataToSession()
        {
            session.UpdateSaveGameController(this);
        }

        private void UpdateLightMask(SpriteBatch spriteBatch)
        {

            graphics.SetRenderTarget(lightMask);
            graphics.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            foreach (Item item in items)
            {
                if(item is Tourch)
                {
                    Tourch tmp = (Tourch)item;
                        int radious = random.Next(475, 500);
                        spriteBatch.Draw(mask, new Rectangle((int)(tmp.lightSource.X - (ghostSprite.rectangle.X + ghostSprite.rectangle.Width) + (graphics.Viewport.Width / 2) - radious / 2), (int)(tmp.lightSource.Y - (ghostSprite.rectangle.Y + ghostSprite.rectangle.Height) + (graphics.Viewport.Height / 2 + graphics.Viewport.Height / 5) - radious / 2), radious, radious), Color.White);

                }
            }
            spriteBatch.End();
        }

    }
}
