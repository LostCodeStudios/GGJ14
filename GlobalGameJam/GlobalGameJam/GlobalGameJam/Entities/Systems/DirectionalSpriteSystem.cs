using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components.Physics;
using GlobalGameJam.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Systems
{
    class DirectionalSpriteSystem : EntityProcessingSystem
    {
        public DirectionalSpriteSystem()
            : base(typeof(DirectionalSprite))
        {

        }

        public override void Process(Entity e)
        {
            DirectionalSprite ds = e.GetComponent<DirectionalSprite>();

            Body b = e.GetComponent<Body>();


        }
    }
}
