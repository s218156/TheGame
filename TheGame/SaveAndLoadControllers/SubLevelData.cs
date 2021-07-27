using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;

namespace TheGame.SaveAndLoadControllers
{
    public class SubLevelData
    {
        public Rectangle rectangle { get; set; }
        public SaveGameController sublevel { get; set; }

        public SubLevelData()
        {
            this.sublevel = new SaveGameController();
        }
        public void UpdateSubLevelData(SubLevelTrigger trigger)
        {
            this.sublevel.UpdateSaveGameData(trigger.sublevel);
            this.rectangle = trigger.rectangle;
        }
    }
}
