using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Text;
using TheGame.Items;

namespace TheGame.Animations
{
    class SpringAnimation : AnimatedTexture
    {
        
        public SpringAnimation(Texture2D texture,Rectangle rectangle) : base(texture, rectangle, 2, 1)
        {
        }

        public void Update(GameTime gameTime, Spring spring)
        {
            if (spring.isPlayerOn)
                currentFrame = 1;
            else
                currentFrame = 0;
        }
    }
}
