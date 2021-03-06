﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameLibrary.Helpers
{
    /// <summary>
    /// Contains a Texture2D and a dictionary of source rectangle arrays. Each animation sequence can be accessed using a string key.
    /// </summary>
    public class SpriteSheet
    {
        #region Fields

        private Texture2D texture;

        private Dictionary<string, Rectangle[]> animations = new Dictionary<string, Rectangle[]>();

        #endregion Fields

        #region Operator

        public Rectangle[] this[string key]
        {
            get { return Animations[key]; }
            set { Animations[key] = value; }
        }

        #endregion Operator

        #region Properties

        /// <summary>
        /// The spritesheet's texture.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
        }

        /// <summary>
        /// The animations dictionary.
        /// </summary>
        public Dictionary<string, Rectangle[]> Animations
        {
            get { return animations; }
            set { animations = value; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Makes a new Spritesheet, loading in the texture.
        /// </summary>
        public SpriteSheet(ContentManager content, string filename)
        {
            texture = content.Load<Texture2D>(filename);
        }

        public SpriteSheet(Texture2D spriteSheet)
        {
            texture = spriteSheet;
        }

        #endregion Constructor
    }
}