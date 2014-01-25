using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Collision;
using GameLibrary.Entities.Components.Physics;
using GlobalGameJam.Entities.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Systems
{
    public class GoblinSystem : GroupSystem
    {
        const float SEARCH_DISTANCE = 10;
        const float GOBLIN_SPEED = 4;

        public GoblinSystem()
            : base("Goblins")
        {
        }

        public override void Process(Entity e)
        {
            if (!e.HasComponent<GoblinAI>())
            {
                Vector2 position = e.GetComponent<Body>().Position;

                Entity target = null;

                AABB box = new AABB(position, SEARCH_DISTANCE * 2, SEARCH_DISTANCE * 2);
                float smallestDistance = float.MaxValue;
                world.QueryAABB(
                    (f) =>
                    {
                        Body b = (Body)f.Body;
                        Entity e1 = b.UserData as Entity;

                        Console.WriteLine("Checking entity " + e1.Group + " for targeting");

                        if (e1.Group != "Goblins" && e1.HasComponent<Health>())
                        {
                            if (Vector2.Distance(position, b.Position) < smallestDistance)
                                target = e1;
                        }

                        return true;
                    }, ref box);

                if (target != null)
                {
                    GoblinAI ai = new GoblinAI(target);
                    e.AddComponent<GoblinAI>(ai);
                }
                else
                {
                    e.Delete(); //avoid repeated fruitless searchers
                }
            }
            else
            {
                Body goblin = e.GetComponent<Body>();
                Body target = e.GetComponent<GoblinAI>().Target.GetComponent<Body>();

                if (target == null)
                {
                    e.RemoveComponent<GoblinAI>(e.GetComponent<GoblinAI>());
                    goblin.LinearVelocity = Vector2.Zero;
                    return;
                }

                Vector2 dir = target.Position - goblin.Position;
                dir.Normalize();

                dir *= GOBLIN_SPEED;

                goblin.LinearVelocity = dir;
            }
        }
    }
}
