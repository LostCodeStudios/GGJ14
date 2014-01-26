using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam
{
    public static class DelayCode
    {
        static float duration;
        static Action lambda;

        public static void Delay(Action code, float time)
        {
            lambda = code;
            duration = time;
        }

        public static void Update(GameTime gameTime)
        {
            if (lambda != null)
            {
                duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (duration <= 0)
                {
                    duration = 0;

                    lambda.Invoke();
                    lambda = null;
                }
            }
        }
    }
}
