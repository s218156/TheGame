using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.SaveAndLoadControllers
{
    public class SpriteData
    {
        public Rectangle possition { set; get; }
        public Vector2 velocity { set; get; }
        public bool isAlive { get; set; }

        public SpriteData()
        {
            possition = new Rectangle(0, 0, 0, 0);
            velocity = Vector2.Zero;
        }

    public void UpdateSpriteData(Sprite sprite)
        {
            this.possition = sprite.rectangle;
            this.velocity = sprite.velocity;
            this.isAlive = sprite.isAlive;
        }
    }
}
