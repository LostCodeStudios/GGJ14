using GameLibrary.Dependencies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates.Terrain
{
    class TreeTemplate : IEntityTemplate
    {
        EntityWorld world;
        public TreeTemplate(EntityWorld world)
        {
            this.world = world;
        }


        /// <summary>
        /// Makes the i entity template
        /// </summary>
        /// <param name="e"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Entity BuildEntity(Entity e, params object[] args)
        {
            

            e.Refresh();
            return e;
        }
    }
}
