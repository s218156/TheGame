using System;
using System.Collections.Generic;
using System.Text;
using TheGame.SaveAndLoadControllers;
using TheGame.States;

namespace TheGame.Mics
{
    public class SessionData
    {
        private int playerLives;
        private int playerPoints;
        private SaveGameController saveGameController;

        public SessionData()
        {
            this.playerLives = 5;
            this.playerPoints = 0;
            this.saveGameController = new SaveGameController();
        }
        public void UpdatePlayerPoints(int value)
        {
            playerPoints += value;
        }
        public void SetPlayerPoints(int value)
        {
            playerPoints = value;
        }

        public int GetPlayerPoints()
        {
            return playerPoints;
        }
        public void UpdateSaveGameController(Level level)
        {
            saveGameController.UpdateSaveGameData(level);
        }

        public int GetPlayerLives()
        {
            return playerLives;
        }
        public void SetPlayerLives(int quantity)
        {
            this.playerLives = quantity;
        }
        public void SaveData()
        {
            saveGameController.SaveGame();
        }
    }
}
