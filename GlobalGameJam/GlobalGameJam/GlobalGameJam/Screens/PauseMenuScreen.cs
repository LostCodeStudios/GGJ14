using GameLibrary.GameStates.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Screens
{
    public class PauseMenuScreen : MenuScreen
    {
        GameplayScreen gameplayScreen;
        private MainMenuScreen main;

        public PauseMenuScreen(GameplayScreen gameplayScreen, MainMenuScreen main)
            : base("Paused")
        {
            MenuEntry resume = new MenuEntry("Resume");
            MenuEntry quit = new MenuEntry("Quit");
            this.main = main;

            this.gameplayScreen = gameplayScreen;

            resume.Selected += resume_Selected;
            quit.Selected += quit_Selected;

            MenuEntries.Add(resume);
            MenuEntries.Add(quit);
        }

        void resume_Selected(object sender, EventArgs e)
        {
            ExitScreen();
        }

        void quit_Selected(object sender, EventArgs e)
        {
            main.OnFocus();
            ExitScreen();
            Manager.RemoveScreen(gameplayScreen);
        }
    }
}
