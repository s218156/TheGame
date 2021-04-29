using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Items;
using TheGame.Mics;

namespace TheGame.Sprites
{
    class GhostSprite : Sprite
    {
        private int type;
        private GraphicsDevice graphics;
        private bool moveToLeft, moveToRight;
        private List<Rectangle> _steps;
        public List<Vector2> velocitieSteps;
        public GhostSprite(int type, GraphicsDevice graphics) : base(new Microsoft.Xna.Framework.Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2),null)
        {
            this.type = type;
            this.graphics = graphics;
            moveToRight = false;
            moveToLeft = false;
            if (type == 1)
                moveToRight = true;
        }
        public GhostSprite(Sprite player):base(new Vector2(player.rectangle.X,player.rectangle.Y),null)
        {
            this.type = 0;
            _steps = new List<Rectangle>();
            velocitieSteps = new List<Vector2>();
            for(int i = 0; i < 5; i++)
            {
                _steps.Add(player.rectangle);
                velocitieSteps.Add(player.velocity);
            }
        }

        public override void Update(GameTime gameTime, Player player, TileMap map,List<MovableItem>movableList)
        {
            if (type == 0)
            {
                rectangle = _steps[0];
                _steps.RemoveAt(0);
                _steps.Add(player.rectangle);

                velocity = velocitieSteps[0];
                velocitieSteps.RemoveAt(0);
                velocitieSteps.Add(player.velocity);
                

            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
           
        }

    }
}
