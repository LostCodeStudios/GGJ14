using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components;
using GlobalGameJam.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Systems
{
    public class BloodSystem : EntityProcessingSystem
    {
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

            e.RemoveComponent<Sprite>(e.GetComponent<Sprite>());
            e.AddComponent<Sprite>(sprite);
            e.Refresh();
        }
    }
}
