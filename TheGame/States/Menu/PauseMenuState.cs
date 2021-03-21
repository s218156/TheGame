using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;

namespace TheGame.States.Menu
{
    class PauseMenuState : State
    {
        private State _previousState;
        private List<Component> _components;
        public PauseMenuState(Game1 game, GraphicsDevice graphics, ContentManager content,State previousState):base(game,graphics,content)
        {
            this._previousState = previousState;
            Initialize();
        }

        public override void Initialize()
        {
            _components = new List<Component>();
            int y;
            y = (graphics.Viewport.Height / 10) * 3;

            Button ReturnButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Return"));
            ReturnButton.Click += ReturnButtonClicked;
            _components.Add(ReturnButton);
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button ResetButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Restart Level"));
            ResetButton.Click += ResetButtonClicked;
            _components.Add(ResetButton);
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button SaveButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Save game"));
            _components.Add(SaveButton);
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button MainMenuButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Main Menu"));
            MainMenuButton.Click += MainMenuButtonClicked;
            _components.Add(MainMenuButton);
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button ExitButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Quit Game"));
            ExitButton.Click += ExitButtonClicked;
            _components.Add(ExitButton);
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
            foreach(var item in _components)
            {
                item.Update(gameTime);
            }
        }

        public void ReturnButtonClicked(object sender, EventArgs e)
        {
            game.ChangeState(_previousState);
        }

        public void ResetButtonClicked(object sender, EventArgs e)
        {
            _previousState.Initialize();
            game.ChangeState(_previousState);
        }

        public void ExitButtonClicked(object sender, EventArgs e)
        {
            game.Exit();
        }

        public void MainMenuButtonClicked(object sender, EventArgs e)
        {
            game.ChangeState(new MainMenuState(game, graphics, content));
        }

       
    }
}
