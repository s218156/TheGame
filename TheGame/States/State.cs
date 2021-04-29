using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Mics;

namespace TheGame.States
{
    public abstract class State
    {
        protected ContentManager content;
        protected GraphicsDevice graphics;
        protected Game1 game;
        public SessionData session;
        public abstract void Initialize();

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public State(Game1 game, GraphicsDevice graphics, ContentManager content,SessionData session)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = content;
            this.session = session;
        }
        public void UpdateSessionData(SessionData session)
        {
            this.session = session;
        }


    }
}
