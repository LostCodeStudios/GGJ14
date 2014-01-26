using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components.Physics;
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

        public Body TrackingBody;

        public BloodTimer(float time)
        {
            StartTime = time;
            Time = time;
        }
    }
}
