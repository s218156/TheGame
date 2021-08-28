using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TheGame.Mics;
using TheGame.SaveAndLoadControllers;

namespace TheGame.States.Menu
{
    class PauseMenuState : MenuState
    {
        private State _previousState;
        private Level gameLevel;
        private bool isExitable;
        public PauseMenuState(Game1 game, GraphicsDevice graphics, ContentManager content,State previousState,SessionData session):base(game,graphics,content,session)
        {
            this._previousState = previousState;
            Initialize();
            isExitable = false;
        }

        public override void Initialize()
        {
            Texture2D buttonTexture = content.Load<Texture2D>("gameUI/button");
            int y;
            y = (graphics.Viewport.Height / 10) * 3;

            Button ReturnButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Return"));
            ReturnButton.Click += ReturnButtonClicked;
            _components.Add(ReturnButton);
            _buttons.Add(ReturnButton);
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button ResetButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Restart Level"));
            ResetButton.Click += ResetButtonClicked;
            _components.Add(ResetButton);
            _buttons.Add(ResetButton);

            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button SaveButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Save game"));
            SaveButton.Click += SaveGameButtonClicked;
            _components.Add(SaveButton);
            _buttons.Add(SaveButton);

            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button MainMenuButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Main Menu"));
            MainMenuButton.Click += MainMenuButtonClicked;
            _components.Add(MainMenuButton);
            _buttons.Add(MainMenuButton);

            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button ExitButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Quit Game"));
            ExitButton.Click += ExitButtonClicked;
            _components.Add(ExitButton);
            _buttons.Add(ExitButton);
            base.Initialize();

        }


        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyUp(Keys.Escape))
                isExitable = true;

            if ((Keyboard.GetState().IsKeyDown(Keys.Escape))&(isExitable))
            {
                game.ChangeState(_previousState);
                Thread.Sleep(200);
            }

            base.Update(gameTime);
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
            game.ChangeState(new MainMenuState(game, graphics, content,null));
        }

        public void SaveGameButtonClicked(object sender, EventArgs e)
        {
            _previousState.session.SaveData();
        }

       
    }
}
