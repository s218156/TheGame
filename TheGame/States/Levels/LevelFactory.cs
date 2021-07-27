using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;
using TheGame.States.Levels.Sublevels;

namespace TheGame.States.Levels
{
    public static class LevelFactory
    {
       

        public static Level PickLevelById(int id, Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session)
        {
            switch (id)
            {
                case 0:
                    return new Level0(game, graphics, content, session);
                    break;
                case 1:
                    return new Level1(game, graphics, content, session);
                    break;
                case 2:
                    return new Level2(game, graphics, content, session);
                    break;
                case 3:
                    return new Level3(game, graphics, content, session);
                    break;
                case 4:
                    return new Level4(game, graphics, content, session);
                default:
                    return null;
                    break;
            }
        }
        public static Sublevel PickSubLevelById(int id, Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session,Level baseLevel)
        {
            switch (id)
            {
                case 1001:
                    return new SubLevel1(game, graphics, content, session, baseLevel);
                    break;
                default:
                    return null;
                    break;
            }
        }

    }
}
