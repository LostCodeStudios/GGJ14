using GameLibrary.Dependencies.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Systems
{
    class Chunk
    {
        public Chunk(Vector2 position, List<Entity> terrain)
        {
            this.Position = position;
            this.Terrain = terrain;
        }
        /// <summary>
        /// The position of the chunk in world coordinates
        /// </summary>
        public Vector2 Position { set; get; }

        /// <summary>
        /// The terrain entities stored within the chunk.
        /// </summary>
        public List<Entity> Terrain { set; get; }

        /// <summary>
        /// Deletes the chunk giving off a new position to generate a suceeding chunk based on an anchor and a new origin
        /// </summary>
        /// <param name="anchor">The old center origin</param>
        /// <param name="origin">The new origin</param>
        /// <returns></returns>
        public Vector2 Delete(Vector2 anchor, Vector2 origin)
        {
            foreach (Entity e in Terrain)
                e.Delete();

            return anchor - Position + origin;
        }
    }
}
