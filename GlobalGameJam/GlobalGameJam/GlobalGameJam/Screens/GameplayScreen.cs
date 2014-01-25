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
        public BoblinWorld World;
        MainMenuScreen mainMenu;

        public GameplayScreen(BoblinWorld world, MainMenuScreen mainMenu)
        {
            this.World = world;
            this.mainMenu = mainMenu;
            world.Start();
        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (!otherScreenHasFocus)
            {
                World.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            World.Draw(gameTime);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            PlayerIndex index;
            if (input.IsKeyPressed(Keys.Escape, null, out index))
            {
                Manager.AddScreen(new PauseMenuScreen(this, mainMenu), null);
            }
        }
    }
}
