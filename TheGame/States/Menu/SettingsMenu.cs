using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;

namespace TheGame.States.Menu
{
    
    class SettingsMenu : State
    {
        private List<Component> _components;
        public SettingsMenu(Game1 game, GraphicsDevice graphics, ContentManager content) : base(game, graphics, content)
        {
            _components = new List<Component>();



            //Dodawanie przycisków na dole ekranu
            int x = (graphics.Viewport.Width / 10) * 8;
            Button applyButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(x, (graphics.Viewport.Height / 10) * 9 - (graphics.Viewport.Height / 20), (graphics.Viewport.Width / 11), (graphics.Viewport.Height / 10)), new string("Apply"));
            x += graphics.Viewport.Width / 10 + graphics.Viewport.Height / 11;

            Button backButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle((graphics.Viewport.Width / 10)*9, (graphics.Viewport.Height/10)*9 -(graphics.Viewport.Height/20), (graphics.Viewport.Width / 11), (graphics.Viewport.Height / 10)), new string("Back"));

            backButton.Click += backButtonClick;
            _components.Add(applyButton);
            _components.Add(backButton);


        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach(var item in _components)
            {
                item.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var item in _components)
            {
                item.Update(gameTime);
            }
        }

        private void backButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new MainMenuState(game, graphics, content));
        }
    }
}
