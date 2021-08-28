using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Items;

namespace TheGame.SaveAndLoadControllers
{
    public class MovableData
    {
        public Rectangle rectangle { get; set; }
        public MovableData()
        {
            rectangle = new Rectangle(0, 0, 0, 0);
        }
        public void UpdateMovableData(MovableItem movable)
        {
            this.rectangle = movable.rectangle;
        }
    }
}
