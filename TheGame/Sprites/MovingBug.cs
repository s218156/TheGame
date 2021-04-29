using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Items;
using TheGame.Mics;

namespace TheGame.Sprites
{
    public class MovingBug:Sprite
    {
        private bool direction;         //false-to the right, true-to the left
        private Vector2 startPosition;
        private int range;
        private int movingSpeed;
        public MovingBug(Texture2D texture,Vector2 position,Texture2D deathTexture, int range, int movingSpeed) : base(position, deathTexture)
        {
            lifePoints = 5;
            hitPoints = 10;
            startPosition = position;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, 64, 64);
            this.animatedTexture = new BasicSpriteAnimation(texture, rectangle);
            direction = true;
            this.range = range;
            this.movingSpeed = movingSpeed;
        }
        public override void Update(GameTime gameTime, Player player, TileMap map,List<MovableItem>movableList)
        {
            if (isAlive)
            {
                if (direction)
                {
                    velocity.X-=movingSpeed;
                    if (rectangle.X < startPosition.X - range)
                        direction = false;
                    
                }
                else
                {
                    velocity.X+=movingSpeed;
                    if (rectangle.X > startPosition.X + range)
                        direction = true;
                    
                }
                CheckColisionWithPlayer(player);
            }
            base.Update(gameTime, player, map,movableList);

        }
        private void CheckColisionWithPlayer(Player player)
        {
            if (rectangle.Intersects(player.rectangle))
                player.ColisionWithMovingBug(this, velocity, hitPoints);
            
        }
        public void AttackedByPlayer()
        {
            lifePoints = 0;
        }
    }
}
