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
        public GoblinAI(Entity target)
        {
            Target = target;
        }
    }
}
