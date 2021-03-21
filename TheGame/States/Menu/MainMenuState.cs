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
    class MainMenuState : State
    {


        private List<Component> _components;

        public MainMenuState(Game1 game, GraphicsDevice graphics, ContentManager content) : base(game, graphics, content)
        {
            Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button item in _components){
                item.Update(gameTime);

                //na tem moment na sztywno
                if(item.caption=="Load Game")
                {
                    item.isAbleToClick = false;
                }


                
            }
        }

        public override void Initialize()
        {
            int y;
            y = (graphics.Viewport.Height / 10) * 2;


            Button newGameButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("New Game"));
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button loadGameButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Load Game"));
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button settingsButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Settings"));
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button aboutButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("About"));

            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;
            Button exitButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Quit Game"));

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
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var item in _components)
            {
                item.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        private void AboutButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new AboutMenuState(game, graphics, content));
        }

        private void ExitButtonClick(object sender, EventArgs e)
        {
            game.Exit();
        }

        private void SettingsButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new SettingsMenuState(game, graphics, content));
        }

        private void NewGameButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new Level1(game, graphics, content));
        }

        
    }
}
