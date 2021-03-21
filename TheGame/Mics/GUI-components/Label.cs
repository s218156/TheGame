using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics
{
    class Label : Component
    {
        private SpriteFont _font;
        public string caption;
        private Color PenColour;
        private Vector2 position;

        public Label(SpriteFont font, string caption, Vector2 position)
        {
            _font = font;
            this.caption = caption;
            this.position = position;
            this.PenColour = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, caption, position, PenColour);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
