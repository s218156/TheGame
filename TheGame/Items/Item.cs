using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.Items
{
    public abstract class Item
    {
        public Rectangle rectangle;
        public Texture2D texture;

        public Item(Texture2D texture, Rectangle rectangle)
        {
            this.texture = texture;
            this.rectangle = rectangle;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public virtual void Update(GameTime gameTime, Player player, TileMap map) 
        {

        }

    }
}
