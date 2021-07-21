using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Content.Items;
using TheGame.Inventory;
using TheGame.Items;
using TheGame.Mics;
using TheGame.Mics.GUI_components;
using TheGame.SoundControllers;
using TheGame.Sprites;
using TheGame.States.Menu;

namespace TheGame.States
{
    class Level4 : Level
    {

        public Level4(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session) : base(game, graphics, content, session,4,0)
        {
            pointAtTheBegining = session.GetPlayerPoints();
            LoadMap();
            messageList = new List<string>(){
                "It's dark down here. As you can see the light is emited by touches",
                "Great job! you made it!",
                "That's all for now, I hope you enjoyed The Game",
                "Follow the GitHub repository for updates",
                "If developer will have enough coffee to evolve this project xD"
            };
            Initialize();

        }

        protected override void LoadMap()
        {
            map = new TileMap(content.Load<TiledMap>("TileMaps/level4/Level4-map"), graphics);


            Paralax p1 = new Paralax(content.Load<Texture2D>("Backgrounds/Level3/l3p1"), graphics, Vector2.Zero, new Vector2((float)0.5,0));
            Paralax p3 = new Paralax(content.Load<Texture2D>("Backgrounds/Level3/l3p2"), graphics, Vector2.Zero, new Vector2((float)0.2, (float)0));

            _paralaxes.Add(p3);
            _paralaxes.Add(p1);
            
        }
    }
}
