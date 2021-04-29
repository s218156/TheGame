using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics
{
    public class LeverInstanceForMap
    {
        public Rectangle rectangle;
        public int leverNumber;
        public LeverInstanceForMap(Rectangle rectangle, int leverNumber)
        {
            this.rectangle = rectangle;
            this.leverNumber = leverNumber;
        }
    }
}
