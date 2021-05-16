using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.States.Menu
{
    class MenuState:State
    {
        protected List<Paralax> _paralaxes;
        protected List<Component> _components;
        protected List<Button> _buttons;
        protected Selection selection;
        

        public MenuState(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session) : base(game, graphics, content, session)
        {
            _buttons = new List<Button>();
            _components = new List<Component>();
        }
        
        public override void Initialize()
        {
            selection = new Selection(content);
            selection.SetSelectionPosition(_buttons);
            _paralaxes = new List<Paralax>();
            
            _paralaxes.Add(new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p2"), graphics, new Vector2(3, 0), new Vector2((float)0.5, (float)0.9)));
            _paralaxes.Add(new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p3"), graphics, new Vector2((float)2, 0), new Vector2((float)0.5, (float)0.9)));
            _paralaxes.Add(new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p0"), graphics, new Vector2((float)1, 0), new Vector2((float)0.5, (float)0.9)));
            _paralaxes.Add(new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p1"), graphics, new Vector2(3, 0), new Vector2((float)0.5, (float)0.9)));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach(var tmp in _paralaxes)
            {
                tmp.Draw(gameTime, spriteBatch);
            }
            foreach (var item in _components)
                item.Draw(gameTime, spriteBatch);
            selection.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button item in _components)
            {
                item.Update(gameTime);

                selection.Update(_buttons);
            }
            foreach(Paralax tmp in _paralaxes)
            {
                tmp.Update(new Player(null,Vector2.Zero,null,1),graphics);
            }
        }
    }
    class Selection
    {
        public int selected;
        private Texture2D texture;
        private Rectangle rectangle;
        private bool keysWasUp;

        public Selection(ContentManager content)
        {
            texture = content.Load<Texture2D>("GameUI/selection");
            selected = 0;
            keysWasUp = false;
        }
        public void Update(List<Button> _buttons)
        {
            
            if (keysWasUp)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    _buttons[selected].ButtonSelected();
                    keysWasUp = false;
                }
                    


                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    selected += 1;
                    keysWasUp = false;
                }
                    

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    selected -= 1;
                    keysWasUp = false;
                }
                    
            }
            if (Keyboard.GetState().IsKeyUp(Keys.W) & Keyboard.GetState().IsKeyUp(Keys.S)& Keyboard.GetState().IsKeyUp(Keys.Enter))
                keysWasUp = true;
            

            if (selected > _buttons.Count - 1)
                selected = _buttons.Count - 1;
            if (selected < 0)
                selected = 0;
            rectangle.Y = _buttons[selected].rectangle.Y;
            
        }
        public void SetSelectionPosition(List<Button> _buttons)
        {
            rectangle = new Rectangle(_buttons[selected].rectangle.X - _buttons[selected].rectangle.Height, _buttons[selected].rectangle.Y,
                _buttons[selected].rectangle.Height, _buttons[selected].rectangle.Height);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

    }
}
