using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics
{
    class Background
    {
        public Texture2D texture;
        public Rectangle rectangle;
        private bool mirroredTexture;

        public Background(Texture2D texture, Vector2 position, Vector2 size, bool isMirrored)
        {
            this.texture = texture;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y,(int) size.X,(int) size.Y);
            this.mirroredTexture = isMirrored;
        }

        public void Update(Vector2 speed)
        {
            rectangle.X += (int)speed.X;
            rectangle.Y += (int)speed.Y;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (mirroredTexture)
                spriteBatch.Draw(texture, rectangle, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0f);
            else
                spriteBatch.Draw(texture, rectangle, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
        }
    }
}
