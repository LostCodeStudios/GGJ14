using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates
{
    public class CorpseTemplate : IEntityTemplate
    {
        public Entity BuildEntity(Entity e, params object[] args)
        {
            Vector2 position = (Vector2)args[0];

            Particle p = new Particle(e, position, 0f, Vector2.Zero, 0f);
            e.AddComponent(p);

            Sprite sprite = new Sprite(ScreenHelper.SpriteSheet, "corpse", 0.5f);
            e.AddComponent<Sprite>(sprite);

            return e;
        }
    }
}
