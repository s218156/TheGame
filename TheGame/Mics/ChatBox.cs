using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame.Mics
{
    class ChatBox
    {
        Rectangle rectangle;
        Texture2D texture;
        Vector2 relativePoint;
        SpriteFont font;
        string caption;
        string infoCation = "Press 'Space' to continue...";
        public ChatBox(Texture2D texture ,Rectangle rectangle ,SpriteFont font)
        {
            this.texture = texture;
            this.relativePoint = new Vector2(rectangle.X, rectangle.Y);
            this.font = font;
            this.rectangle = new Rectangle((int)((relativePoint.X) - (rectangle.Width / 2)), (int)(relativePoint.Y - rectangle.Height / 2), rectangle.Width / 2, rectangle.Height / 2);

        }
        
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, rectangle, Color.White);

        }

        public void DrawText(SpriteBatch spriteBatch)
        {
            
            


            int textLenght = (int)font.MeasureString(caption).X;
            int textHeight = (int)font.MeasureString(caption).Y;
            
            int textLines = textLenght / (4 * (rectangle.Width / 5)) + 1;
            int boxHeight = (textHeight * 2) * (textLines + 2);
            int charactersInLine = caption.Length / textLines;

            rectangle.Height = boxHeight;
            rectangle.Y = (int)relativePoint.Y-boxHeight;
            Vector2 textVector = new Vector2((rectangle.X + 3 * (rectangle.Width / 4) - (int)font.MeasureString(infoCation).X / 2), rectangle.Y + 3 * (rectangle.Height / 5));

            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.DrawString(font, infoCation, textVector, Color.Black);

            string tmp = "";
            int j = 0;
            for(int i = 1; i <= textLines; i++)
            {
                tmp = "";
                do
                {
                    tmp += caption[j];
                    j++;
                    if (j > caption.Length - 1)
                        break;
                } while (!((caption[j].Equals(' ')) & (j >= charactersInLine * i)));

                Vector2 textPosition = new Vector2((int)(rectangle.X + rectangle.Width / 2 - font.MeasureString(tmp).X/2), rectangle.Y + rectangle.Height/5 + textHeight * (i-1));
            spriteBatch.DrawString(font, tmp, textPosition, Color.Black); ;
            }
        }

        public void UpdateCaption(string caption)
        {
            this.caption = caption;
        }
        
    }
}
