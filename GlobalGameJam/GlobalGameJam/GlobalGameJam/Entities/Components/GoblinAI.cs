using GameLibrary.Dependencies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Components
{
    public class GoblinAI : Component
    {
        public Entity Target;

        public bool CanMakeNoise = true;

        public GoblinAI(Entity target)
        {
            Target = target;
        }

        public float Delay = 0f;
    }
}
