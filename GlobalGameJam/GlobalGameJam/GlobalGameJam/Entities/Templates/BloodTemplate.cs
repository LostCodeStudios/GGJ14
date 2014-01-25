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

            Sprite sprite = new Sprite(ScreenHelper.SpriteSheet, "blood");
            e.AddComponent<Sprite>(sprite);

            Particle p = new Particle(e);
            p.Position = position;
            e.AddComponent<Particle>(p);

            BloodTimer timer = new BloodTimer(BLOOD_TIME);
            e.AddComponent<BloodTimer>(timer);

            return e;
        }
    }
}