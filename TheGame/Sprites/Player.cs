using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using System.Text;
using TheGame.Mics;

namespace TheGame.Sprites
{
    public class Player:Sprite
    {
        public int points;
        private bool crouch;
        public int lifes;
        
        public Player(Texture2D texture, Vector2 position,Texture2D deathTexture, int lifes) : base(texture, position, deathTexture)
        {
            crouch = false;
            isOnLadder = false;
            this.lifes = lifes;
            this.hitPoints = 20;
        }

        public override void Update(GameTime gameTime, Sprite player, TileMap map)
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
                {
                    lifes--;
                }
                
            }
            base.Update(gameTime, player, map);
        }

        private void CrouchingInfluence()
        {
            if (crouch&floorColision)
            {
                velocity.X = velocity.X - (int)velocity.X / 2;                
            }
        }

        private void IsOnLadder(TileMap map)
        {
            isOnLadder = false;
            foreach(Rectangle tmp in map.GetLadders())
            {
                if (rectangle.Intersects(tmp))
                {
                    isOnLadder = true;
                }
            }
        }

        

        private void GetMovementFormKeyboard(TileMap map)
        {
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.LeftControl))
            {
                attacking = 10;
            }

            if ((keyState.IsKeyDown(Keys.W)) & isOnLadder)
            {
                velocity.Y--;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity.X++;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                velocity.X--;
            }
            if ((keyState.IsKeyDown(Keys.Space)) & (floorColision))
            {
                velocity.Y = -30;
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
                {
                    velocity.Y++;
                }
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