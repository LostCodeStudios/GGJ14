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
    public class BunnyTemplate : IEntityTemplate
    {
        BoblinWorld world;

        public BunnyTemplate(BoblinWorld world)
        {
            this.world = world;
        }

        public Entity BuildEntity(Entity e, params object[] args)
        {
            Vector2 position = (Vector2)args[0];

            Sprite sprite = new Sprite(ScreenHelper.SpriteSheet, "bunny", 0.5f);

            e.AddComponent<Sprite>(sprite);

            Body body = new Body(world, e);
            FixtureFactory.AttachCircle(0.2f, 1, body);
            body.Position = position;
            body.BodyType = GameLibrary.Dependencies.Physics.Dynamics.BodyType.Dynamic;
            body.FixedRotation = true;
            e.AddComponent<Body>(body);

            BunnyAI ai = new BunnyAI();
            e.AddComponent<BunnyAI>(ai);

            return e;
        }
    }
}
