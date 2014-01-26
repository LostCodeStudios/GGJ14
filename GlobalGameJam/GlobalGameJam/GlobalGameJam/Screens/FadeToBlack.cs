using GameLibrary.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Screens
{
    public static class FadeToBlack
    {
        const float TRANSITION_TIME = 0.25f;

        static float elapsedTime = 0f;
        static float duration = 0f;

        public static SpriteBatch SpriteBatch
        {
            get;
            set;
        }

        public static void Fade(float time)
        {
            duration = time;
        }

        public static void Update(GameTime gameTime)
        {
            if (duration > 0)
            {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTime >= duration)
                {
                    duration = 0;
                    return;
                }

                if (elapsedTime < TRANSITION_TIME)
                {
                    float alpha = 1 * elapsedTime / TRANSITION_TIME;

                    SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    SpriteBatch.Draw(ScreenHelper.BlankTexture, ScreenHelper.Viewport.Bounds, Color.Black * alpha);
                    SpriteBatch.End();
                }
                else if (elapsedTime > duration - TRANSITION_TIME)
                {
                    float alpha = 1 * ((duration - elapsedTime) / TRANSITION_TIME);

                    SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    SpriteBatch.Draw(ScreenHelper.BlankTexture, ScreenHelper.Viewport.Bounds, Color.Black * alpha);
                    SpriteBatch.End();
                }
                else
                {
                    SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    SpriteBatch.Draw(ScreenHelper.BlankTexture, ScreenHelper.Viewport.Bounds, Color.Black);
                    SpriteBatch.End();
                }
            }
        }
    }
}
