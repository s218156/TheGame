using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;

namespace TheGame.Items
{
    public class Platform
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private bool isAcitve;
        public Platform(Texture2D texture, Rectangle rectangle)
        {
            this.texture = texture;
            this.rectangle = rectangle;
        }

        public void Update(Lever lever, TileMap map)
        {
            if (lever.isActive)
            {
                isAcitve = true;
                if(!map.mapObjects.Contains(rectangle))
                map.mapObjects.Add(rectangle);
            }
            else
            {
                isAcitve = false;
                while(map.mapObjects.Contains(rectangle))
                    map.mapObjects.Remove(rectangle);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(isAcitve)
                spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
