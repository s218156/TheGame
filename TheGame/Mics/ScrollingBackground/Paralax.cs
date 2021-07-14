using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.Mics
{
    public class Paralax
    {
        private int height, width;
        private Texture2D texture;
        private Vector2 size;
        Background[] b = new Background[2];
        private Vector2 selfSpeed, relativeSpeed;

        public Paralax(Texture2D texture, GraphicsDevice graphics, Vector2 selfSpeed, Vector2 relativeSpeed)
        {
            this.texture = texture;
            height = graphics.Viewport.Height;
            width = graphics.Viewport.Width;
            size = new Vector2(graphics.Viewport.Width*(float)1.5, graphics.Viewport.Height*(float)2);
            this.selfSpeed = selfSpeed;
            this.relativeSpeed = relativeSpeed;
            Initialize();
        }
        public void Initialize()
        {
            b[0] = new Background(texture, new Vector2(width * (float)(-0.25), height * (float)(-0.75)), size, false);
            b[1] = new Background(texture, new Vector2(width * (float)1.25, height * (float)(-0.75)), size, true);
        }

        public void Update(Sprite player, GraphicsDevice graphics)
        {
            Vector2 finalSpeed = (-1)*player.velocity * relativeSpeed;
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
            if (b[0].rectangle.X >  graphics.Viewport.Width * (float)1.5)
                b[0].rectangle.X = b[1].rectangle.X - b[0].rectangle.Width;

            if (b[1].rectangle.X > graphics.Viewport.Width * (float)1.5)
                b[1].rectangle.X = b[0].rectangle.X - b[1].rectangle.Width;


            if (b[0].rectangle.X+b[0].rectangle.Width < -1* graphics.Viewport.Width * (float)0.5)
                b[0].rectangle.X = b[1].rectangle.X + b[1].rectangle.Width;

            if (b[1].rectangle.X + b[1].rectangle.Width <-1* graphics.Viewport.Width * (float)0.5)
                b[1].rectangle.X = b[0].rectangle.X + b[0].rectangle.Width;
            
        }
    }
}
