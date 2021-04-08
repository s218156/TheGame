using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.Items
{
    
    public class MovableItem : PhysicalObject
    {
        public MovableItem(Texture2D texture,Rectangle rectangle) : base(texture, rectangle)
        {

        }
        public void Draw(GameTime gameTIme, SpriteBatch spriteBatch)
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
        
    }
    
}
