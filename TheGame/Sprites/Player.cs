using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using System.Text;
using TheGame.Mics;

namespace TheGame.Sprites
{
    class Player:Sprite
    {
        private bool crouch;
        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            crouch = false;
        }

        public override void Update(GameTime gameTime, Sprite player, TileMap map)
        {
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
        private void GetMovementFormKeyboard()
        {
            var keyState = Keyboard.GetState();

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
