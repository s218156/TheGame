using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
    class Level0 : State
    {
        private TileMap map;
        private Player player;
        private List<Sprite> _sprites;
        private Camera _camera;
        private List<CheckPoint> _checkpoints;
        private GhostSprite ghostSprite;
        private List<Paralax> _paralaxes;
        private List<Item> _items;
        private Vector2 spawnPoint;
        private GameUI gameUI;
        private Vector2 EndPoint;
        private List<MovableItem> movableItems;
        private int pointAtTheBegining;
        private GameMaster gameMaster;
        private List<PhysicalObject> physicalObjects;
        private List<string>messageList;
        public Level0(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session):base(game,graphics,content, session)
        {
            Initialize();
        }

        public override void Initialize()
        {
            messageList = new List<string>(){
                "Hi Player! Welcome to the game! to move press 'A' or 'D'!" ,
                "Great! On Your right, there is a BOX. To jump on it press 'SPACE'",
                "AWESOME! If you see any spikes remember to ommit them. In this issue try to jump over the gap.",
                "Remember that You can crouch. When on the ground press 'S'!",
                "Now let's see if You can pass this!",
                "WOW! Remember to keep focused...",
                "OK. Now this is the ladder. When You are on it, you can climp by pressing 'W' or 's'",
                "Chains works same as ladders, but horizontaly...",
                "Some of the BOXES are movable. Try to reach upper platform.",
                "Now time for enemies. These are bugs. You can kill them by jumping on them",
                "But remember... they can hurt you as well...",
                "This is CheckPoint. If you die, You will respawn at this point!",
                "Now follow the arrows and DONT get killed!"

            };
            session.SetPlayerPoints(pointAtTheBegining);
            _camera = new Camera();
            _sprites = new List<Sprite>();
            _paralaxes = new List<Paralax>();
            _items = new List<Item>();
            _checkpoints = new List<CheckPoint>();
            movableItems = new List<MovableItem>();
            physicalObjects = new List<PhysicalObject>();
            gameUI = new GameUI(content);
            GenerateObjects();
        }

        private void GenerateObjects()
        {
            CoinSoundController coinSound = new CoinSoundController(content.Load<Song>("Audio/handleCoins"));

            

            map = new TileMap(content.Load<TiledMap>("TileMaps//level0/Level0-map"), graphics);
            spawnPoint = map.spawnPosition;
            player = new Player(content.Load<Texture2D>("Sprites/playerAnimation"), spawnPoint, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), session.GetPlayerLives());
            Paralax p1 = new Paralax(content.Load<Texture2D>("Backgrounds/Level0/background"), graphics, Vector2.Zero, new Vector2((float)0.5, (float)0.9));
            _paralaxes.Add(p1);
            ghostSprite = new GhostSprite(player);
            _sprites.Add(player);
            _sprites.Add(ghostSprite);
            foreach (Rectangle obj in map.movableObjects)
            {
                movableItems.Add(new MovableItem(content.Load<Texture2D>("Items/chest"), obj));
                physicalObjects.Add(new MovableItem(content.Load<Texture2D>("Items/chest"), obj));
            }
            foreach (var tmp in map.checkPoints)
            {
                var newItem = new CheckPoint(tmp, content.Load<Texture2D>("Items/checkpoint"), content.Load<Texture2D>("Items/checkpointanim"));
                _checkpoints.Add(newItem);
                _items.Add(newItem);
            }
                
            foreach (var tmp in map.GetCoins())
            {
                _items.Add(new Coin(content.Load<Texture2D>("Items/coinAnimation"), new Rectangle((int)tmp.X, (int)tmp.Y, 50, 50), 1, coinSound));
            }
            foreach (var tmp in map.GetLadders())
            {
                _items.Add(new Ladder(tmp));
            }
            foreach (var tmp in map.snails)
            {
                _sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/snailAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 100, 1));
            }
            foreach (var tmp in map.mouse)
            {
                _sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/mouseAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 300,3));
            }
            foreach (var tmp in map.worms)
            {
                _sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/greenWormAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 150,2));
            }
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
            spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: _camera.Transform);
            foreach(var paralax in _paralaxes)
            {
                paralax.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
            map.Draw(_camera.Transform);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: _camera.Transform);
            foreach (Sprite sprite in _sprites)
            {
                sprite.Draw(gameTime, spriteBatch);
            }
            foreach(Item item in _items)
            {
                item.Draw(gameTime, spriteBatch);
            }
            foreach (MovableItem item in movableItems)
            {
                item.Draw(gameTime, spriteBatch);
            }

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
                {
                    game.ChangeState(new MainMenuState(game, graphics, content, null));
                }
                else
                {
                    _sprites.Remove(player);
                    _sprites.Remove(ghostSprite);
                    player = new Player(content.Load<Texture2D>("Sprites/playerAnimation"),spawnPoint, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), session.GetPlayerLives());
                    ghostSprite = new GhostSprite(player);
                    _sprites.Insert(0,player);
                    _sprites.Add(ghostSprite);
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
                {
                    sprite.Update(gameTime, player, map, movableItems);
                }
                map.Update(gameTime);
                _camera.Follow(ghostSprite);
                foreach (var paralax in _paralaxes)
                {
                    paralax.Update(ghostSprite, graphics);
                }
                foreach (Item item in _items)
                {
                    item.Update(gameTime, player);
                }
                foreach (MovableItem item in movableItems)
                {
                    item.Update(gameTime, player, map, movableItems);
                }
                foreach (CheckPoint checkPoint in _checkpoints)
                {
                    if ((checkPoint.isChecked) & (!checkPoint.wasChecked))
                    {
                        spawnPoint = new Vector2(checkPoint.rectangle.X, checkPoint.rectangle.Y - 50);
                    }
                }


                gameUI.Update(gameTime);
                CheckEndLevel();
            }
        }
        public void CheckEndLevel()
        {
            if(player.rectangle.Intersects(new Rectangle((int)EndPoint.X, (int)EndPoint.Y, 100, 100)))
            {
                Thread.Sleep(200);
                game.ChangeState(new Level1(game, graphics, content, session));
            }
        }
    }
}
