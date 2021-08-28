using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TheGame.Mics;
using TheGame.SaveAndLoadControllers;
using TheGame.States.Levels;

namespace TheGame.States
{
    class LoadingScreen:State
    {
        private bool levelInitialized;
        Level level;
        Thread loadingThread;
        public LoadingScreen(Game1 game, GraphicsDevice graphics,ContentManager content, SessionData session, int nextLevel,bool shouldLoad) : base(game,graphics,content,session)
        {
            levelInitialized = false;
            level = LevelFactory.PickLevelById(nextLevel, game, graphics, content, session);
            if (!shouldLoad)
                loadingThread = new Thread(level.prepareLevel);
            else
                loadingThread = new Thread(LoadGame);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Green);
        }

        public override void Initialize()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if ((!levelInitialized)&&(!loadingThread.IsAlive)){
                levelInitialized = true;
                loadingThread.Start();
            }
            if ((levelInitialized) && (!loadingThread.IsAlive))
            {
                game.ChangeState(level);
            }

        }
        public void LoadGame()
        {
            SaveGameController sg = new SaveGameController();
            sg = sg.LoadGame();
            SessionData session = new SessionData();
            session.SetPlayerLives(sg.playerLives);
            session.SetPlayerPoints(sg.PlayerPoints);
            level = LoadDataFromSaveGameController(sg);

            if (sg.isSubLevel)
            {
                level.baseLevel = LoadDataFromSaveGameController(sg.baseLevelData);
            }
            else
            {
                int j = 0;
                for (int i = 0; i < level.sublevelTriggers.Count; i++)
                {
                    if (j <= sg.sublevelsData.Count - 1)
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
            level.prepareLevel();
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

    }
}
