using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Factories;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using GlobalGameJam.Entities.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates
{
    public class GoblinTemplate : IEntityTemplate
    {
        BoblinWorld world;

        public GoblinTemplate(BoblinWorld world)
        {
            this.world = world;
        }

        public Entity BuildEntity(Entity e, params object[] args)
        {
            Vector2 position = (Vector2)args[0];

            Sprite sprite = new Sprite(ScreenHelper.SpriteSheet, "goblin", 0.5f);
            e.AddComponent<Sprite>(sprite);

            Body body = new Body(world, e);
            FixtureFactory.AttachCircle(0.2f, 1f, body);
            body.Position = position;
            body.OnCollision += GenericEvents.BasicCollision();
            e.AddComponent<Body>(body);

            Health health = new Health(5f);
            health.OnDeath += GenericEvents.BloodyDeath(world, e, 5);
            e.AddComponent<Health>(health);

            return e;
        }
    }
}
