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

        public void Update(GameTime gameTime, Player player)
        {
            if (rectangle.Intersects(player.rectangle))
                wasTouched = true;

            if (wasTouched)
            {
                if (timeAfterTouch < 100)
                {
                    timeAfterTouch++;
                }
                else
                {
                    GravitySimulation();
                }
            }
            rectangle.X += (int)velocity.X;
            rectangle.Y += (int)velocity.Y;




        }
        
    }
    
}
