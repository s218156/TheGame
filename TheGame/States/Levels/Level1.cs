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
    class Level1 : Level
    {

        public Level1(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session) : base(game, graphics, content, session)
        {
            pointAtTheBegining = session.GetPlayerPoints();
            LoadMap();
            Initialize();

        }

        protected override void LoadMap()
        {
            map = new TileMap(content.Load<TiledMap>("TileMaps//level1/Level1-map"), graphics);
        }
    }
}
