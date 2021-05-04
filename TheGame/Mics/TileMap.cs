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
        public List<Rectangle> gameMaster;
        public List<Vector2> coins;
        public List<Rectangle> ladders;
        public List<Rectangle> obstracles;
        public List<Vector2> snails;
        public List<Vector2> worms;
        public List<Vector2> mouse;
        public List<Vector2> enemies;
        public Vector2 spawnPosition;
        public Rectangle endPosition;
        public List<Rectangle> movableObjects;
        public List<Rectangle> powerups;
        public List<Rectangle> gameMasterSpawn, checkPoints,fallableObjects,springs, tourches;
        public List<LeverInstanceForMap> levers;
        public List<PlatformInstanceForMap> platforms;
     
        private string[] objectLayersToVector = { "Mouse", "Snails", "Worms", "Enemies", "Spawn","Coins" };
        private string[] objectLayersToRectangle = { "WorldColision", "Ladder", "PowerUps", "Obstracles", "Movable Boxes", "GameMaster","CheckPoints", "FallableObject" ,"Springs", "Levers","Platforms", "End", "Tourches" };
        public TileMap(TiledMap map, GraphicsDevice graphics)
        {
            tMap = map;
            mapRenderer = new TiledMapRenderer(graphics,tMap);
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
            TiledMapObject[] objTmp;

            mapObjects = new List<Rectangle>();
            ladders = new List<Rectangle>();
            powerups = new List<Rectangle>();
            obstracles = new List<Rectangle>();
            movableObjects = new List<Rectangle>();
            gameMasterSpawn = new List<Rectangle>();
            checkPoints= new List<Rectangle>();
            fallableObjects = new List<Rectangle>();
            springs = new List<Rectangle>();
            tourches = new List<Rectangle>();
            levers = new List<LeverInstanceForMap>();
            platforms = new List<PlatformInstanceForMap>();

            mouse = new List<Vector2>();
            worms = new List<Vector2>();
            snails = new List<Vector2>();
            enemies = new List<Vector2>();
            coins = new List<Vector2>();

            foreach (string layer in objectLayersToRectangle)
            {
                objTmp = tMap.GetLayer<TiledMapObjectLayer>(layer).Objects;
                foreach (var tmp in objTmp)
                {
                    if(layer=="WorldColision")
                        mapObjects.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
                    if (layer == "Ladder")
                        ladders.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
                    if(layer=="PowerUps")
                        powerups.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
                    if(layer=="Obstracles")
                        obstracles.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
                    if(layer=="Movable Boxes")
                        movableObjects.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
                    if (layer == "GameMaster")
                        gameMasterSpawn.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
                    if (layer == "CheckPoints")
                        checkPoints.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
                    if (layer == "FallableObject")
                        fallableObjects.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
                    if(layer=="Springs")
                        springs.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
                    if(layer=="Levers")
                        levers.Add(new LeverInstanceForMap(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height),int.Parse(tmp.Type)));
                    if (layer == "Platforms")
                        platforms.Add(new PlatformInstanceForMap(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height), int.Parse(tmp.Type)));
                    if (layer == "End")
                        endPosition = new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height);
                    if (layer == "Tourches")
                        tourches.Add(new Rectangle((int)tmp.Position.X, (int)tmp.Position.Y, (int)tmp.Size.Width, (int)tmp.Size.Height));
                }
            }
                       
            foreach (string layer in objectLayersToVector)
            {
                objTmp = tMap.GetLayer<TiledMapObjectLayer>(layer).Objects;
                foreach (var tmp in objTmp)
                {
                    if(layer=="Mouse")
                        mouse.Add(new Vector2((int)tmp.Position.X, (int)tmp.Position.Y));
                    if (layer=="Worms")
                        worms.Add(new Vector2((int)tmp.Position.X, (int)tmp.Position.Y));
                    if(layer=="Snails")
                        snails.Add(new Vector2((int)tmp.Position.X, (int)tmp.Position.Y));
                    if(layer=="Enemies")
                        enemies.Add(new Vector2((int)tmp.Position.X, (int)tmp.Position.Y));
                    if(layer=="Spawn")
                        spawnPosition = new Vector2((int)tmp.Position.X, (int)tmp.Position.Y);
                    if (layer == "Coins")
                        coins.Add(new Vector2((int)tmp.Position.X, (int)tmp.Position.Y));

                }
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

        
    }
}
