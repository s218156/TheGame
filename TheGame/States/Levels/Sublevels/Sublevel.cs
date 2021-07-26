using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;

namespace TheGame.States.Levels.Sublevels
{
    public abstract class Sublevel: Level
    {
        public Sublevel(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session, int levelId, Level baseLevel) : base(game, graphics, content, session, levelId, -1)
        {
            this.baseLevel = baseLevel;
        }

        


        
        public override void CheckEndLevel()
        {
            if (player.rectangle.Intersects(EndPoint))
            {
                baseLevel.session = this.session;
                game.ChangeState(baseLevel);
            }
        }
    }
}
