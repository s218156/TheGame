using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;

namespace TheGame.Sprites
{
    public abstract class Sprite
    {
        protected bool isOnLadder;
        protected bool floorColision;
        protected bool jump;
        public Rectangle rectangle;
        public Texture2D texture;
        public Vector2 velocity;
        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 100, 100);
            velocity = Vector2.Zero;
            floorColision = false;
            jump = false;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
        public virtual void Update(GameTime gameTime,Sprite player, TileMap map)
        {
            FrictionCount();
            GravitySimulation();
            CheckEnviromentColision(map);
            rectangle.X +=(int) velocity.X;
            rectangle.Y += (int)velocity.Y;
            

        }

        public void FrictionCount()
        {
            velocity.X =velocity.X- velocity.X * (float)0.1;
            velocity.Y = velocity.Y - velocity.Y * (float)0.08;
        }

        public void GravitySimulation()
        {
            if (!isOnLadder)
            {
                velocity.Y++;
            }
        }
        public void CheckEnviromentColision(TileMap map)
        {
            int i;
            List<Rectangle> mapObjects = map.GetMapObjectList();
            floorColision = false;
            foreach(Rectangle obj in mapObjects)
            {

                //kolizje po X
                if (!(velocity.X == 0))
                {
                    if (velocity.X > 0)
                    {
                        for (i = 1; i <= (int)velocity.X; i++)
                        {
                            if ((new Rectangle(rectangle.X + i, rectangle.Y , rectangle.Width, rectangle.Height).Intersects(obj)))
                            {
                                velocity.X = i - 1;

                            }
                        }
                    }
                    else
                    {
                        for (i = -1; i >= (int)velocity.X; i--)
                        {
                            if ((new Rectangle(rectangle.X + i, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(obj)))
                            {
                                velocity.X = i + 1;

                            }
                        }
                    }
                }



                //kolizje po Y
                if (!(velocity.Y == 0))
                {
                    if (velocity.Y > 0)
                    {
                        for (i = 1; i <= (int)velocity.Y; i++)
                        {
                            if ((new Rectangle(rectangle.X, rectangle.Y + i, rectangle.Width , rectangle.Height).Intersects(obj)))
                            {
                                floorColision = true;
                                velocity.Y = i - 1;

                            }

                        }
                    }
                    else
                    {
                       
                            for (i = -1; i >= (int)velocity.Y; i--)
                            {
                                if ((new Rectangle(rectangle.X, rectangle.Y + i, rectangle.Width, rectangle.Height).Intersects(obj)))
                                {
                                    velocity.Y = i + 1;
                                }
                            }
                        
                        
                    }
                }
                    
                
                


            }
        }
    }
}
