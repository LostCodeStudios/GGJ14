using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components;
using GameLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Components
{
    public class HouseSprite : Component
    {
        public Sprite Closed;
        public Sprite Open;

        public HouseSprite()
        {
            Closed = new Sprite(ScreenHelper.SpriteSheet, "house", 1f);
            Open = new Sprite(ScreenHelper.SpriteSheet, "houseOpen", 1f);
        }
    }
}
