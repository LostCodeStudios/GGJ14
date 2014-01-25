using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Factories;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates
{
    class PlayerTemplate : IEntityTemplate
    {
        EntityWorld _World;
        public PlayerTemplate(EntityWorld world)
        {
            this._World = world;
        }

        public Entity BuildEntity(Entity e, params object[] args)
        {
            e.Tag = "Player";
            Rectangle source = (Rectangle)args[1];


            FixtureFactory.AttachCircle(ConvertUnits.ToSimUnits(source.Height / 2), 1f, e.AddComponent<Body>(new Body(_World, e)));
            e.GetComponent<Body>().BodyType = GameLibrary.Dependencies.Physics.Dynamics.BodyType.Dynamic;
            e.GetComponent<Body>().FixedRotation = true;

            e.AddComponent<Sprite>(new Sprite(args[0] as Texture2D, source, new Vector2(30, 16), 1f, Color.White, 0.1f)); //Sprite

            return e;
        }
    }
}
