using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Items;

namespace TheGame.Mics
{
    public class PhysicalObject
    {
        public float friction;
        private float baseFriction=(float)0.1;
        public Rectangle rectangle;
        public Vector2 velocity;
        protected Texture2D texture;
        protected bool floorColision;

        public PhysicalObject(Texture2D texture, Rectangle rectangle)
        {
            this.texture = texture;
            this.rectangle = rectangle;
            this.floorColision = false;
            friction = this.baseFriction;
            
        }
        public PhysicalObject(Texture2D texture,Vector2 position)
        {
            this.texture = texture;
            this.rectangle= new Rectangle((int)position.X, (int)position.Y, 100, 100);
            this.floorColision = false;
            friction = this.baseFriction;
        }

        protected void CheckColisionWithOtherObjects(List<MovableItem> movableItems)
        {
            int i;
            foreach (MovableItem obj in movableItems)
            {
                if (!(obj == this))
                    ColisionChecker(obj.rectangle);
            }
        }

        private void ColisionChecker(Rectangle obj)
        {

            //kolizje po X

            int i;
            if (velocity.X >= 0)
            {
                for (i = 0; i <= (int)velocity.X; i++)
                {
                    if ((new Rectangle(rectangle.X + i, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(obj)))
                        velocity.X = i - 1;
                    
                }
            }
            else
            {
                for (i = -1; i >= (int)velocity.X; i--)
                {
                    if ((new Rectangle(rectangle.X + i, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(obj)))
                        velocity.X = i + 1;
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
                        floorColision = true;
                    }
                }
            }
            else
            {

                for (i = -1; i >= (int)velocity.Y; i--)
                {
                    if ((new Rectangle(rectangle.X, rectangle.Y + i, rectangle.Width, rectangle.Height).Intersects(obj)))
                        velocity.Y = i + 1;  
                }
            }
        }


        public void CheckEnviromentColision(TileMap map)
        {
            floorColision = false;
            List<Rectangle> mapObjects = map.GetMapObjectList();
            foreach (Rectangle obj in mapObjects)
                ColisionChecker(obj);                
        }

        public void FrictionCount()
        {
            velocity.X = velocity.X - (velocity.X * friction);
            velocity.Y = velocity.Y - (velocity.Y * friction);
        }
        public void GravitySimulation()
        {
            velocity.Y++;
        }
            


        public void UpdatePosition()
        {
            rectangle.X += (int)velocity.X;
            rectangle.Y += (int)velocity.Y;
        }

        public void SetDefaultFriction()
        {
            friction = baseFriction;
        }

       
    }
}
