using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Animations;
using TheGame.Mics;
using TheGame.Sprites;

namespace TheGame.Items
{
    class GameMaster:Item
    {
        ItemAnimation animatedTexture;
        ChatBox chatBox;
        public bool isActive;
        private bool wasAccepted;
        List<Rectangle> triggers;
        List<string> captions;
        public GameMaster(ContentManager content, Rectangle rectangle,List<Rectangle> triggers, List<string> captions) : base(null, rectangle)
        {

            this.animatedTexture = new ItemAnimation(content.Load<Texture2D>("Sprites/GameMaster"), rectangle, 3, 1);
            isActive = false;
            this.triggers = triggers;
            wasAccepted = false;
            this.captions = captions;

            chatBox = new ChatBox(content.Load<Texture2D>("GameUI/chatBox"), new Rectangle((((rectangle.X / 4) * 5) / 4)+100, (rectangle.Y * 4) / 10, ((rectangle.X / 4) * 5) /2, rectangle.Y), content.Load<SpriteFont>("Fonts/smallFont"));
        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                chatBox.Draw(gameTime, spriteBatch);
                animatedTexture.Draw(gameTime, spriteBatch);
                chatBox.DrawText(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime, Player player)
        {
            if (wasAccepted & Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                isActive = false;
                wasAccepted = false;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Space))&(isActive))
                wasAccepted = true;
            else
            {
                animatedTexture.Update(gameTime, null);
                List < Rectangle > newTriggers= new List<Rectangle>();
                foreach(Rectangle tmp in triggers)
                {
                    if (tmp.Intersects(player.rectangle))
                    {
                        chatBox.UpdateCaption(captions[0]);
                        captions.RemoveAt(0);
                        isActive = true;
                    }
                    else
                    {
                        newTriggers.Add(tmp);
                    }
                }
                triggers = newTriggers;
            }
        }

        
    }
}
