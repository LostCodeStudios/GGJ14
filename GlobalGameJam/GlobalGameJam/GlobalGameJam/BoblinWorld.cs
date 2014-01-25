using GameLibrary;
using GlobalGameJam.Entities.Systems;
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
            base.BuildEntities(Content, args);
        }

        /// <summary>
        /// Builds the templates for entitiy creation.
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="args"></param>
        protected override void BuildTemplates(Microsoft.Xna.Framework.Content.ContentManager Content, params object[] args)
        {
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
