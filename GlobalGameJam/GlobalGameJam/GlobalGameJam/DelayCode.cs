using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam
{
    public static class DelayCode
    {
       
        public static List<Delay> Delays = new List<Delay>();

        public static void Delay(Action code, float time)
        {
            Delays.Add(new Delay(code, time));
        }

        public static void Update(GameTime gameTime)
        {
            for(int i = 0; i < Delays.Count; i++){
                if (Delays[i].Update(gameTime))
                {
                    Delays.RemoveAt(i);
                    i--;
                }
            }
            
        }
    }

    /// <summary>
    /// Basic dleay unit for DelayCoded
    /// </summary>
    public class Delay{
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
