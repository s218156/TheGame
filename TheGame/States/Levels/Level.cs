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
using TheGame.States.Menu;

namespace TheGame.States
{
    abstract class Level : State
    {
        protected TileMap map;
        protected Player player;
        protected List<Sprite> _sprites;
        protected Camera _camera;
        protected List<CheckPoint> _checkpoints;
        protected GhostSprite ghostSprite;
        protected List<Paralax> _paralaxes, _startParalaxes;
        protected List<Item> _items;
        protected Vector2 spawnPoint;
        protected GameUI gameUI;
        protected Vector2 EndPoint;
        protected List<MovableItem> movableItems;
        protected List<FallableObject> fallableObjects;
        protected int pointAtTheBegining;
        protected GameMaster gameMaster;
        protected List<string>messageList;
        protected List<Spring> springs;
        private State nextGameState;
        public Level(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session,State nextGameState):base(game,graphics,content, session)
        {
            pointAtTheBegining = session.GetPlayerPoints();
            _paralaxes = new List<Paralax>();
            this.nextGameState = nextGameState;
        }

        public void GeneratePlayerAndBackground()
        {
            player = new Player(content.Load<Texture2D>("Sprites/playerAnimation"), spawnPoint, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), session.GetPlayerLives());

            foreach (Paralax tmp in _paralaxes)
                tmp.Initialize();

            ghostSprite = new GhostSprite(player);
            _sprites.Insert(0, player);
            _sprites.Add(ghostSprite);
        }

        public override void Initialize()
        {
            session.SetPlayerPoints(pointAtTheBegining);
            _startParalaxes = new List<Paralax>(_paralaxes);
            _camera = new Camera();
            _sprites = new List<Sprite>();
            _items = new List<Item>();
            _checkpoints = new List<CheckPoint>();
            movableItems = new List<MovableItem>();
            gameUI = new GameUI(content);
            fallableObjects = new List<FallableObject>();
            springs = new List<Spring>();
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

            foreach (var tmp in map.checkPoints)
            {
                var newItem = new CheckPoint(tmp, content.Load<Texture2D>("Items/checkpoint"), content.Load<Texture2D>("Items/checkpointanim"));
                _checkpoints.Add(newItem);
                _items.Add(newItem);
            }
                
            foreach (var tmp in map.GetCoins())
                _items.Add(new Coin(content.Load<Texture2D>("Items/coinAnimation"), new Rectangle((int)tmp.X, (int)tmp.Y, 50, 50), 1, coinSound));
            
            foreach (var tmp in map.GetLadders())
                _items.Add(new Ladder(tmp));
            
            foreach (var tmp in map.snails)
                _sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/snailAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 100, 1));
            
            foreach (var tmp in map.mouse)
                _sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/mouseAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 300,3));
            
            foreach (var tmp in map.worms)
                _sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/greenWormAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 150,2));
            
            EndPoint = map.endPosition;
            foreach (var tmp in map.powerups)
            {
                if (tmp.Height == 1)
                {
                    InventoryItem tmpItem = new InventoryItem(tmp.Height, 20, content.Load<Texture2D>("jetpack"), false);
                    _items.Add(new PickableItem(content.Load<Texture2D>("jetpack"), new Rectangle(tmp.X, tmp.Y, tmp.Width, tmp.Width), tmpItem));
                }
            }
            gameMaster = new GameMaster(content, new Vector2(graphics.Viewport.Width,graphics.Viewport.Height), map.gameMasterSpawn,messageList);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach(var paralax in _paralaxes)
                paralax.Draw(gameTime, spriteBatch);
            
            spriteBatch.End();
            map.Draw(_camera.Transform);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: _camera.Transform);
            foreach (Sprite sprite in _sprites)
                sprite.Draw(gameTime, spriteBatch);
            
            foreach(Item item in _items)
                item.Draw(gameTime, spriteBatch);

            foreach (Spring tmp in springs)
                tmp.Draw(gameTime, spriteBatch);
            
            foreach (MovableItem item in movableItems)
                item.Draw(gameTime, spriteBatch);
            
            foreach (FallableObject item in fallableObjects)
                item.Draw(gameTime, spriteBatch);
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
                    _sprites.Remove(player);
                    _sprites.Remove(ghostSprite);
                    GeneratePlayerAndBackground();
                }
            }
        }


        public override void Update(GameTime gameTime)
        {
            gameMaster.Update(gameTime,player);
            UpdateSessionData();
            if (!gameMaster.isActive)
            {
                foreach (Sprite sprite in _sprites)
                    sprite.Update(gameTime, player, map, movableItems);
                
                map.Update(gameTime);
                _camera.Follow(ghostSprite);
                foreach (var paralax in _paralaxes)
                    paralax.Update(ghostSprite, graphics);
                
                foreach (Item item in _items)
                    item.Update(gameTime, player);
                
                foreach (MovableItem item in movableItems)
                    item.Update(gameTime, player, map, movableItems);
                
                foreach (FallableObject item in fallableObjects)
                    item.Update(gameTime, player,map);
                
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
            if(player.rectangle.Intersects(new Rectangle((int)EndPoint.X, (int)EndPoint.Y, 100, 100)))
            {
                nextGameState.UpdateSessionData(session);
                game.ChangeState(nextGameState);
            }
                
        }

    }
}
