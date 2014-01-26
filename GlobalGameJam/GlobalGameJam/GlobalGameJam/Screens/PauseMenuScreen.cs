using GameLibrary.GameStates.Screens;
using GameLibrary.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Screens
{
    public class PauseMenuScreen : MenuScreen
    {

        private MainMenuScreen main;
        private BoblinWorld world;
        private GameLibrary.GameStates.GameScreen gameplayScreen;

        public PauseMenuScreen(BoblinWorld  world, GameplayScreen gameplayScreen,  MainMenuScreen main)
            : base("Paused")
        {
            MenuEntry resume = new MenuEntry("Resume");
            MenuEntry quit = new MenuEntry("Quit");
            this.main = main;
            this.world = world;

            this.gameplayScreen = gameplayScreen;

            resume.Selected += resume_Selected;
            quit.Selected += quit_Selected;

            MenuEntries.Add(resume);
            MenuEntries.Add(quit);

            this.menuCancel = new InputAction(
                new Buttons[] { Buttons.Start }, 
                new Keys[] { Keys.Escape }, true);
        }


        public override void Draw(GameTime gameTime)
        {
            world.Draw(gameTime);
            base.Draw(gameTime);
        }

        void resume_Selected(object sender, EventArgs e)
        {
            ExitScreen();
        }

        void quit_Selected(object sender, EventArgs e)
        {
            FadeToBlack.SpriteBatch = Manager.SpriteBatch;
            FadeToBlack.Fade(1.25f);

            DelayCode.Delay(
                        () =>
                        {
                            main.OnFocus();
                            ExitScreen();
                            Manager.RemoveScreen(gameplayScreen);
                        }, FadeToBlack.TRANSITION_TIME);
            
        }
    }
}
