using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Items;
using TheGame.Sprites;

namespace TheGame.Mics
{
    public class WaterArea
    {
        public Rectangle rectangle;
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
        public void Update(GameTime gameTime, List<Sprite> sprites)
        {
            animation.Update(gameTime, null);
        }
    }
}
