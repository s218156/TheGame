using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.States.Levels.Sublevels;

namespace TheGame.Mics
{
    public class SubLevelTrigger
    {
        public Rectangle rectangle;
        public int sublevelId;
        public bool wasWisited;
        public Sublevel sublevel;

        public SubLevelTrigger(int sublevelId, Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.sublevelId = sublevelId;
            this.wasWisited = false;
        }
    }
    
}
