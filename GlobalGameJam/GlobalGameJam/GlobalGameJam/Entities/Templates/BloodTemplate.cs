﻿using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using GlobalGameJam.Entities.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates
{
    public class BloodTemplate : IEntityTemplate
    {
        const float BLOOD_TIME = 1.5f;

        public Entity BuildEntity(Entity e, params object[] args)
        {
            Vector2 position = (Vector2)args[0];
            int bloodType = (int)args[1];

            Sprite sprite = new Sprite(ScreenHelper.SpriteSheet, "blood" + bloodType, 0.3f);
            e.AddComponent<Sprite>(sprite);

            Particle p = new Particle(e);
            p.Position = position;
            e.AddComponent<Particle>(p);

            return e;
        }
    }
}
