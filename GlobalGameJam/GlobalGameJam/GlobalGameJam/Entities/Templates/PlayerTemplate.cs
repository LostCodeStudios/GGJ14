using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Factories;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using GlobalGameJam.Entities.Components;
using GlobalGameJam.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates
{
    public class Player : Component
    {

    }
    class PlayerTemplate : IEntityTemplate
    {
        BoblinWorld _World;
        MainMenuScreen main;
        GameplayScreen gameplay;
        public PlayerTemplate(BoblinWorld world, MainMenuScreen main)
        {
            this._World = world;
            this.main = main;
        }

        public Entity BuildEntity(Entity e, params object[] args)
        {
            e.Tag = "Player";

            e.AddComponent<Player>(new Player());
            this.gameplay = (GameplayScreen)args[0];

            FixtureFactory.AttachCircle(0.4f, 1f, e.AddComponent<Body>(new Body(_World, e)));
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
