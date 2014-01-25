using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Systems
{
    public class ChunkUpdateSystem : TagSystem
    {
        /// <summary>
        /// Makes a chunk update system with a width and height of the cunks surroudning the player :)
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public ChunkUpdateSystem(float chunkR) : base("Player")
        {
            chunks = new List<Chunk>();
            radius = chunkR;
        }
        #region Fields

        public List<Chunk> chunks;

        Vector2 oldPosition;
        float radius;

        #endregion

        public void BuildInitial(Vector2 pos, int width, int height, int catsPer, int goblinsPer)
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Vector2 chunkPos = pos + Chunk.SIZE*(-0.5F*new Vector2(height-1, width-1) + new Vector2( x, y)); //see paper algorithm
                    chunks.Add(new Chunk
                    (chunkPos,
                    new List<Entity>(world.CreateEntityGroup("Chunk", "terrain", chunkPos, catsPer, goblinsPer))));
                }
        }

        /// <summary>
        /// Processes the player versus the locations of the chunks.
        /// 1. Removes chunks that are too distant from the player. There storing a list of removed chunks to be replenished.
        /// 
        /// </summary>
        /// <param name="e"></param>
        public override void Process(Entity e)
        {
            Vector2 pos = e.GetComponent<Body>().Position;
            pos = new Vector2((((int)pos.X) / (int)Chunk.SIZE) * Chunk.SIZE, (((int)pos.Y) / (int)Chunk.SIZE) * Chunk.SIZE);

            
            if (pos != oldPosition)
            {
                Console.WriteLine(pos);
                List<Chunk> toRemove = new List<Chunk>();

                foreach(Chunk c in chunks)
                    if(Vector2.Distance(c.Position, pos) > radius)
                        toRemove.Add(c);

                foreach(Chunk c in toRemove)
                {
                    chunks.Remove(c);
                    
                    Vector2 nChunkPos = c.Delete(oldPosition, pos);

                    int cats = BoblinWorld.FIRST_CATS;
                    int goblins = BoblinWorld.FIRST_GOBLINS;
                    chunks.Add(new Chunk(nChunkPos, new List<Entity>(world.CreateEntityGroup("Chunk", "terrain", nChunkPos, cats, goblins))));
                }

                toRemove.Clear();

                oldPosition = pos;

            }


        }

        private void DestroyChunk(List<Entity> chunk){;
            foreach (Entity e in chunk)
                e.Delete();
        }
    }
}
