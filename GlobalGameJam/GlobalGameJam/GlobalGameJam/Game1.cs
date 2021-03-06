using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameLibrary.GameStates;
using GlobalGameJam.Screens;
using GameLibrary.Helpers;

namespace GlobalGameJam
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;

        public static bool ShowGuides = true;

        public const float MUSIC_VOLUME = 0.8f;

        public Game1()
        {
            Window.Title = "Kittens & Kobolds";
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";

            graphics.PreferMultiSampling = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;

            graphics.ApplyChanges();

            IsFixedTimeStep = true;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            ScreenHelper.Initialize(graphics, GraphicsDevice);
            ConvertUnits.SetDisplayUnitToSimUnitRatio(32f);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            FadeToBlack.SpriteBatch = spriteBatch;

            SpriteSheet spriteSheet = new SpriteSheet(Content.Load<Texture2D>("Textures/spritesheet"));

            sourceRectangles(spriteSheet);
            ScreenHelper.SpriteSheet = spriteSheet;
            ScreenHelper.BlankTexture = Content.Load<Texture2D>("Textures/blank");

            screenManager = new ScreenManager(this);
            screenManager.Initialize();

            screenManager.AddScreen(new MainMenuScreen(), null);
            sounds();

            base.LoadContent();

            MusicManager.Volume = MUSIC_VOLUME;
            MusicManager.IsRepeating = true;
            MusicManager.PlaySong("Music");
        }

        void sounds()
        {
            SoundManager.Volume = 1f;
            SoundManager.Add("Heart", Content.Load<SoundEffect>("Sounds/Powerup35"));
            SoundManager.Add("Hit", Content.Load<SoundEffect>("Sounds/Hit_Hurt59"));
            SoundManager.Add("Door", Content.Load<SoundEffect>("Sounds/Hit_Hurt90"));
            SoundManager.Add("Dash", Content.Load<SoundEffect>("Sounds/Explosion30"));
            SoundManager.Add("Kobold", Content.Load<SoundEffect>("Sounds/Randomize56"));
            SoundManager.Add("Footstep", Content.Load<SoundEffect>("Sounds/yuval_grass-loop-short"));

            MusicManager.AddSong("Music", Content.Load<Song>("Music/AHeroisBorn_tgfcoder"));
        }

        void sourceRectangles(SpriteSheet spriteSheet)
        {
            spriteSheet.Animations.Add("bunny", new Rectangle[] { new Rectangle(6, 8, 23, 20) });

            #region Blood

            spriteSheet.Animations.Add("blood1", new Rectangle[] { new Rectangle(159, 335, 3, 3) });
            spriteSheet.Animations.Add("blood2", new Rectangle[] { new Rectangle(180, 327, 21, 17) });
            spriteSheet.Animations.Add("blood3", new Rectangle[] { new Rectangle(212, 331, 14, 11) });
            spriteSheet.Animations.Add("blood4", new Rectangle[] { new Rectangle(233, 328, 25, 17) });
            spriteSheet.Animations.Add("blood5", new Rectangle[] { new Rectangle(262, 328, 25, 21) });

            #endregion

            spriteSheet.Animations.Add("tree1", new Rectangle[] { new Rectangle(277, 99, 32, 32) });
            spriteSheet.Animations.Add("tree2", new Rectangle[] { new Rectangle(317, 98, 32, 32) });
            spriteSheet.Animations.Add("tree3", new Rectangle[] { new Rectangle(363, 86, 40, 36) });
            spriteSheet.Animations.Add("tree4", new Rectangle[] { new Rectangle(415, 90, 36, 32) });

            spriteSheet.Animations.Add("goblin", new Rectangle[] { new Rectangle(39, 58, 12, 26) });

            #region Tiles

            spriteSheet.Animations.Add("grass1", new Rectangle[] { new Rectangle(179, 227, 32, 32) });
            spriteSheet.Animations.Add("grass2", new Rectangle[] { new Rectangle(179, 263, 32, 32) });

            spriteSheet.Animations.Add("water1", new Rectangle[] { new Rectangle(217, 227, 32, 32) });
            spriteSheet.Animations.Add("water2", new Rectangle[] { new Rectangle(217, 263, 32, 32) });

            spriteSheet.Animations.Add("redGround1", new Rectangle[] { new Rectangle(281, 227, 32, 32) });
            spriteSheet.Animations.Add("redGround2", new Rectangle[] { new Rectangle(281, 263, 32, 32) });

            spriteSheet.Animations.Add("lava1", new Rectangle[] { new Rectangle(143, 227, 32, 32) });
            spriteSheet.Animations.Add("lava2", new Rectangle[] { new Rectangle(143, 263, 32, 32) });

            spriteSheet.Animations.Add("grayGround1", new Rectangle[] { new Rectangle(320, 227, 32, 32) });
            spriteSheet.Animations.Add("grayGround2", new Rectangle[] { new Rectangle(320, 263, 32, 32) });

            spriteSheet.Animations.Add("grayWater1", new Rectangle[] { new Rectangle(358, 227, 32, 32) });
            spriteSheet.Animations.Add("grayWater2", new Rectangle[] { new Rectangle(358, 263, 32, 32) });

            spriteSheet.Animations.Add("grayLava1", new Rectangle[] { new Rectangle(396, 227, 32, 32) });
            spriteSheet.Animations.Add("grayLava2", new Rectangle[] { new Rectangle(396, 263, 32, 32) });

            #endregion

            spriteSheet.Animations.Add("heart", new Rectangle[] { new Rectangle(178, 63, 22, 22) });

            spriteSheet.Animations.Add("corpse", new Rectangle[] { new Rectangle(7, 404, 32, 20) });

            spriteSheet.Animations.Add("house", new Rectangle[] { new Rectangle(162, 373, 120, 108) });
            spriteSheet.Animations.Add("houseOpen", new Rectangle[] { new Rectangle(302, 373, 120, 108) });
            spriteSheet.Animations.Add("doorBack", new Rectangle[] { new Rectangle(433, 441, 32, 40) });

            spriteSheet.Animations.Add("healthBarFrame", new Rectangle[] { new Rectangle(244, 21, 180, 24) });
            spriteSheet.Animations.Add("healthBarFill", new Rectangle[] { new Rectangle(248, 57, 172, 16) });

            #region Player
            spriteSheet.Animations.Add("playerMoveRight", new Rectangle[] { 
                new Rectangle(42, 248, 32, 32),
                new Rectangle(78, 248, 32, 32) 
            });

            spriteSheet.Animations.Add("playerMoveDown", new Rectangle[] { 
                new Rectangle(36, 284, 32, 32),
                new Rectangle(68, 284, 32, 32)
            });

            spriteSheet.Animations.Add("playerMoveLeft", new Rectangle[] { 
                new Rectangle(40, 320, 32, 32),
                new Rectangle(76, 320, 32, 32)
            });

            spriteSheet.Animations.Add("playerMoveUp", new Rectangle[] { 
                new Rectangle(38, 358, 32, 32),
                new Rectangle(70, 358, 32, 32)
            });

            spriteSheet.Animations.Add("playerRight", new Rectangle[] { 
                new Rectangle(6, 248, 32, 32) 
            });

            spriteSheet.Animations.Add("playerDown", new Rectangle[] { 
                new Rectangle(6, 284, 32, 32)
            });

            spriteSheet.Animations.Add("playerLeft", new Rectangle[] { 
                new Rectangle(5, 320, 32, 32)
            });

            spriteSheet.Animations.Add("playerUp", new Rectangle[] { 
                new Rectangle(4, 358, 32, 32)
            });
            #endregion

            #region Cats

            #region white
            spriteSheet.Animations.Add("catMoveRight", new Rectangle[] { 
                new Rectangle(185, 113, 24, 21),
                new Rectangle(212, 113, 24, 21)
            });

            spriteSheet.Animations.Add("catMoveDown", new Rectangle[] { 
                new Rectangle(179, 136, 18, 21),
                new Rectangle(203, 136, 18, 21)
            });

            spriteSheet.Animations.Add("catMoveLeft", new Rectangle[] { 
                new Rectangle(185, 162, 24, 21),
                new Rectangle(212, 162, 24, 21)
            });

            spriteSheet.Animations.Add("catMoveUp", new Rectangle[] { 
                new Rectangle(184, 187, 18, 21),
                new Rectangle(208, 187, 18, 21)
            });

            spriteSheet.Animations.Add("catRight", new Rectangle[] { 
                new Rectangle(158, 113, 24, 21) 
            });

            spriteSheet.Animations.Add("catDown", new Rectangle[] { 
                new Rectangle(158, 136, 18, 21)
            });

            spriteSheet.Animations.Add("catLeft", new Rectangle[] { 
                new Rectangle(158, 162, 24, 21)
            });

            spriteSheet.Animations.Add("catUp", new Rectangle[] { 
                new Rectangle(160, 187, 18, 21)
            });
            #endregion

            #region black

            spriteSheet.Animations.Add("blackMoveRight", new Rectangle[] { 
                new Rectangle(185+316, 113-93, 24, 21),
                new Rectangle(212+316, 113-93, 24, 21)
            });

            spriteSheet.Animations.Add("blackMoveDown", new Rectangle[] { 
                new Rectangle(179+316, 136-93, 18, 21),
                new Rectangle(203+316, 136-93, 18, 21)
            });

            spriteSheet.Animations.Add("blackMoveLeft", new Rectangle[] { 
                new Rectangle(185+316, 162-93, 24, 21),
                new Rectangle(212+316, 162-93, 24, 21)
            });

            spriteSheet.Animations.Add("blackMoveUp", new Rectangle[] { 
                new Rectangle(184+316, 187-93, 18, 21),
                new Rectangle(208+316, 187-93, 18, 21)
            });

            spriteSheet.Animations.Add("blackRight", new Rectangle[] { 
                new Rectangle(158+316, 113-93, 24, 21) 
            });

            spriteSheet.Animations.Add("blackDown", new Rectangle[] { 
                new Rectangle(158+316, 136-93, 18, 21)
            });

            spriteSheet.Animations.Add("blackLeft", new Rectangle[] { 
                new Rectangle(158+316, 162-93, 24, 21)
            });

            spriteSheet.Animations.Add("blackUp", new Rectangle[] { 
                new Rectangle(160+316, 187-93, 18, 21)
            });
            #endregion




            #endregion

            #region Goblins

            spriteSheet.Animations.Add("goblinMoveRight", new Rectangle[] { 
                new Rectangle(39, 98, 32, 32),
                new Rectangle(75, 98, 32, 32) 
            });

            spriteSheet.Animations.Add("goblinMoveDown", new Rectangle[] { 
                new Rectangle(36, 135, 32, 32),
                new Rectangle(72, 135, 32, 32)
            });

            spriteSheet.Animations.Add("goblinMoveLeft", new Rectangle[] { 
                new Rectangle(40, 171, 32, 32),
                new Rectangle(76, 171, 32, 32)
            });

            spriteSheet.Animations.Add("goblinMoveUp", new Rectangle[] { 
                new Rectangle(39, 207, 32, 32),
                new Rectangle(75, 207, 32, 32)
            });

            spriteSheet.Animations.Add("goblinRight", new Rectangle[] { 
                new Rectangle(39, 98, 32, 32)
            });

            spriteSheet.Animations.Add("goblinDown", new Rectangle[] { 
                new Rectangle(2, 135, 32, 32)
            });

            spriteSheet.Animations.Add("goblinLeft", new Rectangle[] { 
                new Rectangle(40, 171, 32, 32)
            });

            spriteSheet.Animations.Add("goblinUp", new Rectangle[] { 
                new Rectangle(3, 207, 32, 32)
            });

            #endregion

            spriteSheet.Animations.Add("WASD", new Rectangle[] { new Rectangle(275, 180, 184, 120) });
            spriteSheet.Animations.Add("Mouse", new Rectangle[] { new Rectangle(499, 205, 56, 96) });

            spriteSheet.Animations.Add("LeftStick", new Rectangle[] { new Rectangle(500, 336, 56, 56) });
            spriteSheet.Animations.Add("AnyButton", new Rectangle[] { new Rectangle(534, 397, 68, 68) });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            screenManager.Update(gameTime);

            DelayCode.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            screenManager.Draw(gameTime);

            FadeToBlack.Update(gameTime);

            base.Draw(gameTime);
        }
    }
}
