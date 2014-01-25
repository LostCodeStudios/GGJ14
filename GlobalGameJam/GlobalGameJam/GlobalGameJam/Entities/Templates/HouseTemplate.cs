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
    public class HouseTemplate : IEntityTemplate
    {
        BoblinWorld world;

        public HouseTemplate(BoblinWorld world)
        {
            this.world = world;
        }

        public Entity BuildEntity(Entity e, params object[] args)
        {
            HouseSprite hs = new HouseSprite();

            e.AddComponent<HouseSprite>(hs);
            e.AddComponent<Sprite>(hs.Closed);

            Body body = new Body(world, e);
            FixtureFactory.AttachRectangle(ConvertUnits.ToSimUnits(hs.Closed.Source[0].Width - 16), 1f, 1f, new Vector2(0, 1.2f), body, e);
            e.AddComponent<Body>(body);

            return e;
        }
    }
}
