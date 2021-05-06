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
    class Level3 : Level
    {

        public Level3(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session) : base(game, graphics, content, session,new MainMenuState(game,graphics,content, session))
        {
            pointAtTheBegining = session.GetPlayerPoints();
            LoadMap();
            Initialize();

        }

        protected override void LoadMap()
        {
            map = new TileMap(content.Load<TiledMap>("TileMaps//level3/Level3-map"), graphics);


            Paralax p1 = new Paralax(content.Load<Texture2D>("Backgrounds/Level3/l3p1"), graphics, Vector2.Zero, new Vector2((float)0.5,0));
            Paralax p3 = new Paralax(content.Load<Texture2D>("Backgrounds/Level3/l3p2"), graphics, Vector2.Zero, new Vector2((float)0.2, (float)0));

            _paralaxes.Add(p3);
            _paralaxes.Add(p1);
            

            effect = content.Load<Effect>("shaders/lightEffect");
            isLightShader = true;
        }
    }
}
