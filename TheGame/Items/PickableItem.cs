using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Inventory;
using TheGame.Sprites;

namespace TheGame.Items
{
    class PickableItem : Item
    {
        protected bool isActive;
        private InventoryItem inventoryItem;
        public PickableItem(Texture2D texture, Rectangle rectangle,InventoryItem inventoryItem) : base(texture, rectangle)
        {
            this.isActive = true;
            this.inventoryItem = inventoryItem;
            
        }
        public override void Draw(GameTime gameTIme, SpriteBatch spriteBatch)
        {
            if (isActive)
                spriteBatch.Draw(texture, rectangle, Color.White);
         
        }
        public override void Update(GameTime gameTime, Player player)
        {
            if ((rectangle.Intersects(player.rectangle))&isActive)
            {
                isActive = false;
                inventoryItem.UpdatePlayerStat(player);
                player.UpdateInventoryList(inventoryItem);
            }
        }
    }
}
