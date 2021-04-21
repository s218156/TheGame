using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.SoundControllers;
using TheGame.Sprites;

namespace TheGame.Items
{
    public class Coin:Item
    {
        int value;
        bool isActive;
        private ItemAnimation animation;
        CoinSoundController sound;
        public Coin(Texture2D texture, Rectangle rectangle, int value,CoinSoundController sound) : base(texture, rectangle)
        {
            this.value = value;
            this.isActive = true;
            this.animation = new ItemAnimation(texture, rectangle,4,1);
            this.sound = sound;
        }

        public override void Draw(GameTime gameTIme, SpriteBatch spriteBatch)
        {
            if (isActive)
                animation.Draw(gameTIme, spriteBatch);
            
        }

        public override void Update(GameTime gameTime, Player player)
        {
            animation.Update(gameTime,null);
            if ((rectangle.Intersects(player.rectangle))&(isActive))
            {
                sound.PlaySound();
                isActive = false;
                player.points += value;
            }

        }
    }
}
