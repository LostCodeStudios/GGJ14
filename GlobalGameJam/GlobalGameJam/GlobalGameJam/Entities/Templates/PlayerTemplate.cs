using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Factories;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using GlobalGameJam.Entities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates
{
    class PlayerTemplate : IEntityTemplate
    {
        BoblinWorld _World;
        public PlayerTemplate(BoblinWorld world)
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

            Health h = new Health(3f);
            h.OnDeath += GenericEvents.BloodyDeath(_World);
            e.GetComponent<Body>().OnCollision += GenericEvents.BasicCollision();

            e.AddComponent<Health>(h);

            return e;
        }
    }
}
