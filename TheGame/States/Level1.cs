using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.States
{
    class Level1 : State
    {
        private TileMap map;
        private Player player;
        private List<Sprite> _sprites;
        private Camera _camera;
        private GhostSprite ghostSprite;
        public Level1(Game1 game, GraphicsDevice graphics, ContentManager content):base(game,graphics,content)
        {
            Initialize();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: _camera.Transform);
            foreach (Sprite sprite in _sprites)
            {
                sprite.Draw(gameTime,spriteBatch);
            }
            spriteBatch.End();
            map.Draw(_camera.Transform);
        }

        public override void Initialize()
        {
            map = new TileMap(content.Load<TiledMap>("TileMaps//level1/testLevel1"), graphics);
            player = new Player(content.Load<Texture2D>("Sprites/test-character"), Vector2.Zero);
            _camera = new Camera();
            _sprites = new List<Sprite>();
            _sprites.Add(player);
            ghostSprite = new GhostSprite(player);
            _sprites.Add(ghostSprite);
        }

        public override void Update(GameTime gameTime)
        {
            foreach(Sprite sprite in _sprites)
            {
                sprite.Update(gameTime, player, map);
            }
            map.Update(gameTime);
            _camera.Follow(ghostSprite);

        }
    }
}
