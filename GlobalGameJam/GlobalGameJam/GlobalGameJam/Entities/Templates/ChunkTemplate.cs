using GameLibrary.Dependencies.Entities;
using GlobalGameJam.Entities.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates
{
    /// <summary>
    /// BUILDS 32x32 chunk
    /// </summary>
    class ChunkTemplate : IEntityGroupTemplate
    {
        Random r = new Random();
        public Entity[] BuildEntityGroup(EntityWorld world, params object[] args)
        {
            List<Entity> terrain = new List<Entity>();

            Vector2 position = (Vector2)args[0];

            /////and god said let there be grass
            //for (int x = 0; x < Chunk.SIZE; x++)
            //    for (int y = 0; y < Chunk.SIZE; y++)
            //        terrain.Add(world.CreateEntity("Grass",
            //            position+new Vector2(x-Chunk.SIZE/2, y-Chunk.SIZE/2)));

            //and god said, fuck okay, trees.
            for (int i = 0; i < 25; i++)
                terrain.Add(world.CreateEntity("Tree",
                    position + new Vector2((float)r.NextDouble() * Chunk.SIZE - Chunk.SIZE/2,
                        (float)r.NextDouble() * Chunk.SIZE - Chunk.SIZE/2)));
                return terrain.ToArray();
        }
    }
}
