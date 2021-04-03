using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Animations
{

    public abstract class AnimatedTexture
    {
        protected int timer, columns, rows, currentFrame, totalFrames;
        protected Texture2D texture;
        protected Rectangle rectangle;
        

        public AnimatedTexture(Texture2D texture, Rectangle rectangle,int columns, int rows)
        {
            this.texture = texture;
            this.rectangle = rectangle;
            this.timer = 0;
            this.currentFrame = 0;
            this.columns = columns;
            this.rows = rows;
            this.totalFrames = columns * rows;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

            spriteBatch.Draw(texture, rectangle, sourceRectangle, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            timer++;
            if (timer == 10)
            {
                timer = 0;
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }
        }

    }
}
