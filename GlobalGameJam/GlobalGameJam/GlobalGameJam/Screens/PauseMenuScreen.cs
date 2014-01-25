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

        public PauseMenuScreen(GameplayScreen gameplayScreen)
            : base("Paused")
        {
            MenuEntry resume = new MenuEntry("Resume");
            MenuEntry quit = new MenuEntry("Quit");

            this.gameplayScreen = gameplayScreen;
        }

        void resume_Selected(object sender, EventArgs e)
        {
            ExitScreen();
        }

        void quit_Selected(object sender, EventArgs e)
        {
            ExitScreen();
            Manager.RemoveScreen(gameplayScreen);
        }
    }
}
