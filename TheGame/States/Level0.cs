using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Items;
using TheGame.Mics;
using TheGame.Mics.GUI_components;
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
        public Level0(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session):base(game,graphics,content, session)
        {
            Initialize();
        }

        public override void Initialize()
        {
            map = new TileMap(content.Load<TiledMap>("TileMaps//level0/Level0-map"), graphics);
            player = new Player(content.Load<Texture2D>("Sprites/playerAnimation"), map.spawnPosition,content.Load<Texture2D>("textureEffects/whiteFogAnimation"),session.GetPlayerLives());
            _camera = new Camera();
            _sprites = new List<Sprite>();
            _sprites.Add(player);
            ghostSprite = new GhostSprite(player);
            _sprites.Add(ghostSprite);

            _paralaxes = new List<Paralax>();
            Paralax p1 = new Paralax(content.Load<Texture2D>("Backgrounds/Level0/background"), graphics, Vector2.Zero, new Vector2((float)0.5,(float) 0.9));
            _paralaxes.Add(p1);

            _items = new List<Item>();
            foreach(var tmp in map.GetCoins())
            {
                _items.Add(new Coin(content.Load<Texture2D>("Items/coinAnimation"), new Rectangle((int)tmp.X,(int)tmp.Y,50,50), 1));
            }
            foreach (var tmp in map.GetLadders())
            {
                _items.Add(new Ladder(tmp));
            }
            foreach (var tmp in map.GetEnemies())
            {
                _sprites.Add(new MovingBug(content.Load<Texture2D>("Sprites/snailAnimation"), tmp, content.Load<Texture2D>("textureEffects/whiteFogAnimation"),250));
            }



            gameUI = new GameUI(content);



        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: _camera.Transform);
            foreach(var paralax in _paralaxes)
            {
                paralax.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: _camera.Transform);
            foreach (Sprite sprite in _sprites)
            {
                sprite.Draw(gameTime,spriteBatch);
            }
            foreach(Item item in _items)
            {
                item.Draw(gameTime, spriteBatch);
            }
            
            spriteBatch.End();
            map.Draw(_camera.Transform);
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            gameUI.Draw(gameTime, spriteBatch, session);
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
            foreach(Sprite sprite in _sprites)
            {
                sprite.Update(gameTime, player, map);
            }
            map.Update(gameTime);
            _camera.Follow(ghostSprite);
            foreach(var paralax in _paralaxes)
            {
                paralax.Update(ghostSprite, graphics);
            }
            foreach(Item item in _items)
            {
                item.Update(gameTime, player);
            }
            UpdateSessionData();

            gameUI.Update(gameTime);
            

        }
    }
}
