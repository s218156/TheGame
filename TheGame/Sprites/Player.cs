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
        
        public Player(Texture2D texture, Vector2 position,int lifes) : base(texture, position)
        {
            crouch = false;
            isOnLadder = false;
            this.lifes = lifes;
        }

        public override void Update(GameTime gameTime, Sprite player, TileMap map)
        {
            IsOnLadder(map);
            IsOnObstracles(map);
            GetMovementFormKeyboard();
            CrouchingInfluence();
            base.Update(gameTime, player,map);
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

        private void IsOnObstracles(TileMap map)
        {
            foreach(var tmp in map.GetObstracles())
            {
                if (rectangle.Intersects(tmp))
                {
                    lifes--;
                }
            }
        }
        private void GetMovementFormKeyboard()
        {
            var keyState = Keyboard.GetState();
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
                    rectangle.Y = rectangle.Y + 25;
                    rectangle.Height = rectangle.Height - 25;
                }
                else
                {
                    velocity.Y++;
                }

            }
            if (keyState.IsKeyUp(Keys.S)&crouch)
            {
                crouch = false;
                rectangle.Y = rectangle.Y - 25;
                rectangle.Height = rectangle.Height + 25;
            }
        }
    }
}
