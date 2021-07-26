using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics
{
    public class subLevelInstanceForMap
    {
        public Rectangle rectangle;
        public int sublevelId;

        public subLevelInstanceForMap(int sublevelId, Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.sublevelId = sublevelId;
        }
    }
}
