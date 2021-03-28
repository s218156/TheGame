﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Items;
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
        private List<Paralax> _paralaxes;
        private List<Item> _items;
        public Level1(Game1 game, GraphicsDevice graphics, ContentManager content):base(game,graphics,content)
        {
            Initialize();
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

            _paralaxes = new List<Paralax>();
            Paralax p1 = new Paralax(content.Load<Texture2D>("Backgrounds/Level1/mountain"), graphics, Vector2.Zero, new Vector2((float)0.5,(float) 0.9));
            _paralaxes.Add(p1);

            _items = new List<Item>();
            foreach(var tmp in map.GetCoins())
            {
                _items.Add(new Coin(content.Load<Texture2D>("Items/coin1"), new Rectangle((int)tmp.X,(int)tmp.Y,50,50), 1));
            }
            foreach (var tmp in map.GetLadders())
            {
                _items.Add(new Ladder(content.Load<Texture2D>("Items/ladder"), tmp));
            }




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

        }
    }
}
