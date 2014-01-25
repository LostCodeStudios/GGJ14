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

            DirectionalSprite sprite = new DirectionalSprite("player");
            e.AddComponent<DirectionalSprite>(sprite);
            e.AddComponent<Sprite>(sprite.CurrentSprite);

            Health h = new Health(3f);
            h.OnDeath += GenericEvents.BloodyDeath(_World, e, 5, new Vector2(0, 0.2f));
            h.OnDeath += GenericEvents.CorpseDeath(_World, e);
            e.GetComponent<Body>().OnCollision += GenericEvents.BasicCollision();

            e.AddComponent<Health>(h);

            return e;
        }
    }
}
