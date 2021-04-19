using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;
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
        private GhostSprite ghostSprite;
        private List<Paralax> _paralaxes;
        private List<Item> _items;
        private GameUI gameUI;
        private Vector2 EndPoint;
        private List<MovableItem> movableItems;
        private int pointAtTheBegining;
        private GameMaster gameMaster;
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
                "Remember that You can crouch. When on the ground press 'S'!"
            };
            session.SetPlayerPoints(pointAtTheBegining);
            _camera = new Camera();
            _sprites = new List<Sprite>();
            _paralaxes = new List<Paralax>();
            _items = new List<Item>();
            movableItems = new List<MovableItem>();
            gameUI = new GameUI(content);
            GenerateObjects();
        }

        private void GenerateObjects()
        {
            CoinSoundController coinSound = new CoinSoundController(content.Load<Song>("Audio/handleCoins"));

            map = new TileMap(content.Load<TiledMap>("TileMaps//level0/Level0-map"), graphics);
            player = new Player(content.Load<Texture2D>("Sprites/playerAnimation"), map.spawnPosition, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), session.GetPlayerLives());
            Paralax p1 = new Paralax(content.Load<Texture2D>("Backgrounds/Level0/background"), graphics, Vector2.Zero, new Vector2((float)0.5, (float)0.9));
            _paralaxes.Add(p1);
            ghostSprite = new GhostSprite(player);
            _sprites.Add(player);
            _sprites.Add(ghostSprite);
            foreach (Rectangle obj in map.movableObjects)
            {
                movableItems.Add(new MovableItem(content.Load<Texture2D>("Items/chest"), obj));
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
                _sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/snailAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 100));
            }
            foreach (var tmp in map.mouse)
            {
                _sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/mouseAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 300));
            }
            foreach (var tmp in map.worms)
            {
                _sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/greenWormAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"), 150));
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
                    Initialize();

                }
            }
        }


        public override void Update(GameTime gameTime)
        {
            gameMaster.Update(gameTime,player);

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
                UpdateSessionData();

                gameUI.Update(gameTime);
                CheckEndLevel();
            }
        }
        public void CheckEndLevel()
        {
            if(player.rectangle.Intersects(new Rectangle((int)EndPoint.X, (int)EndPoint.Y, 100, 100)))
            {
                game.ChangeState(new MainMenuState(game, graphics, content, session));
            }
        }
    }
}
