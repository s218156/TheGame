using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics
{
    class GameController
    {
        public bool isEnterDown()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Enter);
        }
    }
}
