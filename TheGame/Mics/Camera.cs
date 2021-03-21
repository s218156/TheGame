using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.Mics
{
    class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Sprite target)
        {
            var position = Matrix.CreateTranslation(
              -target.rectangle.X - (target.rectangle.Width),

            -target.rectangle.Y - (target.rectangle.Height),
              0);

            var offset = Matrix.CreateTranslation(
                Game1.screenWidth / 2,

                Game1.screenHeight / 2 + Game1.screenHeight / 5,
                0);

            Transform = position * offset;
        }
    }
}
