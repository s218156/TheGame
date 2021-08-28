using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TheGame.Items;
using TheGame.Mics;

namespace TheGame.Sprites
{
    class Enemy:Sprite
    {
        public Enemy(Texture2D texture, Vector2 position,Texture2D deathTexture) : base(position,deathTexture)
        {
            hitPoints = 10;
        }

        public override void Update(GameTime gameTime, Player player, TileMap map,List<MovableItem> movableList, List<WaterArea> waterAreas)
        {
            MoveTowardsPlayer(player);
            CheckFightCondition(player);
            base.Update(gameTime, player, map,movableList,waterAreas);
        }
        private void MoveTowardsPlayer(Sprite player)
        {
            if ((player.rectangle.X - rectangle.X < 300)&(rectangle.X - player.rectangle.X < 300))
            {
                if((player.rectangle.X - rectangle.X < 300)&(player.rectangle.X - rectangle.X >0)){
                    velocity.X++;
                }

                if ((rectangle.X - player.rectangle.X < 300) & (rectangle.X - player.rectangle.X > 0))
                {
                    velocity.X--;
                }
                if (rectangle.Intersects(player.rectangle))
                {
                    velocity.X = 0;
                }
            }
        }
        public void CheckFightCondition(Sprite player)
        {
            if (rectangle.Intersects(player.rectangle))
            {
                if (attacking == 0)
                {
                    attacking = 10;
                }
                player.IsUnderAttack(this);
                if (player.attacking == 5)
                {
                    lifePoints -= player.hitPoints;
                }
            }
        }
    }
}
