using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Text;
using TheGame.Items;
using TheGame.Sprites;

namespace TheGame.Inventory
{
    public class InventoryItem 
    {
        public Rectangle rectangle;
        public bool drawableOnTop;
        public Texture2D texture;
        private int itemType, powerupValue;     //itemType: 1-jetpack (jump powerup)
        private PickableItem trigger;
        public  InventoryItem(int itemType,int powerupValue,Texture2D texture,bool drawableOnTop)
        {
            this.drawableOnTop = drawableOnTop;
            this.texture = texture;
            this.itemType = itemType;
            this.powerupValue = powerupValue;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void Update(GameTime gametime, Player player)
        {
            rectangle = new Rectangle(player.rectangle.X + (player.rectangle.Height / 3), player.rectangle.Y, player.rectangle.Width/2, (player.rectangle.Height / 3) * 2);
        }

        public void UpdatePlayerStat(Player player)
        {
            if (itemType == 1)
            {
                player.jumpHeight += powerupValue;
            }
        }
    }

}
