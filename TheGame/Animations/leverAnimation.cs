using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Items;
using TheGame.Sprites;

namespace TheGame.Animations
{
    class LeverAnimation:AnimatedTexture
    {
        public LeverAnimation(Texture2D texture, Rectangle rectangle) : base(texture, rectangle, 2, 1)
        {
        }
        public void Update(GameTime gameTime, Lever lever)
        {
            if (lever.isActive)
                currentFrame = 1;
            else
                currentFrame = 0;
        }
    }
}
