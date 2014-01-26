using GameLibrary.GameStates.Screens;
using GameLibrary.Helpers;
using GameLibrary.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
            : base("Kittens & Kobolds")
        {
            MenuEntry playGameEntry;
            MenuEntry quitEntry;

            playGameEntry = new MenuEntry("Play");
            quitEntry = new MenuEntry("Exit");

            playGameEntry.Selected += playGameEntry_Selected;
            quitEntry.Selected += quitEntry_Selected;

            MenuEntries.Add(playGameEntry);
            MenuEntries.Add(quitEntry);

            menuCancel = new InputAction(new Buttons[] { }, new Keys[] { }, true);
        }

        void playGameEntry_Selected(object sender, PlayerIndexEventArgs e)
        {
            GameplayScreen screen = new GameplayScreen(world, this);
            world.GameplayScreen = screen;
            Manager.AddScreen(screen, null);
            world.Start();
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
            world = new BoblinWorld(Manager.Game, this);
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

            Manager.SpriteBatch.Begin();
            Vector2 position = new Vector2(ScreenHelper.Viewport.Width / 14, 7 * ScreenHelper.Viewport.Height / 8);
            string soundMessage = "(S)ound: " + (SoundManager.Volume > 0 ? "On" : "Off");
            Manager.SpriteBatch.DrawString(Manager.Font, soundMessage, position, Color.White);
            Manager.SpriteBatch.End();
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);

            PlayerIndex idx;
            if (input.IsKeyPressed(Keys.S, null, out idx))
            {
                if (SoundManager.Volume != 0)
                {
                    SoundManager.Volume = 0;
                    MusicManager.Volume = 0;
                }
                else
                {
                    SoundManager.Volume = 1;
                    MusicManager.Volume = 1;
                }
            }
        }
    }
}
