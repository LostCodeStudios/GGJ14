using GameLibrary.Dependencies.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Components
{
    public class BunnyAI : Component
    {
        static Random r = new Random();
        const float minTime = 0.75f;
        const float maxTime = 2.5f;

        public Vector2 Direction;
        public float Time;

        public BunnyAI()
        {
            Direction = NextDirection();
            Time = NextTime();
        }

        public Vector2 NextDirection()
        {
            float x = (float)r.NextDouble() * 2 - 1;
            float y = (float)r.NextDouble() * 2 - 1;

            Vector2 dir = new Vector2(x, y);

            if (dir != Vector2.Zero)
                dir.Normalize();

            return dir;
        }

        public float NextTime()
        {
            return (float)r.NextDouble() * (maxTime - minTime) + minTime;
        }
    }
}
