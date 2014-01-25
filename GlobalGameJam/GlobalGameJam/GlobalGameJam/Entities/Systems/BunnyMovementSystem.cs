using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components.Physics;
using GlobalGameJam.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Systems
{
    public class BunnyMovementSystem : EntityProcessingSystem
    {
        const float BUNNY_SPEED = 2f;

        public BunnyMovementSystem()
            : base(typeof(BunnyAI))
        {
        }

        public override void Process(Entity e)
        {
            BunnyAI hop = e.GetComponent<BunnyAI>();

            float seconds = (float)world.Delta / 1000f;

            hop.Time -= seconds;

            if (hop.Time <= 0f)
            {
                hop.Time = hop.NextTime();
                hop.Direction = hop.NextDirection();
            }

            Body body = e.GetComponent<Body>();
            body.LinearVelocity = hop.Direction * BUNNY_SPEED;
        }
    }
}
