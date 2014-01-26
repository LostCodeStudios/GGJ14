using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Collision;
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
        public const float TREE_SPACE = 2f;

        Random r = new Random();
        public Entity[] BuildEntityGroup(EntityWorld world, params object[] args)
        {
            List<Entity> terrain = new List<Entity>();

            Vector2 position = (Vector2)args[0];
            int cats = BoblinWorld.FIRST_CATS;
            int goblins = toInt(BoblinWorld.FIRST_GOBLINS);

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

                world.CreateEntity("Goblin", pos).Refresh();
            }

            //and god said, fuck okay, trees.
            for (int i = 0; i < BoblinWorld.FIRST_TREES; i++)
            {
                Vector2 pos;
                while (true)
                {
                    pos = position + new Vector2((float)r.NextDouble() * Chunk.SIZE - Chunk.SIZE / 2,
                            (float)r.NextDouble() * Chunk.SIZE - Chunk.SIZE / 2);

                    AABB box = new AABB(pos, TREE_SPACE, TREE_SPACE);
                    bool treeInSpace = false;
                    world.QueryAABB(
                        (f) =>
                        {
                            treeInSpace = true;

                            return false;
                        }, ref box);

                    if (!treeInSpace)
                        break;
                }

                terrain.Add(world.CreateEntity("Tree", pos));
            }
            return terrain.ToArray();
        }

        private bool percentChance(float chance)
        {
            return r.NextDouble() < chance;
        }

        private int toInt(float chance)
        {
            int intPart = (int)chance;
            float decimalPart = chance - intPart;

            if (percentChance(decimalPart)) intPart++;

            return intPart;
        }
    }
}
