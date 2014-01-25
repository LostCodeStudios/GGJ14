using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components.Physics;
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

        List<Chunk> chunks;

        Vector2 oldPosition;
        float radius;

        #endregion

        public void BuildInitial(Vector2 pos, int width, int height)
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Vector2 chunkPos = pos + new Vector2(32 * x, 32 * y) - new Vector2(32); //see paper algorithm
                    chunks.Add(new Chunk
                    (chunkPos,
                    new List<Entity>(world.CreateEntityGroup("Chunk", "terrain", chunkPos))));
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
            pos = new Vector2((((int)pos.X)/32)*32f, (((int)pos.Y)/32)*32f);
            if (pos != oldPosition)
            {
                List<Chunk> toRemove = new List<Chunk>();

                foreach(Chunk c in chunks)
                    if(Vector2.Distance(c.Position, pos) > radius)
                        toRemove.Add(c);

                foreach(Chunk c in toRemove)
                {
                    chunks.Remove(c);
                    Vector2 nChunkPos = c.Delete(oldPosition, pos);
                    chunks.Add(new Chunk(nChunkPos, new List<Entity>(world.CreateEntityGroup("Chunk", "terrain", nChunkPos))));
                }

                toRemove.Clear();

            }


        }

        private void DestroyChunk(List<Entity> chunk){;
            foreach (Entity e in chunk)
                e.Delete();
        }
    }
}
