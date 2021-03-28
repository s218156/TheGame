using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.Items
{
    public class Ladder : Item
    {
        public Ladder(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {

        }

        public override void Draw(GameTime gameTIme, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public override void Update(GameTime gameTime, Player player)
        {
            
        }
    }
}
