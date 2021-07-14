using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using TheGame.States;

namespace TheGame.SaveAndLoadControllers
{
    public class SaveGameController
    {
        public PlayerData playerData { get; set; }
        public SaveGameController()
        {
            this.playerData = new PlayerData();
        }
        public void UpdateSaveGameData(Level level)
        {
            this.playerData.UpdatePlayerData(level.player);
        }

        public void SaveGame()
        {
            using (var stream = new FileStream("Save1.xml", FileMode.Create))
            {
                var XML = new XmlSerializer(typeof(SaveGameController));
                XML.Serialize(stream, this);
            }
        }

    }
}
