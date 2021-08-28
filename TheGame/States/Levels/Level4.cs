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
            
        }
        public override void prepareLevel()
        {
            pointAtTheBegining = session.GetPlayerPoints();
            LoadMap();
            messageList = new List<string>()
            {

            };
            Initialize();
        }

        protected override void LoadMap()
        {
            map = new TileMap(content.Load<TiledMap>("TileMaps/level4/Level4-map"), graphics);


            Paralax p1 = new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p2"), graphics, Vector2.Zero, new Vector2((float)0.2, 0));
            Paralax p2 = new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p1"), graphics, new Vector2(-1, 0), new Vector2((float)0.5, (float)0.9));
            Paralax p3 = new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p0"), graphics, Vector2.Zero, new Vector2((float)0.4, (float)0.1));
            Paralax p4 = new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p3"), graphics, Vector2.Zero, new Vector2((float)0.3, (float)0));
            _paralaxes.Add(p1);
            _paralaxes.Add(p4);
            _paralaxes.Add(p3);
            _paralaxes.Add(p2);

        }
    }
}
