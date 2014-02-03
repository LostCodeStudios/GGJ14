using GameLibrary;
using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Collision;
using GameLibrary.Dependencies.Physics.Dynamics;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using GlobalGameJam.Entities;
using GlobalGameJam.Entities.Components;
using GlobalGameJam.Entities.Systems;
using GlobalGameJam.Entities.Templates;
using GlobalGameJam.Entities.Templates.Terrain;
using GlobalGameJam.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceHordes.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam
{
    public class BoblinWorld : World
    {
        private enum GameOverState
        {
            NotYet,
            Dead,
            Transitioning
        }

        public const int FIRST_CATS = 10;
        public const float FIRST_GOBLINS = 0f;
        public const int FIRST_TREES = 75;

        public static int Trees;
        public static int Cats;
        public static float Goblins;

        public static float LAVA_EVIL = 0.7f;

        public const float EVIL_INC = 0.03f;

        public static float BLACKTREE_COEF = 0.98f;
        public const float REDGROUND_COEF = 1f;
        public const float TREE_RATE = 1.3F;
        public const float CAT_RATE = 0.5f;
        public const float GOBLIN_RATE = 3f;
        public const float KILLING_EVIL = 0.3f;

        const float HOUSE_SPACE = 7f;

        public float Evil = 0f;

        public int Hearts = 0;

        public const int TRIGGER_HEARTS = 6;

        public bool GameStarted = false;

        bool theEnd = false;

        float timer;
        GameOverState gameOverState = GameOverState.NotYet;

        MainMenuScreen main;
        GameplayScreen gameplay;

        public GameplayScreen GameplayScreen
        {
            set { gameplay = value; }
        }

        #region Constructors
        public BoblinWorld(Game game, MainMenuScreen main)
            : base(game, Vector2.Zero)
        {
            Cats = FIRST_CATS;
            Goblins = FIRST_GOBLINS;
            Trees = FIRST_TREES;

            FadeToBlack.SpriteBatch = SpriteBatch;

            //Camera.Zoom = 1.3f;
            this.main = main;


#if DEBUG
            Evil = 1f;
#endif
        }
        #endregion

        public Entity player;

        #region Initialization

        /// <summary>
        /// Builds the entitties and the world.
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="args"></param>
        protected override void BuildEntities(Microsoft.Xna.Framework.Content.ContentManager Content, params object[] args)
        {
            //TEST BULLSHIT
        	chunkUpdateSytem.BuildInitial(Vector2.Zero, 3, 3, this);
            //CreateEntity("Goblin", new Vector2(-7, 0)).Refresh();

            ClearArea(Vector2.Zero, HOUSE_SPACE, HOUSE_SPACE);

            house = CreateEntity("House");
            house.Refresh();

            doorBack = CreateEntity("DoorBack");
            doorBack.Refresh();

            #region Player
            //Player
            player = this.CreateEntity("Player", gameplay);
            player.Refresh();

            //camerashit
            Camera.TrackingBody = player.GetComponent<Body>();
            Camera.EnableTracking = true;
            Camera.EnableRotationTracking = false;

            Camera.Zoom = 1.6f;
            #endregion

#if DEBUG && WINDOWS //Debug render system
            this._DebugSystem.LoadContent(SpriteBatch.GraphicsDevice, Content,
                new KeyValuePair<string, object>("Camera", this.Camera),
                 new KeyValuePair<string, object>("EntitySystem Time:\n", this.SystemManager));

#endif

            base.BuildEntities(Content, args);
        }

        /// <summary>
        /// Builds the templates for entitiy creation.
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="args"></param>
        protected override void BuildTemplates(Microsoft.Xna.Framework.Content.ContentManager Content, params object[] args)
        {
            this.SetEntityTemplate("Player", new PlayerTemplate(this, main));
            this.SetEntityTemplate("Cat", new BunnyTemplate(this));
            this.SetEntityTemplate("Blood", new BloodTemplate());
            this.SetEntityTemplate("Heart", new HeartTemplate());

            this.SetEntityTemplate("Goblin", new GoblinTemplate(this));

            this.SetEntityGroupTemplate("Chunk", new ChunkTemplate());
                this.SetEntityTemplate("Grass", new GrassTemplate());
                this.SetEntityTemplate("Tree", new TreeTemplate(this));

            this.SetEntityTemplate("Corpse", new CorpseTemplate());
            this.SetEntityTemplate("House", new HouseTemplate(this));

            this.SetEntityTemplate("DoorBack", new ThresholdTemplate());
            this.SetEntityTemplate("Guide", new GuidesTemplate());

            base.BuildTemplates(Content, args);
        }

        /// <summary>
        /// Builds the systems
        /// </summary>
        protected override void BuildSystems()
        {
            playerControlSystem = this.SystemManager.SetSystem(new PlayerControlSystem(this.Camera), ExecutionType.Update, 0);
            this.SystemManager.SetSystem(new BunnyMovementSystem(), ExecutionType.Update, 0);
            this.SystemManager.SetSystem(new BloodSystem(), ExecutionType.Update, 0);
            this.SystemManager.SetSystem(new DirectionalSpriteSystem(), ExecutionType.Update, 0);
            this.SystemManager.SetSystem(new AnimationSystem(), ExecutionType.Update, 0);
            this.SystemManager.SetSystem(new GoblinSystem(), ExecutionType.Update, 0);
            chunkUpdateSytem = this.SystemManager.SetSystem(new ChunkUpdateSystem(Chunk.SIZE * (float)Math.Sqrt(2)), ExecutionType.Update, 0);

            SystemManager.SetSystem(new ChunkRenderSystem(SpriteBatch, Camera), ExecutionType.Draw, -1);
            SystemManager.SetSystem(new GuideRenderSystem(SpriteBatch, main.Manager.Font), ExecutionType.Draw, 1);

            base.BuildSystems();

            playerControlSystem.Enabled = false;
        }

        bool animating = false;

        Entity doorBack;

        /// <summary>
        /// Starts the game
        /// </summary>
        public void Start()
        {
            GameStarted = true;

            HouseSprite hs = house.GetComponent<HouseSprite>();
            house.RemoveComponent<Sprite>(house.GetComponent<Sprite>());
            house.AddComponent<Sprite>(hs.Open);
            SoundManager.Play("Door");

            Body b = player.GetComponent<Body>();
            b.BodyType = BodyType.Kinematic;
            b.LinearVelocity = new Vector2(0, PlayerControlSystem.PLAYER_WALK_SPEED);

            animating = true;


            if (Game1.ShowGuides)
            {
                CreateEntity("Guide").Refresh();
                Game1.ShowGuides = false;
            }

            Health h = player.GetComponent<Health>();
            h.OnDeath += (e1) =>
            {
                gameOverState = GameOverState.Dead;

                if (Evil < 1f)
                {
                    timer = 1f;
                }
                else
                {
                    timer = 3f;
                    theEnd = true;
                }
            };
        }

        #endregion

        #region Functioning Loop

        float elapsedAnimation = 0f;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (gameOverState == GameOverState.Dead)
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timer <= 0f)
                {
                    gameOverState = GameOverState.Transitioning;
                    timer = FadeToBlack.TRANSITION_TIME;
                    FadeToBlack.SpriteBatch = gameplay.Manager.SpriteBatch;
                    FadeToBlack.Fade(1.25f);
                }
            }

            else if (gameOverState == GameOverState.Transitioning)
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timer <= 0f)
                {
                     main.OnFocus();
                     main.Manager.RemoveScreen(gameplay);
                }
            }

            if (Hearts >= TRIGGER_HEARTS)
            {
                Goblins = 0.5f;
            }

            if (animating)
            {
                elapsedAnimation += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (elapsedAnimation >= 1.5f)
                {
                    Body b = player.GetComponent<Body>();
                    b.BodyType = BodyType.Dynamic;
                    b.LinearVelocity = Vector2.Zero;
                    playerControlSystem.Enabled = true;

                    HouseSprite hs = house.GetComponent<HouseSprite>();
                    house.RemoveComponent<Sprite>(house.GetComponent<Sprite>());
                    house.AddComponent<Sprite>(hs.Closed);
                    SoundManager.Play("Door");

                    doorBack.Delete();
                    animating = false;
                }
            }
        }

        public float EnergyBar = -1;
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (EnergyBar != -1)
            {
                SpriteBatch.Begin();

                Rectangle source = ScreenHelper.SpriteSheet.Animations["healthBarFrame"][0];

                Vector2 position = new Vector2(ScreenHelper.Viewport.Width / 2, 7 * ScreenHelper.Viewport.Height / 8);
                position -= new Vector2(source.Width / 2, source.Height / 2);




                SpriteBatch.Draw(ScreenHelper.SpriteSheet.Texture, position, source, Color.White);

                source = ScreenHelper.SpriteSheet.Animations["healthBarFill"][0];



                Rectangle dest = new Rectangle((int)position.X + 4, (int)position.Y + 4, (int)(source.Width * EnergyBar), source.Height);

                SpriteBatch.Draw(ScreenHelper.SpriteSheet.Texture, dest, source, Color.White);


                SpriteBatch.End();
            }

            if (theEnd)
            {
                Vector2 size = main.Manager.TitleFont.MeasureString("The End");

                SpriteBatch.Begin();
                SpriteBatch.DrawString(main.Manager.TitleFont, "The End",
                    new Vector2(ScreenHelper.Viewport.Width / 2, ScreenHelper.Viewport.Height / 2), Color.Yellow, 0f, size / 2, 1f, SpriteEffects.None, 1f);
                SpriteBatch.End();
            }
        }

        #endregion

        #region Properties

        #endregion

        public void ClearArea(Vector2 center, float width, float height)
        {
            AABB box = new AABB(center, width, height);

            QueryAABB(
                (f) =>
                {
                    (f.Body.UserData as Entity).Delete();

                    return true;
                }, ref box);
        }

        Entity house;

        #region Fields
        public PlayerControlSystem playerControlSystem;
        public ChunkUpdateSystem chunkUpdateSytem;

        
        #endregion
    }
}
