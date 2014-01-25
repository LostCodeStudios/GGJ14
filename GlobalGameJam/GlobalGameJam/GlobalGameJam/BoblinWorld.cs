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
        public static int FIRST_CATS = 5;
        public static int FIRST_GOBLINS = 0;

        #region Constructors
        public BoblinWorld(Game game)
            : base(game, Vector2.Zero)
        {

        }
        #endregion

        #region Initialization

        /// <summary>
        /// Builds the entitties and the world.
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="args"></param>
        protected override void BuildEntities(Microsoft.Xna.Framework.Content.ContentManager Content, params object[] args)
        {
            #region Player
            //Player
            Entity player = this.CreateEntity("Player", Content.Load<Texture2D>("Textures/player"), new Rectangle(15, 30, 50, 30));
            player.Refresh();

            //camerashit
            Camera.TrackingBody = player.GetComponent<Body>();
            Camera.EnableTracking = true;
            Camera.EnableRotationTracking = false;
            #endregion

            //TEST BULLSHIT
            chunkUpdateSytem.BuildInitial(Vector2.Zero, 3, 3, FIRST_CATS, FIRST_GOBLINS);
            //CreateEntity("Goblin", new Vector2(-7, 0)).Refresh();

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

            this.SetEntityTemplate("Goblin", new GoblinTemplate(this));

            this.SetEntityGroupTemplate("Chunk", new ChunkTemplate());
                this.SetEntityTemplate("Grass", new GrassTemplate());
                this.SetEntityTemplate("Tree", new TreeTemplate(this));

            this.SetEntityTemplate("Corpse", new CorpseTemplate());

            base.BuildTemplates(Content, args);
        }

        /// <summary>
        /// Builds the systems
        /// </summary>
        protected override void BuildSystems()
        {
            playerControlSystem = this.SystemManager.SetSystem(new PlayerControlSystem(this.Camera), ExecutionType.Update, 0);
            this.SystemManager.SetSystem(new BunnyMovementSystem(), ExecutionType.Update, 0);
            //this.SystemManager.SetSystem(new BloodSystem(), ExecutionType.Update, 0);
            this.SystemManager.SetSystem(new DirectionalSpriteSystem(), ExecutionType.Update, 0);
            this.SystemManager.SetSystem(new AnimationSystem(), ExecutionType.Update, 0);
            this.SystemManager.SetSystem(new GoblinSystem(), ExecutionType.Update, 0);
            chunkUpdateSytem = this.SystemManager.SetSystem(new ChunkUpdateSystem(Chunk.SIZE * (float)Math.Sqrt(2)), ExecutionType.Update, 0);

            SystemManager.SetSystem(new ChunkRenderSystem(SpriteBatch, Camera), ExecutionType.Draw, -1);

            base.BuildSystems();
        }

        #endregion

        #region Functioning Loop

        #endregion

        #region Properties

        #endregion

        #region Fields
        PlayerControlSystem playerControlSystem;
        public ChunkUpdateSystem chunkUpdateSytem;
        #endregion
    }
}
