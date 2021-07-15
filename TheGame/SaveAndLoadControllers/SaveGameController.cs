﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using TheGame.Items;
using TheGame.Sprites;
using TheGame.States;

namespace TheGame.SaveAndLoadControllers
{
    public class SaveGameController
    {
        public int LevelId { set; get; }
        public PlayerData playerData { get; set; }
        public List<SpriteData> spritesData {get; set; }
        public List<ItemData> itemsData { get; set; }
        public int playerLives { get; set; }
        public int PlayerPoints { get; set; }
        public SaveGameController()
        {
            this.playerData = new PlayerData();
            this.spritesData = new List<SpriteData>();
            this.itemsData = new List<ItemData>();
        }
        public void UpdateSaveGameData(Level level)
        {
            this.LevelId = level.levelId;
            this.playerData.UpdatePlayerData(level.player);
            foreach(Sprite sprite in level.sprites)
            {
                SpriteData tmp = new SpriteData();
                tmp.UpdateSpriteData(sprite);
                spritesData.Add(tmp);
            }
            foreach(Item item in level.items)
            {
                ItemData tmp = new ItemData();
                tmp.UpdateItemData(item);
                itemsData.Add(tmp);
            }
            this.playerLives = level.session.GetPlayerLives();
            this.PlayerPoints = level.session.GetPlayerPoints();
        }

        public SaveGameController LoadGame()
        {
            using (var stream = new FileStream("save1.xml", FileMode.Open))
            {
                var XML = new XmlSerializer(typeof(SaveGameController));
                return (SaveGameController)XML.Deserialize(stream);
            }
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
