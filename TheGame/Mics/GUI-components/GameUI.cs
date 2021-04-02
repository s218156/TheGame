using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Sprites;

namespace TheGame.Mics.GUI_components
{
    class GameUI
    {
        private Rectangle boxRectangle;
        private Texture2D boxTexture;
        private Texture2D lifeTexture;
        private Texture2D coinTexture;

        private SpriteFont font;


        public GameUI(ContentManager content)
        {
            lifeTexture = content.Load<Texture2D>("gameUI/heart");
            coinTexture = content.Load<Texture2D>("gameUI/coin");
            boxTexture = content.Load<Texture2D>("gameUI/frame");
            boxRectangle = new Rectangle(0, 0, 300, 100);
            this.font = content.Load<SpriteFont>("Fonts/Basic");
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch,SessionData session)
        {
            spriteBatch.Draw(boxTexture, boxRectangle, Color.White);
            spriteBatch.Draw(coinTexture,new Rectangle(boxRectangle.X+25, boxRectangle.Y+25, 25, 25),Color.White);
            spriteBatch.DrawString(font, session.GetPlayerPoints().ToString(), new Vector2(boxRectangle.X + 60, boxRectangle.Y + 25),Color.Goldenrod);

            for(int i = 0; i < session.GetPlayerLives(); i++)
            {
                spriteBatch.Draw(lifeTexture, new Rectangle(boxRectangle.X + 25 + i * 25, boxRectangle.Y + 50, 25, 25), Color.White);
            }
            
        }
        public void Update(GameTime gameTime)
        {
            boxRectangle = new Rectangle(0,0,300,100);
        }

    }
}
