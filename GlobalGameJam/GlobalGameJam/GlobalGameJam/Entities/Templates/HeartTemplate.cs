using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using GlobalGameJam.Entities.Components;
using GlobalGameJam.Entities.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates
{
    public class HeartTemplate : IEntityTemplate
    {
        const float BLOOD_TIME = 1.5f;

        public Entity BuildEntity(Entity e, params object[] args)
        {
            Body trackingBody = (Body)args[0];
            //Vector2 position = (Vector2)args[0];

            Sprite sprite = new Sprite(ScreenHelper.SpriteSheet, "heart", 0.61f);
            e.AddComponent<Sprite>(sprite);

            Particle p = new Particle(e);
            p.Position = trackingBody.Position - new Vector2(0, BloodSystem.STARTING_HEART_HEIGHT);
            e.AddComponent<Particle>(p);

            BloodTimer timer = new BloodTimer(BLOOD_TIME);
            timer.TrackingBody = trackingBody;
            e.AddComponent<BloodTimer>(timer);

            return e;
        }
    }
}
