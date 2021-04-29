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
        Vector2 screenData;
        public GameMaster(ContentManager content, Vector2 screenData,List<Rectangle> triggers, List<string> captions) : base(null, new Rectangle((int)(4 * (screenData.X / 5)), (int)(3 * (screenData.Y / 5)), (int)screenData.X,(int)screenData.Y))
        {
            this.screenData = screenData;
            
            isActive = false;
            this.triggers = triggers;
            wasAccepted = false;
            this.captions = captions;

            chatBox = new ChatBox(content.Load<Texture2D>("GameUI/chatBox"), rectangle, content.Load<SpriteFont>("Fonts/smallFont"));

            rectangle.X = 4 * (rectangle.Width / 5);
            rectangle.Y = 3 * (rectangle.Height / 5);
            rectangle.Width = 100;
            rectangle.Height = 100;
            this.animatedTexture = new ItemAnimation(content.Load<Texture2D>("Sprites/GameMaster"), rectangle, 3, 1);
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

        public override void Update(GameTime gameTime, Player player, TileMap map)
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
                CheckTrigger(player);
                
            }
        }

        private void CheckTrigger(Player player)
        {
            List<Rectangle> newTriggers = new List<Rectangle>();
            foreach (Rectangle tmp in triggers)
            {
                if (tmp.Intersects(player.rectangle))
                {
                    chatBox.UpdateCaption(captions[0]);
                    captions.RemoveAt(0);
                    isActive = true;
                }
                else
                    newTriggers.Add(tmp);
                
            }
            triggers = newTriggers;
        }
    }
}
