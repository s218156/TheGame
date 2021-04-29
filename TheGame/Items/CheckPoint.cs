using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Items;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.Content.Items
{
    class CheckPoint:Item
    {
        public bool isChecked;
        public bool wasChecked;
        private ItemAnimation animation; 
        public CheckPoint(Rectangle rectangle,Texture2D texture, Texture2D markedTexture):base(texture,rectangle)
        {
            this.animation = new ItemAnimation(markedTexture, rectangle, 2, 1);
            this.rectangle = rectangle;
            this.texture = texture;
            isChecked = false;
            wasChecked = false;

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isChecked)
                animation.Draw(gameTime, spriteBatch);
            else
                spriteBatch.Draw(texture, rectangle, Color.White);
        }
        public override void Update(GameTime gameTime, Player player, TileMap map)
        {
            wasChecked = isChecked;
            if (isChecked)
                animation.Update(gameTime, null);

            if (rectangle.Intersects(player.rectangle))
                isChecked = true;
        }
    }
}
