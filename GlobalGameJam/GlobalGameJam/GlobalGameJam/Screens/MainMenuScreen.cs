using GameLibrary.GameStates.Screens;
using GameLibrary.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Screens
{
    public class MainMenuScreen : MenuScreen
    {
        BoblinWorld world;
        public MainMenuScreen()
            : base("Cats & Goblins")
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
            Manager.AddScreen(new GameplayScreen(world, this), null);
        }

        void quitEntry_Selected(object sender, PlayerIndexEventArgs e)
        {
            Manager.Game.Exit();
        }


        public override void Activate()
        {
            base.Activate();

            OnFocus();
        }

        public override void OnFocus()
        {
            world = new BoblinWorld(Manager.Game);
            world.Initialize();
            world.LoadContent(Manager.Game.Content);
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (!otherScreenHasFocus)
            {
                world.Update(gameTime);
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            world.Draw(gameTime);
            base.Draw(gameTime);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
            base.Draw(gameTime);
        }
    }
}
