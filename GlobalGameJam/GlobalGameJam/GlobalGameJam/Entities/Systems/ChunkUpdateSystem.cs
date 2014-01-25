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
        public ChunkUpdateSystem(int width = 3, int height= 3) : base("Player")
        {
            this.width = width;
            this.height = height;

            chunks = new KeyValuePair<Vector2, List<Entity>>[width, height];
        }
        #region Fields
        KeyValuePair<Vector2, List<Entity>>[,] chunks;

        int width;
        int height;

        #endregion

        /// <summary>
        /// Processes the player versus the locations of the chunks.
        /// 1. Removes chunks that are too distant from the player. There storing a list of removed chunks to be replenished.
        /// 
        /// </summary>
        /// <param name="e"></param>
        public override void Process(Entity e)
        {
            Vector2 pos = e.GetComponent<Body>().Position;

            #region Edge Detection
            //Check top/bot
            for (int x = 0; x < width; x++)
            {
                Vector2 top = chunks[x, 0].Key;
                Vector2 bottom = chunks[x, height - 1].Key;

                if (Vector2.Distance(top, pos) > Math.Sqrt(2) * 32)
                {
                    DestroyChunk(x, 0);
                    MoveCollumn(x, false);
                }
                else if (Vector2.Distance(bottom, pos) > Math.Sqrt(2) * 32)
                {
                    DestroyChunk(x, height - 1);
                    MoveCollumn(x, true);
                }
            }

            //Check left.right
            for (int y = 0; y < height; y++)
            {
                Vector2 left = chunks[0, y].Key;
                Vector2 right = chunks[width -1, y].Key;

                if (Vector2.Distance(left, pos) > Math.Sqrt(2) * 32)
                {
                    DestroyChunk(0, y);
                    MoveRow(y, false);
                }

                if (Vector2.Distance(right, pos) > Math.Sqrt(2) * 32)
                {
                    DestroyChunk(width-1, y);
                    MoveRow(y, true);
                }

            }

            #endregion

            #region Replenishment

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    if (chunks[x, y].Value == null){
                        Vector2 chunkPos = pos+new Vector2(32*x,32*y) - new Vector2(32); //see paper algorithm
                        chunks[x,y] = new KeyValuePair<Vector2, List<Entity>>
                        (chunkPos,
                        new List<Entity>(world.CreateEntityGroup("Chunk", "terrain", chunkPos)));
                    }

            #endregion

        }

        private void DestroyChunk(int x, int y){
            KeyValuePair<Vector2, List<Entity>> chunk = chunks[x, y];
            if(chunk.Value != null)
                foreach (Entity e in chunk.Value)
                    e.Delete();
        }

        private void MoveCollumn(int x, bool down)
        {
            if (down)
            {
                chunks[x, height - 1] = chunks[x, height - 2];
                chunks[x, height - 2] = chunks[x, 0];
                chunks[x, 0] = new KeyValuePair<Vector2, List<Entity>>(Vector2.Zero, null);
            }
            else
            {
                chunks[x, 0] = chunks[x, height - 2];
                chunks[x, height - 2] = chunks[x, height - 1];
                chunks[x, height-1] = new KeyValuePair<Vector2, List<Entity>>(Vector2.Zero, null);
            }
        }

        private void MoveRow(int y, bool right)
        {
            if (right)
            {
                chunks[width - 1, y] = chunks[width - 2, y];
                chunks[width - 2, y] = chunks[0, y];
                chunks[0, y] = new KeyValuePair<Vector2, List<Entity>>(Vector2.Zero, null);
            }
            else
            {
                chunks[0, y] = chunks[width - 2, y];
                chunks[width - 2, y] = chunks[width-1, y];
                chunks[width-1, y] = new KeyValuePair<Vector2, List<Entity>>(Vector2.Zero, null);
            }
        }
    }
}
