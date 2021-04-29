﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.Items
{
    class Lever : Item
    {
        public bool isRight;
        private Rectangle colisionRectangle;
        private LeverAnimation animation;
        private List<Platform> platforms;
        public Lever(Texture2D texture,Rectangle rectangle, TileMap map,List<Platform> platforms) : base(null, rectangle)
        {
            isRight = false;
            colisionRectangle = new Rectangle(rectangle.X + (rectangle.Width / 10), rectangle.Y + 3 * (rectangle.Height / 4), 8 * (rectangle.Width / 10), rectangle.Height / 4);
            animation = new LeverAnimation(texture, rectangle);
            map.mapObjects.Add(colisionRectangle);
            this.platforms = platforms;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Draw(gameTime, spriteBatch);
            foreach (Platform tmp in platforms)
                tmp.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime, Player player, TileMap map)
        {
            if ((rectangle.Intersects(player.rectangle))&(player.crouch)){
                if (player.velocity.X != 0)
                {
                    if (player.velocity.X > 0)
                        isRight = true;
                    else
                        isRight = false;
                }  
            }

            foreach (Platform tmp in platforms)
                tmp.Update(this, map);
            animation.Update(gameTime, this);
        }
    }
}