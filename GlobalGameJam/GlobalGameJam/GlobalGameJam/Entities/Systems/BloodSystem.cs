using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GlobalGameJam.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Systems
{
    public class BloodSystem : EntityProcessingSystem
    {
        const float FLOAT_SPEED = 0.5f;

        public BloodSystem()
            : base(typeof(BloodTimer))
        {
        }

        public override void Process(Entity e)
        {
            BloodTimer timer = e.GetComponent<BloodTimer>();

            float seconds = (float)world.Delta / 1000f;

            timer.Time -= seconds;

            if (timer.Time <= 0)
            {
                e.Delete();
                return;
            }

            float alpha = timer.Time / timer.StartTime;

            Sprite sprite = e.GetComponent<Sprite>();
            sprite.Color.A = (byte)(alpha * 255);

            Particle p = e.GetComponent<Particle>();
            p.Position = new Microsoft.Xna.Framework.Vector2(p.Position.X, p.Position.Y - FLOAT_SPEED * (world.Delta / 1000f));

            e.RemoveComponent<Sprite>(e.GetComponent<Sprite>());
            e.AddComponent<Sprite>(sprite);
            e.Refresh();
        }
    }
}
