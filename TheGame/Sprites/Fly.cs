using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TheGame.Items;
using TheGame.Mics;

namespace TheGame.Sprites
{
    class Fly:MovingBug
    {
        public Fly(Texture2D texture,Vector2 position,Texture2D deathTexture, int range, int speed) : base(texture, position, deathTexture, range, speed)
        {
            canFly = true;
        }

        public override void Update(GameTime gameTime, Player player, TileMap map, List<MovableItem> movableList, List<WaterArea> waterAreas)
        {
            canFly = true;
            base.Update(gameTime, player, map, movableList,waterAreas);
        }
    }
}
