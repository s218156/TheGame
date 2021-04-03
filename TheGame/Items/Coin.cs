using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Sprites;

namespace TheGame.Items
{
    public class Coin:Item
    {
        int value;
        bool isActive;
        private ItemAnimation animation;
        public Coin(Texture2D texture, Rectangle rectangle, int value) : base(texture, rectangle)
        {
            this.value = value;
            this.isActive = true;
            this.animation = new ItemAnimation(texture, rectangle,4,1);

        }

        public override void Draw(GameTime gameTIme, SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                animation.Draw(gameTIme, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime, Player player)
        {
            animation.Update(gameTime);
            if ((rectangle.Intersects(player.rectangle))&(isActive))
            {
                isActive = false;
                player.points += value;
            }

        }
    }
}
