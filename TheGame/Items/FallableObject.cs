using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.Items
{
    public class FallableObject : PhysicalObject
    {
        int timeAfterTouch;
        bool wasTouched;
        public FallableObject(Texture2D texture, Rectangle rectangle):base(texture,rectangle)
        {
            timeAfterTouch = 0;
            wasTouched = false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void Update(GameTime gameTime, Player player, TileMap map)
        {
            Rectangle tmp = new Rectangle(rectangle.X,rectangle.Y-5,rectangle.Width,rectangle.Height);

            if (tmp.Intersects(player.rectangle))
                wasTouched = true;

            if (wasTouched)
            {
                if (timeAfterTouch < 25)
                    timeAfterTouch++;
                else
                {
                    if (rectangle.X < 500 * 64)
                    {
                        GravitySimulation();
                        if (map.mapObjects.Contains(rectangle))
                            map.mapObjects.Remove(rectangle);
                    }
                    
                }
                    

            }
            UpdatePosition();
        }        
    }
    
}
