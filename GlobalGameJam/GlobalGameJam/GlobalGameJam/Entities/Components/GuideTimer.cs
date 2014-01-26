using GameLibrary.Dependencies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Components
{
    public class GuideTimer : Component
    {
        public const float TRANSITION_TIME = 0.4f;
        public const float GUIDE_DURATION = 4.5f;

        public float ElapsedTime = 0f;
    }
}
