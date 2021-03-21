using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Text;

namespace TheGame.Mics
{
    class TileMap
    {
        Vector2 position = Vector2.Zero;
        TiledMap tMap;
        TiledMapRenderer mapRenderer;
        TiledMapLayer layer1;
        public List<Rectangle> mapObjects;
        Rectangle[] coins;
        Rectangle[] obstracles;
        Rectangle[] Enemies;
        public TileMap(TiledMap map, GraphicsDevice graphics)
        {
            tMap = map;
            mapRenderer = new TiledMapRenderer(graphics, tMap);
            getObjectsFromMap();
        }
        public void Update(GameTime time)
        {
            mapRenderer.Update(time);


        }
        public void Draw(Matrix transform)
        {

            mapRenderer.Draw(transform, null, null, 0);
        }
        void getObjectsFromMap()
        {

            TiledMapObject[] objTmp = tMap.GetLayer<TiledMapObjectLayer>("Col").Objects;
            mapObjects = new List<Rectangle>();
            foreach (var tmp in objTmp)
            {
                mapObjects.Add(new Rectangle((int)tmp.Position.X, (int) tmp.Position.Y, (int) tmp.Size.Width, (int) tmp.Size.Height));
            }
            
            objTmp = tMap.GetLayer<TiledMapObjectLayer>("Enemies").Objects;
            
        }

        public List<Rectangle> GetMapObjectList()
        {
            return mapObjects;
        }
        
    }
}
