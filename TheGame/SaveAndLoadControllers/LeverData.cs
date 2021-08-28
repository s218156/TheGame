using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Items;

namespace TheGame.SaveAndLoadControllers
{
    public class LeverData
    {
        public bool isActive { set; get; }

        public LeverData()
        {
            isActive = false;
        }
        public void UpdateLeverData(Lever lever)
        {
            this.isActive = lever.isActive;
        }
    }
}
