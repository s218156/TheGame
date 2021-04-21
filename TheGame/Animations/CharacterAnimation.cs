using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.Animations
{
    public class CharacterAnimation:AnimatedTexture
    {
        private int currentRow;
        public CharacterAnimation(Texture2D texture,Rectangle rectangle) : base(texture, rectangle, 4, 6)
        {
            totalFrames = 4;
            currentRow = 0;
            this.direction = true;
        }

        public override void Update(GameTime gameTime, Sprite sprite)
        {
            ObtainDirection(sprite.velocity);
            this.rectangle = sprite.rectangle;
            if (sprite.isOnLadder )
                currentRow = 3;
            else
            {
                if (sprite.velocity.Y < 0)
                    currentRow = 2;
                else
                {
                    if (sprite.velocity.Y > 0)
                        currentRow = 5;
                    else
                    {
                        if (sprite.crouch)
                            currentRow = 4;
                        else
                        {
                            if (Math.Abs(sprite.velocity.X )>1)
                                currentRow = 1;
                            else
                                currentRow = 0;
                        }
                    }
                }
            }

            timer++;
            if (timer == 10)
            {
                timer = 0;
                currentFrame++;
                if (currentFrame%4 == 0)
                    currentFrame -= 4;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * currentRow, width, height);

            if (!direction)
                spriteBatch.Draw(texture, rectangle, sourceRectangle, Color.White);
            else
                spriteBatch.Draw(texture, rectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, (float)0);
        }
    }
}
