using GameLibrary;
using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components.Physics;
using GlobalGameJam.Entities;
using GlobalGameJam.Entities.Systems;
using GlobalGameJam.Entities.Templates;
using GlobalGameJam.Entities.Templates.Terrain;
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
        public static int FIRST_CATS = 10;
        public static float FIRST_GOBLINS = 0f;
        public static int FIRST_TREES = 40;


        public const float EVIL_INC = 0.03f;

        public const float LAVA_CHANCE = 0.09f;
        public const float REDGROUND_COEF = 1f;
        public const float TREE_RATE = 1.25f;
        public const float CAT_RATE = 1.5f;
        public const float GOBLIN_RATE = 3f;
        public const float KILLING_EVIL = 0.3f;

        public float Evil = 0f;

        public int Hearts = 0;

        public const int TRIGGER_HEARTS = 1;

        #region Constructors
        public BoblinWorld(Game game)
            : base(game, Vector2.Zero)
        {
            FIRST_CATS = 5;
            FIRST_GOBLINS = 0f;
            FIRST_TREES = 25;
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

            house = CreateEntity("House");
            house.Refresh();

            #region Player
            //Player
            player = this.CreateEntity("Player");
            player.Refresh();

            //camerashit
            Camera.TrackingBody = player.GetComponent<Body>();
            Camera.EnableTracking = true;
            Camera.EnableRotationTracking = false;
            #endregion

            base.BuildEntities(Content, args);
        }

        /// <summary>
        /// Builds the templates for entitiy creation.
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="args"></param>
        protected override void BuildTemplates(Microsoft.Xna.Framework.Content.ContentManager Content, params object[] args)
        {
            this.SetEntityTemplate("Player", new PlayerTemplate(this));
            this.SetEntityTemplate("Cat", new BunnyTemplate(this));
            this.SetEntityTemplate("Blood", new BloodTemplate());
            this.SetEntityTemplate("Heart", new HeartTemplate());

            this.SetEntityTemplate("Goblin", new GoblinTemplate(this));

            this.SetEntityGroupTemplate("Chunk", new ChunkTemplate());
                this.SetEntityTemplate("Grass", new GrassTemplate());
                this.SetEntityTemplate("Tree", new TreeTemplate(this));

            this.SetEntityTemplate("Corpse", new CorpseTemplate());
            this.SetEntityTemplate("House", new HouseTemplate(this));

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

            base.BuildSystems();

            playerControlSystem.Enabled = false;
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void Start()
        {
            playerControlSystem.Enabled = true;
        }

        #endregion

        #region Functioning Loop

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Hearts >= TRIGGER_HEARTS)
            {
                FIRST_GOBLINS = 0.5f;
            }
        }

        #endregion

        #region Properties

        #endregion

        Entity house;

        #region Fields
        PlayerControlSystem playerControlSystem;
        public ChunkUpdateSystem chunkUpdateSytem;
        #endregion
    }
}
