using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;

namespace TheGame.States.Levels
{
    public class LevelFactory
    {
        public LevelFactory()
        {

        }

        public Level PickLevelById(int id, Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session)
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

    }
}
