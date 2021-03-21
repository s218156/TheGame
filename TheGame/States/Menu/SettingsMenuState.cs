﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.States.Menu
{
    
    class SettingsMenuState : State
    {
        private List<Component> _components;
        private Button resolutionButton, toogleFullScreenButton;
        private int height, width;
        private int[,] resolutionTable;
        private bool isFullScreen;
        
        public SettingsMenuState(Game1 game, GraphicsDevice graphics, ContentManager content) : base(game, graphics, content)
        {
            resolutionTable = new int[,] { { 640, 360 }, { 800, 450 }, { 1280, 720 }, { 1600, 900 }, { 1920, 1080 } };
            _components = new List<Component>();

            height = graphics.Viewport.Height;
            width = graphics.Viewport.Width;
            isFullScreen = graphics.PresentationParameters.IsFullScreen;
            Initialize();
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

        private void ResolutionButtonClick(object sender, EventArgs e)
        {
            int i = 0;
            while (width != resolutionTable[i, 0])
            {
                i++;
            }
            if (i+1 >= resolutionTable.Length/2)
            {
                i = -1;
            }
            width = resolutionTable[i + 1, 0];
            height = resolutionTable[i + 1, 1];
            resolutionButton.caption = new string("Resolution: " + width + " X " + height);
        }
        private void backButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new MainMenuState(game, graphics, content));
        }

        private void ApplyButtonClick(object sender, EventArgs e)
        {
            game.ChangeResolution(isFullScreen, height, width);
            game.ChangeState(new MainMenuState(game, graphics, content));
        }

        private void ToogleFullScreenButtonClicked(object sender, EventArgs e)
        {
            if (isFullScreen)
            {
                toogleFullScreenButton.caption = "Full Screen is OFF";
                isFullScreen = false;
            }
            else
            {
                toogleFullScreenButton.caption = "Full Screen is ON";
                isFullScreen = true;
            }
        }

        public override void Initialize()
        {
            //Dodawanie przycisków powrotu i zastosowania zmian
            int x = (graphics.Viewport.Width / 10) * 8;

            Button applyButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(x, (graphics.Viewport.Height / 10) * 9 - (graphics.Viewport.Height / 20), (graphics.Viewport.Width / 11), (graphics.Viewport.Height / 10)), new string("Apply"));
            x += graphics.Viewport.Width / 10 + graphics.Viewport.Height / 11;

            Button backButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle((graphics.Viewport.Width / 10) * 9, (graphics.Viewport.Height / 10) * 9 - (graphics.Viewport.Height / 20), (graphics.Viewport.Width / 11), (graphics.Viewport.Height / 10)), new string("Back"));

            //Dodawanie przycisków ustawień
            int y;
            y = (graphics.Viewport.Height / 10) * 2;

            resolutionButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 4 * 2, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Resolution: " + width + " X " + height));
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            toogleFullScreenButton = new Button(content.Load<Texture2D>("Textures/Misc/Button"), content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 4 * 2, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), null);
            if (isFullScreen)
            {
                toogleFullScreenButton.caption = "Full Screen is ON";
            }
            else
            {
                toogleFullScreenButton.caption = "Full Screen is OFF";
            }

            backButton.Click += backButtonClick;
            applyButton.Click += ApplyButtonClick;
            resolutionButton.Click += ResolutionButtonClick;
            toogleFullScreenButton.Click += ToogleFullScreenButtonClicked;


            _components.Add(resolutionButton);
            _components.Add(toogleFullScreenButton);
            _components.Add(applyButton);
            _components.Add(backButton);





        }
    }
}