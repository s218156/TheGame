using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using System.Text;
using TheGame.Animations;
using TheGame.Inventory;
using TheGame.Items;
using TheGame.Mics;

namespace TheGame.Sprites
{
    public class Player:Sprite
    {
        public int points;
        public int lifes;
        public int jumpHeight;
        private List<InventoryItem> inventory;
        
        public Player(Texture2D texture, Vector2 position,Texture2D deathTexture, int lifes) : base(position, deathTexture)
        {
            inventory = new List<InventoryItem>();
            this.animatedTexture = new CharacterAnimation(texture, rectangle);
            crouch = false;
            isOnLadder = false;
            this.lifes = lifes;
            hitPoints = 20;
            jumpHeight = 30;
        }

        public override void Update(GameTime gameTime, Player player, TileMap map,List<MovableItem>movableList)
        {
            if (isAlive)
            {
                IsOnLadder(map);
                GetMovementFormKeyboard(map);
                CrouchingInfluence();
            }
            else
            {
                if (deathTime >= 80)
                    lifes--;
            }
            base.Update(gameTime, player, map,movableList);
            foreach (InventoryItem item in inventory)
                item.Update(gameTime, this);
            
        }

        private void CrouchingInfluence()
        {
            if (crouch&floorColision)
                velocity.X = velocity.X - (int)velocity.X / 4;                
        }

        private void IsOnLadder(TileMap map)
        {
            isOnLadder = false;
            foreach(Rectangle tmp in map.GetLadders())
            {
                if (rectangle.Intersects(tmp))
                    isOnLadder = true;
            }
        }

        public void ColisionWithMovingBug(MovingBug bug, Vector2 bugVelocity, int bugHitPoints)
        {
            if (isAlive)
            {
                if (velocity.Y > 0)
                    bug.AttackedByPlayer();
                else
                {
                    lifePoints -= bugHitPoints;
                    velocity.X = bugVelocity.X * 2;
                    velocity.Y = -25;
                }
            }
        }


        public void UpdateInventoryList(InventoryItem item)
        {
            inventory.Add(item);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isOnLadder)
            {
                foreach (InventoryItem item in inventory)
                {
                    if (item.drawableOnTop)
                        item.Draw(gameTime, spriteBatch);
                }
                base.Draw(gameTime, spriteBatch);
                foreach (InventoryItem item in inventory)
                {
                    if(!item.drawableOnTop)
                        item.Draw(gameTime, spriteBatch);
                }
            }
            else
            {
                foreach (InventoryItem item in inventory)
                {
                    if(!item.drawableOnTop)
                        item.Draw(gameTime, spriteBatch);
                }
                base.Draw(gameTime, spriteBatch);
                foreach (InventoryItem item in inventory)
                {
                    if (item.drawableOnTop)
                        item.Draw(gameTime, spriteBatch);
                }
            } 
        }


        private void GetMovementFormKeyboard(TileMap map)
        {
            var keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Keys.LeftControl))
                attacking = 10;

            if ((keyState.IsKeyDown(Keys.W)) & isOnLadder)
                velocity.Y--;

            if (keyState.IsKeyDown(Keys.D))
                velocity.X++;
            
            if (keyState.IsKeyDown(Keys.A))
                velocity.X--;
            
            if ((keyState.IsKeyDown(Keys.Space)) & (floorColision))
            {
                velocity.Y = -1*jumpHeight;
                jump = true;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                if (floorColision&!crouch)
                {
                    crouch = true;
                    rectangle.Y = rectangle.Y + 35;
                    rectangle.Height = rectangle.Height - 35;
                }
                else
                    velocity.Y++;
                
            }
            if (keyState.IsKeyUp(Keys.S)&crouch)
            {
                Rectangle newRectangle = rectangle;
                newRectangle.Height = rectangle.Height + 35;
                newRectangle.Y-=35;
                bool isColiding = false;
                foreach(var obj in map.GetMapObjectList())
                {
                    if (newRectangle.Intersects(obj)){
                        isColiding = true;
                    }
                }
                if (!isColiding)
                {
                    crouch = false;
                    rectangle.Y = rectangle.Y - 35;
                    rectangle.Height = rectangle.Height + 35;
                } 
            }
        }

    }
}