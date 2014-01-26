using GameLibrary.GameStates.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Screens
{
    public class CreditScreen : TextScreen
    {
        BoblinWorld world;

        public CreditScreen(BoblinWorld world)
            : base("Credits",
            "Code by William Guss & Nathaniel Nelson",
            "Art by Oryx Design Lab & Nathaniel Nelson",
            "Music by tgfcoder from art.devsader.com",
            "Sound effects by bfxr.net",
            "",
            "This game was made in 48 hours",
            "for the 2014 Global Game Jam.",
            "Many thanks to the wonderful developers,",
            "organizers, and volunteers who made",
            "the event such a success!")
        {
            IsPopup = true;

            OnExit += CreditScreen_OnExit;

            this.world = world;
        }

        void CreditScreen_OnExit(object sender, EventArgs e)
        {
            Manager.AddScreen(new MainMenuScreen(world), null);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            world.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            world.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
