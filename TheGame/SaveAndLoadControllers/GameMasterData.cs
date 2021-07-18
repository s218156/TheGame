using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Items;

namespace TheGame.SaveAndLoadControllers
{
    public class GameMasterData
    {
        public List<String> captions { get; set; }
        public List<Rectangle> triggers { get; set; }
        public GameMasterData()
        {
            this.captions = new List<String>();
            this.triggers = new List<Rectangle>();
        }
        public void UpdateGameMasterData(GameMaster gameMaster)
        {
            this.captions = gameMaster.captions;
            this.triggers = gameMaster.triggers;
        }
    }
}
