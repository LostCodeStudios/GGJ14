using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Dynamics;
using GameLibrary.Entities.Components.Physics;
using GlobalGameJam.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities
{
    public static class GenericEvents
    {
        public static Action<Entity> BloodyDeath(BoblinWorld world)
        {
            return (e) =>
            {
                Body b = e.GetComponent<Body>();
                world.CreateEntity("Blood", b.Position).Refresh();
                e.Delete();
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

                            h1.Damage(e2, h2.MaxHealth);
                            h2.Damage(e1, h1.MaxHealth);
                        }
                    }

                    return false;
                };
        }
    }
}
