using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Dynamics;
using GameLibrary.Entities.Components.Physics;
using GlobalGameJam.Entities.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities
{
    public static class GenericEvents
    {
        public static Action<Entity> BloodyDeath(BoblinWorld world, Entity owner, int bloodType)
        {
            return BloodyDeath(world, owner, bloodType, Vector2.Zero);
        }

        public static Action<Entity> BloodyDeath(BoblinWorld world, Entity owner, int bloodType, Vector2 bloodOffset)
        {
            return (e) =>
            {
                Body b = owner.GetComponent<Body>();
                world.CreateEntity("Blood", b.Position + bloodOffset, bloodType).Refresh();
                owner.Delete();

                if (e.Tag == "Player") world.Evil += BoblinWorld.EVIL_INC;
            };
        }

        public static Action<Entity> CorpseDeath(BoblinWorld world, Entity owner)
        {
            return (e) =>
            {
                Body b = owner.GetComponent<Body>();
                world.CreateEntity("Corpse", b.Position).Refresh();
                owner.Delete();
            };
        }

        public static OnCollisionEventHandler BasicCollision()
        {
            return (f1, f2, c) =>
                {
                    if (f1.Body.UserData != null && f2.Body.UserData != null && f1.Body.UserData is Entity && f2.Body.UserData is Entity)
                    {
                        Entity e1 = f1.Body.UserData as Entity;
                        Entity e2 = f2.Body.UserData as Entity;

                        if (e1.HasComponent<Health>() && e2.HasComponent<Health>())
                        {
                            Health h1 = e1.GetComponent<Health>();
                            Health h2 = e2.GetComponent<Health>();

                            if (h2.MaxHealth > h1.MaxHealth)
                            {
                                if (e1.HasComponent<Killable>() || e2.Group == "Goblins")
                                    h1.Damage(e2, h2.MaxHealth);
                            }
                            else if (h1.MaxHealth > h2.MaxHealth)
                            {
                                if (e2.HasComponent<Killable>() || e1.Group == "Goblins")
                                    h2.Damage(e1, h1.MaxHealth);
                            }

                            return false;
                        }
                    }

                    return true;
                };
        }

        public static OnCollisionEventHandler ReleaseHeart(BoblinWorld world)
        {
            return (f1, f2, c) =>
                {
                    if (f1.Body.UserData != null && f2.Body.UserData != null && f1.Body.UserData is Entity && f2.Body.UserData is Entity)
                    {
                        Entity e1 = f1.Body.UserData as Entity;
                        Entity e2 = f2.Body.UserData as Entity;

                        if (e1.Tag == "Player" && e2.HasComponent<Heart>())
                        {
                            world.CreateEntity("Heart", f2.Body.Position).Refresh();
                            e2.RemoveComponent<Heart>(e2.GetComponent<Heart>());
                            world.Hearts++;

                            return false;
                        }

                        else if (e2.Tag == "Player" && e1.HasComponent<Heart>())
                        {
                            world.CreateEntity("Heart", f1.Body.Position).Refresh();
                            e1.RemoveComponent<Heart>(e1.GetComponent<Heart>());
                            world.Hearts++;

                            return false;
                        }
                    }

                    return true;
                };
        }
    }
}
