using GameLibrary.Dependencies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates
{
    class ChunkTemplate : IEntityGroupTemplate
    {
        public Entity[] BuildEntityGroup(EntityWorld world, params object[] args)
        {
            List<Entity> terrain = new List<Entity>();

            return terrain.ToArray();
        }
    }
}
