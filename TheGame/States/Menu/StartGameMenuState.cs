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

        private Level LoadDataFromSaveGameController(SaveGameController savedGame)
        {
            SessionData session = new SessionData();
            session.SetPlayerLives(savedGame.playerLives);
            session.SetPlayerPoints(savedGame.PlayerPoints);
            Level level;
            if (!savedGame.isSubLevel)
                level = LevelFactory.PickLevelById(savedGame.LevelId, game, graphics, content, session);
            else
                level = LevelFactory.PickSubLevelById(savedGame.LevelId, game, graphics, content, session, null);
            level.player.rectangle = savedGame.playerData.possition;
            for (int i = 0; i < level.sprites.Count; i++)
            {
                level.sprites[i].isAlive = savedGame.spritesData[i].isAlive;
            }
            for (int i = 0; i < level.items.Count; i++)
            {
                level.items[i].rectangle = savedGame.itemsData[i].rectangle;
                level.items[i].isActive = savedGame.itemsData[i].isActive;
            }
            for (int i = 0; i < level.movableItems.Count; i++)
            {
                level.movableItems[i].rectangle = savedGame.movablesData[i].rectangle;
            }
            level.spawnPoint = savedGame.spawnPoint;
            level.gameMaster.captions = savedGame.gameMasterData.captions;
            level.gameMaster.triggers = savedGame.gameMasterData.triggers;
            return level;
        }
        private Level LoadDataFromSaveGameController(SubLevelLevelData savedGame)
        {
            SessionData session = new SessionData();
            session.SetPlayerLives(savedGame.playerLives);
            session.SetPlayerPoints(savedGame.PlayerPoints);
            Level level;
            level = LevelFactory.PickSubLevelById(savedGame.LevelId, game, graphics, content, session, null);
            level.player.rectangle = savedGame.playerData.possition;
            for (int i = 0; i < level.sprites.Count; i++)
            {
                level.sprites[i].isAlive = savedGame.spritesData[i].isAlive;
            }
            for (int i = 0; i < level.items.Count; i++)
            {
                level.items[i].rectangle = savedGame.itemsData[i].rectangle;
                level.items[i].isActive = savedGame.itemsData[i].isActive;
            }
            for (int i = 0; i < level.movableItems.Count; i++)
            {
                level.movableItems[i].rectangle = savedGame.movablesData[i].rectangle;
            }
            level.spawnPoint = savedGame.spawnPoint;
            level.gameMaster.captions = savedGame.gameMasterData.captions;
            level.gameMaster.triggers = savedGame.gameMasterData.triggers;
            return level;
        }
        private void LoadGameButtonClick(object sender, EventArgs e)
        {

            SaveGameController sg = new SaveGameController();
            sg = sg.LoadGame();
            SessionData session = new SessionData();
            session.SetPlayerLives(sg.playerLives);
            session.SetPlayerPoints(sg.PlayerPoints);
            Level level = LoadDataFromSaveGameController(sg);

            if (sg.isSubLevel)
            {
                level.baseLevel = LoadDataFromSaveGameController(sg.baseLevelData);
            }
            else
            {
                int j = 0;
                for(int i = 0; i < level.sublevelTriggers.Count; i++)
                {
                    if(j <= sg.sublevelsData.Count - 1)
                    {
                        if (level.sublevelTriggers[i].rectangle == sg.sublevelsData[j].rectangle)
                        {
                            level.sublevelTriggers[i].sublevel = LoadDataFromSaveGameController(sg.sublevelsData[j].sublevel);
                            level.sublevelTriggers[i].sublevel.baseLevel = level;
                            level.sublevelTriggers[i].wasWisited = true;
                            j++;
                        }
                    } 
                }
            }
            game.ChangeState(level);

        }
    }
}
