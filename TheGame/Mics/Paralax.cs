using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.Mics
{
    class Paralax
    {
        Background[] b = new Background[2];
        private Vector2 selfSpeed, relativeSpeed;
        public Paralax(Texture2D texture, GraphicsDevice graphics, Vector2 selfSpeed, Vector2 relativeSpeed)
        {
            Vector2 size = new Vector2(graphics.Viewport.Width*(float)1.5, graphics.Viewport.Height*(float)2);
            this.selfSpeed = selfSpeed;
            this.relativeSpeed = relativeSpeed;
            b[0] = new Background(texture, new Vector2( graphics.Viewport.Width*(float)(-0.25), graphics.Viewport.Height * (float)(-0.5)),size, false);
            b[1] = new Background(texture, new Vector2(graphics.Viewport.Width*(float)1.25, graphics.Viewport.Height * (float)(-0.5)), size, true);
        }

        public void Update(Sprite player, GraphicsDevice graphics)
        {
            Vector2 finalSpeed = Vector2.Zero;
            finalSpeed = player.velocity * relativeSpeed;
            finalSpeed += selfSpeed;
            b[0].Update(finalSpeed);
            b[1].Update(finalSpeed);

            ToogleBackgrounds(player, graphics);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            b[0].Draw(gameTime, spriteBatch);
            b[1].Draw(gameTime, spriteBatch);
        }

        private void ToogleBackgrounds(Sprite player, GraphicsDevice graphics)
        {
            if (b[0].rectangle.X > player.rectangle.X + graphics.Viewport.Width * (float)0.7)
            {
                b[0].rectangle.X = b[1].rectangle.X - b[0].rectangle.Width;
            }
            if (b[1].rectangle.X > player.rectangle.X + graphics.Viewport.Width * (float)0.5)
            {
                b[1].rectangle.X = b[0].rectangle.X - b[1].rectangle.Width;
            }


            if (b[0].rectangle.X+b[0].rectangle.Width < player.rectangle.X - graphics.Viewport.Width * (float)0.7)
            {
                b[0].rectangle.X = b[1].rectangle.X + b[1].rectangle.Width;
            }
            if (b[1].rectangle.X + b[1].rectangle.Width < player.rectangle.X - graphics.Viewport.Width * (float)0.7)
            {
                b[1].rectangle.X = b[0].rectangle.X + b[0].rectangle.Width;
            }
        }
    }
}
