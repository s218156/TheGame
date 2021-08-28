using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;
using TheGame.States.Levels;

namespace TheGame.SaveAndLoadControllers
{
    public class SubLevelData
    {
        public Rectangle rectangle { get; set; }
        public SubLevelLevelData sublevel { get; set; }

        public SubLevelData()
        {
            this.sublevel = new SubLevelLevelData();
        }
        public void UpdateSubLevelData(SubLevelTrigger trigger)
        {
            if (trigger.wasWisited)
                this.sublevel.UpdateSaveGameData(trigger.sublevel);
            this.rectangle = trigger.rectangle;
        }
    }
}
