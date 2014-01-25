using GameLibrary;
using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components.Physics;
using GlobalGameJam.Entities.Systems;
using GlobalGameJam.Entities.Templates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam
{
    class BoblinWorld : World
    {
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
            Entity player = this.CreateEntity("Player");

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


            base.BuildTemplates(Content, args);
        }

        /// <summary>
        /// Builds the systems
        /// </summary>
        protected override void BuildSystems()
        {
            playerControlSystem = this.SystemManager.SetSystem(new PlayerControlSystem(2.235f), GameLibrary.Dependencies.Entities.ExecutionType.Update, 0);


            base.BuildSystems();
        }

        #endregion

        #region Functioning Loop

        #endregion

        #region Properties

        #endregion

        #region Fields
        PlayerControlSystem playerControlSystem;

        #endregion
    }
}
