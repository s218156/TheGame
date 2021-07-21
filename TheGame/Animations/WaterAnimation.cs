using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Text;
using TheGame.Sprites;

namespace TheGame.Animations
{
    class WaterAnimation:AnimatedTexture
    {
        bool isTextureFlipped;
        int width;
        int height;
        public WaterAnimation(Texture2D texture, Rectangle rectangle) : base(texture, rectangle, 1, 2)
        {
            timer = 0;
            isTextureFlipped = false;
            width = texture.Width / columns;
            height = texture.Height / rows;
        }
        public override void Update(GameTime gameTime, Sprite sprite)
        {
            timer++;
            if (timer > 20)
            {
                timer = 0;
                isTextureFlipped = !isTextureFlipped;
            }
                
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle tmp = new Rectangle(rectangle.X, rectangle.Y, 64, 64);
            int i = 0;
            for(int rows = 0; rows * 64 < rectangle.Height; rows++)
            {
                for(int columns = 0; columns * 64 < rectangle.Width; columns++)
                {
                    if (rows == 0)
                        DrawPeace(gameTime, spriteBatch, new Rectangle(rectangle.X + columns * 64, rectangle.Y + rows * 64, 64, 64), 1);
                    else
                        DrawPeace(gameTime, spriteBatch, new Rectangle(rectangle.X + columns * 64, rectangle.Y + rows * 64, 64, 64), 0);
                }
            }
            
        }

        private void DrawPeace(GameTime gameTime, SpriteBatch spriteBatch,Rectangle target, int type)
        {
            Rectangle sourceRectangle = new Rectangle(64 * type, 0, width / 2, height / 2);
            if (isTextureFlipped)
                spriteBatch.Draw(texture, target, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, (float)0);
            else
                spriteBatch.Draw(texture, target, sourceRectangle, Color.White);


        }
    }
}
