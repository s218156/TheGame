using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.Mics
{
    public class Camera
    {
        private float _zoom { get; set; }
        public Matrix Transform { get; private set; }
        private Vector2 _previousVelocity;
        public Camera()
        {
            _zoom = 1;
            _previousVelocity = Vector2.Zero;
        }

        public float GetZoom()
        {
            return _zoom;
        }

        public void Follow(Sprite target,bool isZoomOn)
        {
            var position = Matrix.CreateTranslation(-target.rectangle.X - (target.rectangle.Width),-target.rectangle.Y - (target.rectangle.Height),0);
            var offset = Matrix.CreateTranslation( Game1.screenWidth / 2, Game1.screenHeight / 2 + Game1.screenHeight / 5,1);
            if (isZoomOn)
            {
                if (Math.Abs(target.velocity.X) < Math.Abs(_previousVelocity.X))
                {
                    if (_zoom < (float)0.99)
                        _zoom += (float)0.005;
                    else
                        _zoom = 1;
                }
                else
                {
                    if (Math.Abs(target.velocity.X) > 0.5)
                        if (_zoom > (float)0.8)
                            _zoom -= (float)0.001;
                }
            }
            else
            {
                _zoom = 1;
            }
            

            Transform = position * Matrix.CreateScale(_zoom) * offset ;

            _previousVelocity = target.velocity;
         }
    }
}
