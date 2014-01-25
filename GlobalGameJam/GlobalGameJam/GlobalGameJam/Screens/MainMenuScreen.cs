using GameLibrary.GameStates.Screens;
using GameLibrary.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Screens
{
    public class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen()
            : base("Bunnies & Goblins")
        {
            MenuEntry playGameEntry;
            MenuEntry quitEntry;

            playGameEntry = new MenuEntry("Play");
            quitEntry = new MenuEntry("Exit");

            playGameEntry.Selected += playGameEntry_Selected;
            quitEntry.Selected += quitEntry_Selected;

            MenuEntries.Add(playGameEntry);
            MenuEntries.Add(quitEntry);
        }

        void playGameEntry_Selected(object sender, PlayerIndexEventArgs e)
        {
            Manager.AddScreen(new GameplayScreen(), null);
        }

        void quitEntry_Selected(object sender, PlayerIndexEventArgs e)
        {
            Manager.Game.Exit();
        }
    }
}
