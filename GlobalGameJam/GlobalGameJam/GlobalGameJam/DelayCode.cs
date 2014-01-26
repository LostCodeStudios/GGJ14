using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam
{
    public static class DelayCode
    {
       
        static List<Delay> delays = new List<Delay>();

        public static void Delay(Action code, float time)
        {
            delays.Add(new Delay(code, time));
        }

        public static void Update(GameTime gameTime)
        {
            for(int i = 0; i < delays.Count; i++){
                if (delays[i].Update(gameTime))
                {
                    delays.RemoveAt(i);
                    i--;
                }
            }
            
        }
    }

    /// <summary>
    /// Basic dleay unit for DelayCoded
    /// </summary>
    class Delay{
        Action lambda;
        float duration;
        public Delay(Action code, float time){
            this.duration= time;
            this.lambda  =code;
        }

        public bool Update(GameTime gameTime){
            if (lambda != null)
            {
                duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (duration <= 0)
                {
                    duration = 0;

                    lambda.Invoke();
                    return true;
                }
            }

            return false;
        }
    }
}
