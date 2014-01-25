using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components;
using GameLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Components
{
    public class DirectionalSprite : Component
    {
        public string SpritePrefix;

        public Sprite LeftSprite;
        public Sprite RightSprite;
        public Sprite DownSprite;
        public Sprite UpSprite;

        public DirectionalSprite(string prefix)
        {
            SpritePrefix = prefix;

            LeftSprite = new Sprite(ScreenHelper.SpriteSheet, prefix + "Left", 0.5f);
            RightSprite = new Sprite(ScreenHelper.SpriteSheet, prefix + "Right", 0.5f);
            DownSprite = new Sprite(ScreenHelper.SpriteSheet, prefix + "Down", 0.5f);
            UpSprite = new Sprite(ScreenHelper.SpriteSheet, prefix + "Up", 0.5f);
        }
    }
}
