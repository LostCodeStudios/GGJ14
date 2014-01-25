using GameLibrary.GameStates;
using GameLibrary.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Screens
{
    public class GameplayScreen : GameScreen
    {
        BoblinWorld world;

        public GameplayScreen()
        {
            world = new BoblinWorld();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (!otherScreenHasFocus)
            {
                world.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            world.Draw(gameTime);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            PlayerIndex index;
            if (input.IsKeyPressed(Keys.Escape, null, out index))
            {
                Manager.AddScreen(new PauseMenuScreen(this), null);
            }
        }
    }
}
