using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics
{
    public class SessionData
    {
        private int playerLives;
        private int playerPoints;

        public SessionData()
        {
            this.playerLives = 5;
            this.playerPoints = 0;
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

        public int GetPlayerLives()
        {
            return playerLives;
        }
        public void SetPlayerLives(int quantity)
        {
            this.playerLives = quantity;
        }
    }
}
