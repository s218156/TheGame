using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
    class Level0 : Level
    {
        public Level0(Game1 game, GraphicsDevice graphics, ContentManager content, SessionData session):base(game,graphics,content, session)
        {
            LoadMap();
            messageList = new List<string>(){
                "Hi Player! Welcome to the game! to move press 'A' or 'D'!" ,
                "Great! On Your right, there is a BOX. To jump on it press 'SPACE'",
                "AWESOME! If you see any spikes remember to ommit them. In this issue try to jump over the gap.",
                "Remember that You can crouch. When on the ground press 'S'!",
                "Now let's see if You can pass this!",
                "WOW! Remember to keep focused...",
                "OK. Now this is the ladder. When You are on it, you can climp by pressing 'W' or 's'",
                "Chains works same as ladders, but horizontaly...",
                "Some of the BOXES are movable. Try to reach upper platform.",
                "Now time for enemies. These are bugs. You can kill them by jumping on them",
                "But remember... they can hurt you as well...",
                "This is CheckPoint. If you die, You will respawn at this point!",
                "Now follow the arrows and DONT get killed!"
            };
            
            Initialize();
        }

        protected override void LoadMap()
        {
            map = new TileMap(content.Load<TiledMap>("TileMaps//level0/Level0-map"), graphics);

            Paralax p1 = new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p2"), graphics, Vector2.Zero, new Vector2((float)0.5, (float)0.9));
            Paralax p2 = new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p1"), graphics, new Vector2(-1,0), new Vector2((float)0.5, (float)0.9));
            Paralax p3 = new Paralax(content.Load<Texture2D>("Backgrounds/Level0/l0p0"), graphics, Vector2.Zero, new Vector2((float)0.5, (float)0.9));
            _paralaxes.Add(p1);
            _paralaxes.Add(p2);
            _paralaxes.Add(p3);
        }
    }
}
