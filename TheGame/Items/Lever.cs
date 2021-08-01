using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.Items
{
    public class Lever : Item
    {
        private Rectangle colisionRectangle;
        private LeverAnimation animation;
        private List<Platform> platforms;
        public Lever(Texture2D texture,Rectangle rectangle, TileMap map,List<Platform> platforms) : base(null, rectangle)
        {
            isActive = false;
            colisionRectangle = new Rectangle(rectangle.X + (rectangle.Width / 10), rectangle.Y + 3 * (rectangle.Height / 4), 8 * (rectangle.Width / 10), rectangle.Height / 4);
            animation = new LeverAnimation(texture, rectangle);
            map.mapObjects.Add(colisionRectangle);
            this.platforms = platforms;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Draw(gameTime, spriteBatch);
            foreach (Platform tmp in platforms)
                tmp.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime, Player player, TileMap map)
        {
            if ((rectangle.Intersects(player.rectangle))&(Keyboard.GetState().IsKeyDown(Keys.F))){
                if (player.velocity.X != 0)
                {
                    if (player.velocity.X > 0)
                        isActive = true;
                    else
                        isActive = false;
                }  
            }

            foreach (Platform tmp in platforms)
                tmp.Update(this, map);
            animation.Update(gameTime, this);
        }
    }
}
