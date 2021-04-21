using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.Animations
{
    
    public class BasicSpriteAnimation:AnimatedTexture
    {     
        public BasicSpriteAnimation(Texture2D texture,Rectangle rectangle):base(texture,rectangle,3,1)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);

            if (!direction)
                spriteBatch.Draw(texture, rectangle, sourceRectangle, Color.White);
            else
                spriteBatch.Draw(texture, rectangle, sourceRectangle, Color.White,0,Vector2.Zero,SpriteEffects.FlipHorizontally,(float)0);          
        }

        public override void Update(GameTime gameTime,Sprite sprite)
        {
            this.rectangle = sprite.rectangle;
            if (!(sprite.isAlive))
                currentFrame = 2;
            else
            {
                ObtainDirection(sprite.velocity);

                timer++;
                if (timer == 10)
                {
                    timer = 0;
                    currentFrame++;
                    if (currentFrame == 2)
                        currentFrame = 0;
                }
            }  
        }
    }
}
