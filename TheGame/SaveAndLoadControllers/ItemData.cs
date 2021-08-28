using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Items;

namespace TheGame.SaveAndLoadControllers
{
    public class ItemData
    {
        public bool isActive { get; set; }
        public Rectangle rectangle { get; set; }
        public ItemData()
        {
            this.rectangle = new Rectangle(0, 0, 0, 0);
        }

        public void UpdateItemData(Item item)
        {
            this.isActive = item.isActive;
            this.rectangle = item.rectangle;
        }
    }
}
