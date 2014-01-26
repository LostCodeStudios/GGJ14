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
    public class ThresholdTemplate : IEntityTemplate
    {
        public Entity BuildEntity(Entity e, params object[] args)
        {
            Particle p = new Particle(e);
            p.Position = new Vector2(0, 1.06f);
            e.AddComponent<Particle>(p);

            Sprite sprite = new Sprite(ScreenHelper.SpriteSheet, "doorBack");
            e.AddComponent<Sprite>(sprite);

            return e;
        }
    }
}
