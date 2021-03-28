using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.Items
{
    public class Coin:Item
    {
        int value;
        bool isActive;
        public Coin(Texture2D texture, Rectangle rectangle, int value) : base(texture, rectangle)
        {
            this.value = value;
            this.isActive = true;
        }

        public override void Draw(GameTime gameTIme, SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(texture, rectangle, Color.White);
            }
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if ((rectangle.Intersects(player.rectangle))&(isActive))
            {
                isActive = false;
                player.points += value;
            }
        }
    }
}
