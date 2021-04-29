using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics
{
    class Button : Component
    {
        private MouseState _currentMouse;

        private SpriteFont _font;

        private bool _isHovering;


        private MouseState _previousMouse;

        private Texture2D _texture;

        public bool isAbleToClick;

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Rectangle rectangle;

        public string caption;



        public Button(Texture2D texture, SpriteFont font, Rectangle rectangle, String caption)
        {
            _texture = texture;
            _font = font;
            PenColour = Color.Black;
            this.rectangle = rectangle;
            this.caption = caption;
            this.isAbleToClick = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (_isHovering)
                colour = Color.Gray;
            if (!isAbleToClick)
            {
                colour = Color.DarkGray;
                PenColour = Color.Gray;
            }
                

            spriteBatch.Draw(_texture, rectangle, colour);

            if (!string.IsNullOrEmpty(caption))
            {
                var x = (rectangle.X + (rectangle.Width / 2)) - (_font.MeasureString(caption).X / 2);
                var y = (rectangle.Y + (rectangle.Height / 2)) - (_font.MeasureString(caption).Y / 2);

                spriteBatch.DrawString(_font, caption, new Vector2(x, y), PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    Click?.Invoke(this, new EventArgs());
            }
        }

    }
}
