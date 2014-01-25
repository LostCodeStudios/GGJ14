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
            int cats = (int)args[1];
            int goblins = (int)args[2];

            for (int i = 0; i < cats; ++i)
            {
                Vector2 pos = position + new Vector2((float)r.NextDouble() * Chunk.SIZE - Chunk.SIZE/2,
                        (float)r.NextDouble() * Chunk.SIZE - Chunk.SIZE/2);

                world.CreateEntity("Cat", pos).Refresh(); ;
            }

            for (int i = 0; i < goblins; ++i)
            {
                Vector2 pos = position + new Vector2((float)r.NextDouble() * Chunk.SIZE - Chunk.SIZE / 2,
                        (float)r.NextDouble() * Chunk.SIZE - Chunk.SIZE / 2);

                world.CreateEntity("Goblin", pos);
            }

            //and god said, fuck okay, trees.
            for (int i = 0; i < 25; i++)
                terrain.Add(world.CreateEntity("Tree",
                    position + new Vector2((float)r.NextDouble() * Chunk.SIZE - Chunk.SIZE/2,
                        (float)r.NextDouble() * Chunk.SIZE - Chunk.SIZE/2)));
                return terrain.ToArray();
        }
    }
}
