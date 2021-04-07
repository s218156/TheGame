using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.Items
{
    
    public class MovableItem : Item
    {
        private Vector2 velocity;
        public MovableItem(Texture2D texture,Rectangle rectangle) : base(texture, rectangle)
        {

        }
        public override void Draw(GameTime gameTIme, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void Update(GameTime gameTime, Player player,TileMap map, List<MovableItem> movableItems)
        {
            FrictionCount();
            GravitySimulation();
            CheckEnviromentColision(map);
            CheckColisionWithOtherObjects(movableItems);
            rectangle.X += (int)velocity.X;
            rectangle.Y += (int)velocity.Y;
        }
        public void FrictionCount()
        {
            velocity.X = velocity.X - velocity.X * (float)0.1;
            velocity.Y = velocity.Y - velocity.Y * (float)0.08;
        }
        public void GravitySimulation()
        {
                velocity.Y++;
        }
        public void CheckEnviromentColision(TileMap map)
        {
            int i;
            List<Rectangle> mapObjects = map.GetMapObjectList();
            foreach (Rectangle obj in mapObjects)
            {

                //kolizje po X

                if (velocity.X >= 0)
                {
                    for (i = 0; i <= (int)velocity.X; i++)
                    {
                        if ((new Rectangle(rectangle.X + i, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(obj)))
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

                //kolizje po Y

                if (velocity.Y >= 0)
                {
                    for (i = 0; i <= (int)velocity.Y; i++)
                    {
                        if ((new Rectangle(rectangle.X, rectangle.Y + i, rectangle.Width, rectangle.Height).Intersects(obj)))
                        {
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

        public void UpdatePosition(Sprite sprite,TileMap map,List<MovableItem> movableItems)
        {
            if ((sprite.rectangle.Y < rectangle.Y) & (sprite.rectangle.Y + sprite.rectangle.Height > rectangle.Y))
            {
                velocity.X = sprite.velocity.X / 2;
                CheckEnviromentColision(map);
                CheckColisionWithOtherObjects(movableItems);
                rectangle.X += (int)velocity.X;
            }
            

        }
        private void CheckColisionWithOtherObjects(List<MovableItem> movableItems)
        {
            
            int i;
            foreach(MovableItem obj in movableItems)
            {
                //kolizje po Y
                if (!(obj == this))
                {
                    if (velocity.Y >= 0)
                    {
                        for (i = 0; i <= (int)velocity.Y; i++)
                        {
                            if ((new Rectangle(rectangle.X, rectangle.Y + i, rectangle.Width, rectangle.Height).Intersects(obj.rectangle)))
                            {

                                velocity.Y = i - 1;

                            }

                        }
                    }
                    else
                    {

                        for (i = -1; i >= (int)velocity.Y; i--)
                        {
                            if ((new Rectangle(rectangle.X, rectangle.Y + i, rectangle.Width, rectangle.Height).Intersects(obj.rectangle)))
                            {
                                velocity.Y = i + 1;
                            }
                        }
                    }

                    //kolizje po X

                    if (velocity.X >= 0)
                    {
                        for (i = 0; i <= (int)velocity.X; i++)
                        {
                            if ((new Rectangle(rectangle.X + i, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(obj.rectangle)))
                            {
                                velocity.X = i - 1;

                            }
                        }
                    }
                    else
                    {
                        for (i = -1; i >= (int)velocity.X; i--)
                        {
                            if ((new Rectangle(rectangle.X + i, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(obj.rectangle)))
                            {
                                velocity.X = i + 1;

                            }
                        }
                    }
                }
                
                

            }
            
        }
    }
    
}
