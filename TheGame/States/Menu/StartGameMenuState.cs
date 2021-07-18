using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;
using TheGame.SaveAndLoadControllers;
using TheGame.Sprites;
using TheGame.States.Levels;

namespace TheGame.States.Menu
{
    class StartGameMenuState:MenuState
    {
        public StartGameMenuState(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session) : base(game,graphics,content,session)
        {
            Initialize();
        }

        public override void Initialize()
        {
            Texture2D buttonTexture = content.Load<Texture2D>("gameUI/button");
            int y;
            y = (graphics.Viewport.Height / 10) * 3;


            Button newGameButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("New Game"));
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button loadGameButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle(graphics.Viewport.Width / 3, y, (graphics.Viewport.Width / 3), (graphics.Viewport.Height / 10)), new string("Load Game"));
            y += graphics.Viewport.Height / 30 + graphics.Viewport.Height / 10;

            Button backButton = new Button(buttonTexture, content.Load<SpriteFont>("Fonts/Basic"), new Rectangle((graphics.Viewport.Width / 10) * 9, (graphics.Viewport.Height / 10) * 9 - (graphics.Viewport.Height / 20), (graphics.Viewport.Width / 11), (graphics.Viewport.Height / 10)), new string("Back"));

            newGameButton.Click += NewGameButtonClick;
            loadGameButton.Click += LoadGameButtonClick;
            backButton.Click += backButtonClick;


            _components = new List<Component>()
            {
                newGameButton,
                loadGameButton,
                backButton
            };
            _buttons = new List<Button>()
            {
                newGameButton,
                loadGameButton,
                backButton
            };
            base.Initialize();

        }
        private void NewGameButtonClick(object sender, EventArgs e)
        {
            SessionData session = new SessionData();
            game.ChangeState(new Level0(game, graphics, content, session));
        }

        private void backButtonClick(object sender, EventArgs e)
        {
            game.ChangeState(new MainMenuState(game, graphics, content, null));
        }
        private void LoadGameButtonClick(object sender, EventArgs e)
        {
            SaveGameController sg = new SaveGameController();
            sg = sg.LoadGame();
            LevelFactory lv = new LevelFactory();
            SessionData session = new SessionData();
            session.SetPlayerLives(sg.playerLives);
            session.SetPlayerPoints(sg.PlayerPoints);
            Level level=lv.PickLevelById(sg.LevelId,game,graphics,content,session);
            level.player.rectangle = sg.playerData.possition;
            level.player.velocity = sg.playerData.velocity;
            for(int i = 0; i < level.sprites.Count; i++)
            {
                level.sprites[i].isAlive = sg.spritesData[i].isAlive;
            }
            for (int i = 0; i < level.items.Count; i++)
            {
                level.items[i].rectangle = sg.itemsData[i].rectangle;
                level.items[i].isActive = sg.itemsData[i].isActive;
            }
            for(int i = 0; i < level.movableItems.Count; i++)
            {
                level.movableItems[i].rectangle = sg.movablesData[i].rectangle;
            }
            level.spawnPoint = sg.spawnPoint;
            level.gameMaster.captions = sg.gameMasterData.captions;
            level.gameMaster.triggers = sg.gameMasterData.triggers;
            game.ChangeState(level);
        }
    }
}
