using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.Items
{
    public class Spring : Item
    {
        public bool isPlayerOn;
        int timer;
        Rectangle colisionRectangle;
        SpringAnimation animation;
        public Spring(Texture2D texture, Rectangle rectangle) : base(null, rectangle)
        {
            isPlayerOn = false;
            timer = 0;
            colisionRectangle = new Rectangle(rectangle.X, rectangle.Y + rectangle.Height / 2, rectangle.Width, rectangle.Height / 2);
            animation = new SpringAnimation(texture, rectangle);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Draw(gameTime, spriteBatch);
        }
        public override void Update(GameTime gameTime, Player player, TileMap map)
        {
            if (!map.mapObjects.Contains(colisionRectangle))
                map.mapObjects.Add(colisionRectangle);
            if (rectangle.Intersects(player.rectangle)){
                isPlayerOn = true;
            }
            else
            {
                isPlayerOn = false;
                timer = 0;
            }
            if (isPlayerOn)
                timer++;
            if(timer > 10)
                player.velocity.Y -= 60;
            animation.Update(gameTime, this);
        }
    }
}
