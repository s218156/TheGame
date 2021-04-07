﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Items;
using TheGame.Mics;

namespace TheGame.Sprites
{
    public abstract class Sprite
    {
        protected bool isOnLadder;
        protected bool floorColision;
        protected bool jump;
        public Rectangle rectangle;
        public BasicSpriteAnimation texture;
        public Vector2 velocity;
        protected int lifePoints;
        protected bool isAlive;
        public int attacking;
        public int hitPoints , deathTime;
        private ItemAnimation deathAnimation;
        public Sprite(Vector2 position,Texture2D deathTexture)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, 100, 100);
            velocity = Vector2.Zero;
            floorColision = false;
            jump = false;
            lifePoints = 100;
            isAlive = true;
            attacking = 0;
            deathTime = 0;
            deathAnimation = new ItemAnimation(deathTexture, rectangle, 4, 2);
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            texture.Draw(gameTime, spriteBatch);
            if(!(isAlive))
            {
                if (deathTime < 25)
                {
                    deathAnimation.Draw(gameTime, spriteBatch);
                }
            }
            
        }
        public virtual void Update(GameTime gameTime,Player player, TileMap map,List<MovableItem> movableList)
        {
            if ((isAlive)&(deathTime<=80))
            {
                FrictionCount();
                GravitySimulation();
                IsOnObstracles(map);
                CheckEnviromentColision(map);
                CheckColisionWithMovables(movableList,map);
                rectangle.X += (int)velocity.X;
                rectangle.Y += (int)velocity.Y;
                
                if (attacking > 0)
                {
                    attacking--;
                }

                if (lifePoints <= 0)
                {
                    isAlive = false;
                    deathAnimation.UpdateRectangle(rectangle);
                    velocity = Vector2.Zero;
                }
            }
            else
            {
                deathTime++;
                deathAnimation.Update(gameTime);
            }
            texture.Update(gameTime, isAlive, velocity, rectangle);
        }

        public void IsUnderAttack(Sprite enemy)
        {
            if (rectangle.Intersects(enemy.rectangle))
            {
                if (enemy.attacking == 5)
                {
                    lifePoints -= enemy.hitPoints;
                }
            }
        }

        private void IsOnObstracles(TileMap map)
        {
            foreach (var tmp in map.GetObstracles())
            {
                if (rectangle.Intersects(tmp))
                {
                    lifePoints -= 100;
                }
            }
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
                //if (!(velocity.X == 0))
                //{
                    if (velocity.X >= 0)
                    {
                        for (i = 0; i <= (int)velocity.X; i++)
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
                //}



                //kolizje po Y
                //if (!(velocity.Y == 0))
                //{
                    if (velocity.Y >= 0)
                    {
                        for (i = 0; i <= (int)velocity.Y; i++)
                        {
                            if ((new Rectangle(rectangle.X, rectangle.Y + i, rectangle.Width , rectangle.Height).Intersects(obj)))
                            {
                                floorColision = true;
                                jump = false;
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
                //}
                    
                
                


            }
        }

       
        public void CheckColisionWithMovables(List<MovableItem> items,TileMap map)
        {
            int i;
            foreach (MovableItem obj in items)
            {
                if (new Rectangle(rectangle.X+(int)velocity.X,rectangle.Y,rectangle.Width,rectangle.Height).Intersects(obj.rectangle))
                {
                    obj.UpdatePosition(this, map,items);
                    
                }

                //kolizje po Y
                
                if (velocity.Y >= 0)
                {
                    for (i = 0; i <= (int)velocity.Y; i++)
                    {
                        if ((new Rectangle(rectangle.X, rectangle.Y + i, rectangle.Width, rectangle.Height).Intersects(obj.rectangle)))
                        {
                            floorColision = true;
                            jump = false;
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