using GameLibrary.Dependencies.Entities;
using GameLibrary.Helpers;
using GlobalGameJam.Entities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Systems
{
    public class GuideRenderSystem : EntityProcessingSystem
    {
        const float MESSAGE_X = 1 / 9f;
        const float MESSAGE_Y = 8 / 9f;

        SpriteBatch spriteBatch;
        SpriteFont font;

        string leftMessage = "Move";
        string rightMessage = "Dash";

        Vector2 offLeft;
        Vector2 offRight;

        Vector2 leftMessageSize;
        Vector2 rightMessageSize;

        Vector2 keyPos;
        Vector2 mousePos;

        public GuideRenderSystem(SpriteBatch spriteBatch, SpriteFont font)
            : base(typeof(GuideTimer))
        {
            this.spriteBatch = spriteBatch;
            this.font = font;

            leftMessageSize = font.MeasureString(leftMessage);
            rightMessageSize = font.MeasureString(rightMessage);

            offLeft = new Vector2(-leftMessageSize.X, MESSAGE_Y * ScreenHelper.Viewport.Height);
            offRight = new Vector2(ScreenHelper.Viewport.Width, MESSAGE_Y * ScreenHelper.Viewport.Height);

            keyPos = new Vector2(MESSAGE_X * ScreenHelper.Viewport.Width, MESSAGE_Y * ScreenHelper.Viewport.Height);
            mousePos = new Vector2((1 - MESSAGE_X) * ScreenHelper.Viewport.Width - rightMessageSize.X, MESSAGE_Y * ScreenHelper.Viewport.Height);
        }

        public override void Process(Entity e)
        {
            float elapsedTime = (float)(world.Delta / 1000f);

            GuideTimer timer = e.GetComponent<GuideTimer>();

            timer.ElapsedTime += elapsedTime;

            spriteBatch.Begin();
            if (timer.ElapsedTime < GuideTimer.TRANSITION_TIME)
            {
                //transition on
                float progress = timer.ElapsedTime / GuideTimer.TRANSITION_TIME;

                Vector2 leftPos = offLeft + (keyPos - offLeft) * progress;
                spriteBatch.DrawString(font, leftMessage, leftPos, Color.White);
                Rectangle source = ScreenHelper.SpriteSheet.Animations["WASD"][0];
                Vector2 offset = new Vector2(-source.Width / 2 + leftMessageSize.X / 2, -source.Height);
                spriteBatch.Draw(ScreenHelper.SpriteSheet.Texture, leftPos + offset, source, Color.White);

                Vector2 rightPos = offRight + (mousePos - offRight) * progress;
                spriteBatch.DrawString(font, rightMessage, rightPos, Color.White);
                source = ScreenHelper.SpriteSheet.Animations["Mouse"][0];
                offset = new Vector2(-source.Width / 2 + rightMessageSize.X / 2, -source.Height);
                spriteBatch.Draw(ScreenHelper.SpriteSheet.Texture, rightPos + offset, source, Color.White);
            }
            else if (timer.ElapsedTime > GuideTimer.GUIDE_DURATION - GuideTimer.TRANSITION_TIME)
            {
                //transition off
                float progress = 1f - ((GuideTimer.GUIDE_DURATION - timer.ElapsedTime) / GuideTimer.TRANSITION_TIME);

                Vector2 leftPos = keyPos + (offLeft - keyPos) * progress;
                spriteBatch.DrawString(font, leftMessage, leftPos, Color.White);
                Rectangle source = ScreenHelper.SpriteSheet.Animations["WASD"][0];
                Vector2 offset = new Vector2(-source.Width / 2 + leftMessageSize.X / 2, -source.Height);
                spriteBatch.Draw(ScreenHelper.SpriteSheet.Texture, leftPos + offset, source, Color.White);

                Vector2 rightPos = mousePos + (offRight - mousePos) * progress;
                spriteBatch.DrawString(font, rightMessage, rightPos, Color.White);
                source = ScreenHelper.SpriteSheet.Animations["Mouse"][0];
                offset = new Vector2(-source.Width / 2 + rightMessageSize.X / 2, -source.Height);
                spriteBatch.Draw(ScreenHelper.SpriteSheet.Texture, rightPos + offset, source, Color.White);
            }
            else
            {
                //just draw

                spriteBatch.DrawString(font, leftMessage, keyPos, Color.White);
                Rectangle source = ScreenHelper.SpriteSheet.Animations["WASD"][0];
                Vector2 offset = new Vector2(-source.Width / 2 + leftMessageSize.X / 2, -source.Height);
                spriteBatch.Draw(ScreenHelper.SpriteSheet.Texture, keyPos + offset, source, Color.White);

                spriteBatch.DrawString(font, rightMessage, mousePos, Color.White);
                source = ScreenHelper.SpriteSheet.Animations["Mouse"][0];
                offset = new Vector2(-source.Width / 2 + rightMessageSize.X / 2, -source.Height);
                spriteBatch.Draw(ScreenHelper.SpriteSheet.Texture, mousePos + offset, source, Color.White);
            }
            spriteBatch.End();
        }
    }
}
