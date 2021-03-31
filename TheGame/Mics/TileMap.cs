using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Text;
using TheGame.Items;

namespace TheGame.Mics
{
    public class TileMap
    {
        Vector2 position = Vector2.Zero;
        TiledMap tMap;
        TiledMapRenderer mapRenderer;
        TiledMapLayer layer1;
        public List<Rectangle> mapObjects;
        public List<Vector2> coins;
        public List<Rectangle> ladders;
        public List<Rectangle> obstracles;
        public List<Vector2> enemies;
        public Vector2 spawnPosition;
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

            TiledMapObject[] objTmp = tMap.GetLayer<TiledMapObjectLayer>("WorldColision").Objects;
            mapObjects = new List<Rectangle>();
            foreach (var tmp in objTmp)
            {
                mapObjects.Add(new Rectangle((int)tmp.Position.X, (int) tmp.Position.Y, (int) tmp.Size.Width, (int) tmp.Size.Height));
            }

            objTmp = tMap.GetLayer<TiledMapObjectLayer>("Coins").Objects;
            coins = new List<Vector2>();
            foreach (var tmp in objTmp)
            {
                coins.Add(new Vector2((int)tmp.Position.X, (int)tmp.Position.Y));
            }
            objTmp = tMap.GetLayer<TiledMapObjectLayer>("Ladder").Objects;
            ladders = new List<Rectangle>();
            foreach (var tmp in objTmp)
            {
                ladders.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
            }

            spawnPosition = new Vector2(tMap.GetLayer<TiledMapObjectLayer>("Spawn").Objects[0].Position.X, tMap.GetLayer<TiledMapObjectLayer>("Spawn").Objects[0].Position.Y);
           
            objTmp = tMap.GetLayer<TiledMapObjectLayer>("Obstracles").Objects;
            obstracles = new List<Rectangle>();
            foreach (var tmp in objTmp)
            {
                obstracles.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
            }

            objTmp = tMap.GetLayer<TiledMapObjectLayer>("Enemies").Objects;
            enemies = new List<Vector2>();
            foreach (var tmp in objTmp)
            {
                enemies.Add(new Vector2((int)tmp.Position.X, (int)tmp.Position.Y));
            }
        }

        public List<Rectangle> GetMapObjectList()
        {
            return mapObjects;
        }

        public List<Vector2> GetCoins()
        {
            return coins;
        }
        public List<Rectangle> GetLadders()
        {
            return ladders;
        }
        public List<Rectangle> GetObstracles()
        {
            return obstracles;
        }

        public List<Vector2> GetEnemies()
        {
            return enemies;
        }
        
    }
}
