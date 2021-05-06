using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.States.Menu
{
    class MainMenuState : MenuState
    {


        

        public MainMenuState(Game1 game, GraphicsDevice graphics, ContentManager content,SessionData session) : base(game, graphics, content,session)
        {
            Initialize();
        }


        public override void Initialize()
        {
            Texture2D buttonTexture = content.Load<Texture2D>("gameUI/button");
            int y;
            y = (graphics.Viewport.Height / 10) * 2;


            Button newGameButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("New Game"));
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button loadGameButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Load Game"));
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button settingsButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Settings"));
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button aboutButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("About"));

            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;
            Button exitButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Quit Game"));

            newGameButton.Click += NewGameButtonClick;
            settingsButton.Click += SettingsButtonClick;
            exitButton.Click += ExitButtonClick;
            aboutButton.Click += AboutButtonClick;

            _components = new List<Component>()
            {
                newGameButton,
                loadGameButton,
                settingsButton,
                aboutButton,
                exitButton
            };
            _buttons = new List<Button>()
            {
                newGameButton,
                loadGameButton,
                settingsButton,
                aboutButton,
                exitButton
            };
            base.Initialize();
            
        }

        private void AboutButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new AboutMenuState(game, graphics, content,null));
        }

        private void ExitButtonClick(object sender, EventArgs e)
        {
            game.Exit();
        }

        private void SettingsButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new SettingsMenuState(game, graphics, content,null));
        }

        private void NewGameButtonClick(object sender, EventArgs e)
        {
            SessionData session = new SessionData();
            game.ChangeState(new Level0(game, graphics, content,session));
        }
    }
}
