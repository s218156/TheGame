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
        SpriteFont font;
        string caption;
        string infoCation = "Press 'Space' to continue...";
        public ChatBox(Texture2D texture ,Rectangle rectangle ,SpriteFont font)
        {
            this.texture = texture;
            this.rectangle = rectangle;
            this.font = font;

        }
        
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, rectangle, Color.White);

        }

        public void DrawText(SpriteBatch spriteBatch)
        {
            Vector2 textVector = new Vector2((rectangle.X + 3 * (rectangle.Width / 4)-(int)font.MeasureString(infoCation).X/2), rectangle.Y + 3 * (rectangle.Height / 5));
            spriteBatch.DrawString(font, infoCation, textVector, Color.Black);


            int textLenght = (int)font.MeasureString(caption).X;
            int textHeight = (int)font.MeasureString(caption).Y;
            if (textLenght < (4 * rectangle.Width / 5)){

                spriteBatch.DrawString(font, caption,new Vector2(rectangle.X + rectangle.Width / 2 - textLenght / 2,rectangle.Y+rectangle.Height/2- textHeight), Color.Black);
            }
            else
            {
                Vector2 drawingTextVector =Vector2.Zero;
                int textLines = textLenght / (4*(rectangle.Width/5))+1;
                int charactersInLine = caption.Length / textLines;

                

                for(int i = 0; i < textLines; i++)
                {
                    string tmp = "";
                    for (int j = 0; j < charactersInLine; j++)
                    {
                        tmp += caption[j+charactersInLine*i];
                    }

                    drawingTextVector = new Vector2(rectangle.X + rectangle.Width / 2 - font.MeasureString(tmp).X/2, rectangle.Y + rectangle.Height / 6 + i*(2*textHeight ));
                    spriteBatch.DrawString(font, tmp, drawingTextVector, Color.Black);


                }



            }

        }

        public void UpdateCaption(string caption)
        {
            this.caption = caption;
        }
        
    }
}
