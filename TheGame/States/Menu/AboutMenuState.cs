using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TheGame.Mics;

namespace TheGame.States.Menu
{
    class AboutMenuState : MenuState
    {
        protected List<Label> labels;
        private Button logo;
        public AboutMenuState(Game1 game, GraphicsDevice graphics, ContentManager content,SessionData session) : base(game, graphics, content,session)
        {
            Initialize();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Begin();
            foreach(var tmp in labels)
            {
                tmp.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Initialize()
        {
            _buttons = new List<Button>();
            Button backButton = new Button(content.Load<Texture2D>("gameUI/button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle((graphics.Viewport.Width / 10) * 9, (graphics.Viewport.Height / 10) * 9 - (graphics.Viewport.Height / 20), (graphics.Viewport.Width / 11), (graphics.Viewport.Height / 10)), "Back");
            backButton.Click += backButtonClick;
            _buttons.Add(backButton);
            _components.Add(backButton);
            labels = new List<Label>();
            labels.Add(new Label(content.Load<SpriteFont>("Fonts/Basic"), "TheGame - project made for university purpose",new Vector2(graphics.Viewport.Width/2-300,100)));
            labels.Add(new Label(content.Load<SpriteFont>("Fonts/Basic"), "Project made by: Michal Matusiak, 218156", new Vector2(graphics.Viewport.Width / 2 - 300, 200)));
            labels.Add(new Label(content.Load<SpriteFont>("Fonts/Basic"), "Graphics and sounds from:", new Vector2(graphics.Viewport.Width / 2 - 300, 300)));
            logo = new Button(content.Load<Texture2D>("Fonts/logo_kga1"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 2 - 300, 350, 400, 400), " ");
            labels.Add(new Label(content.Load<SpriteFont>("Fonts/Basic"), "!!!SPOILER ALERT!!! THIS GAME IS NOT BUG-FREE", new Vector2(graphics.Viewport.Width / 2 - 300, 800)));
            _components.Add(logo);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            logo.unHoverButton();
        }

        private void backButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new MainMenuState(game, graphics, content, null));
        }
    }
}
