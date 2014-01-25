using GameLibrary.Dependencies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Components
{
    public class BloodTimer : Component
    {
        public float Time;
        public float StartTime;

        public BloodTimer(float time)
        {
            StartTime = time;
            Time = time;
        }
    }
}
