using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.SaveAndLoadControllers
{
    public class PlayerData
    {
        public Rectangle possition { get; set; }
        public Vector2 velocity { get; set; } 
        public PlayerData()
        {
            possition = new Rectangle(0, 0, 0, 0);
            velocity = Vector2.Zero;
        }
        public void UpdatePlayerData(Player player)
        {
            this.possition = player.rectangle;
            this.velocity = player.velocity;
        }
    }
}
