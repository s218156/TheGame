using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Items;

namespace TheGame.Mics
{
    class WaterArea
    {
        Rectangle rectangle;
        WaterAnimation animation;
        public WaterArea(Texture2D texture, Rectangle rectangle)
        {
            animation = new WaterAnimation(texture, rectangle);
            this.rectangle = rectangle;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Draw(gameTime, spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime, null);
        }
    }
}
