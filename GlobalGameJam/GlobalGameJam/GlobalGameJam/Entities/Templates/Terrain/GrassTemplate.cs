using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates.Terrain
{
    class GrassTemplate : IEntityTemplate
    {
        /// <summary>
        /// TAKES A BEETCH AND MAKES GRASS IN YO ASS
        /// </summary>
        /// <param name="e">THE E</param>
        /// <param name="args">SUM ARGS FOR (VECTOR2)</param>
        /// <returns></returns>
        public Entity BuildEntity(Entity e, params object[] args)
        {
            e.AddComponent<Sprite>(new Sprite(ScreenHelper.SpriteSheet, "grass"));
            e.AddComponent<Particle>(new Particle(e, (Vector2)args[0], 0f, Vector2.Zero, 0f));

            e.Refresh();
            return e;
        }
    }
}
