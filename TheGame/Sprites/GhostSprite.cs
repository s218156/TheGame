using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;

namespace TheGame.Sprites
{
    class GhostSprite : Sprite
    {
        private int type;
        private GraphicsDevice graphics;
        private bool moveToLeft, moveToRight;
        private List<Rectangle> _steps;
        public GhostSprite(int type, GraphicsDevice graphics) : base(null, new Microsoft.Xna.Framework.Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2))
        {
            this.type = type;
            this.graphics = graphics;
            moveToRight = false;
            moveToLeft = false;
            if (type == 1)
            {
                moveToRight = true;
            }
        }
        public GhostSprite(Sprite player):base(null,new Vector2(player.rectangle.X,player.rectangle.Y))
        {
            this.type = 0;
            _steps = new List<Rectangle>();
            for(int i = 0; i < 5; i++)
            {
                _steps.Add(player.rectangle);
            }
        }

        public override void Update(GameTime gameTime, Sprite player, TileMap map)
        {
            if (type == 1)
            {
                if(rectangle.X==graphics.Viewport.Width/4 *2 )
                {
                    moveToRight = false;
                    moveToLeft = true;
                }
                if (rectangle.X == graphics.Viewport.Width / 4)
                {
                    moveToRight = true;
                    moveToLeft = false;
                }
                if (moveToRight)
                {
                    rectangle.X++;
                }
                if (moveToLeft)
                {
                    rectangle.X--;
                }

            }
            if (type == 0)
            {
                rectangle = _steps[0];
                _steps.RemoveAt(0);
                _steps.Add(player.rectangle);
                

            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
           
        }

    }
}
