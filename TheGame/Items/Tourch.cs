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
    class Tourch : Item
    {
        ItemAnimation animation;
        public Vector2 lightSource;
        public Tourch(Texture2D texture, Rectangle rectangle) : base(null,rectangle) 
        {
            animation = new ItemAnimation(texture, rectangle, 2, 1);
            lightSource = new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Draw(gameTime, spriteBatch);
        }
        public override void Update(GameTime gameTime, Player player, TileMap map)
        {
            animation.Update(gameTime, player);
            base.Update(gameTime, player, map);
        }
    }
}
