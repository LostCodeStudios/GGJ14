using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Collision;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
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
        const float SEARCH_DELAY = 3f;

        const float NOISE_DISTANCE = 3f;

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

                        //Console.WriteLine("Checking entity " + e1.Group + " for targeting");

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
                    GoblinAI ai = new GoblinAI(null);
                    ai.Delay = SEARCH_DELAY;
                }
            }
            else
            {
                GoblinAI ai = e.GetComponent<GoblinAI>();
                Body goblin = e.GetComponent<Body>();
                Body target = ai.Target.GetComponent<Body>();

                if (target == null)
                {
                    if (ai.Delay > 0f)
                    {
                        ai.Delay -= world.Delta / 1000f;
                        return;
                    }

                    e.RemoveComponent<GoblinAI>(e.GetComponent<GoblinAI>());
                    goblin.LinearVelocity = Vector2.Zero;
                    return;
                }

                Vector2 dir = target.Position - goblin.Position;
                dir.Normalize();

                dir *= GOBLIN_SPEED;

                goblin.LinearVelocity = dir;

                if (Vector2.Distance(target.Position, goblin.Position) < NOISE_DISTANCE)
                {
                    if (ai.CanMakeNoise)
                    {
                        float distance = Vector2.Distance(goblin.Position, (world as BoblinWorld).player.GetComponent<Body>().Position);
                        if (distance < GenericEvents.HearingRange)
                        {
                            float volume = 1 - (distance / GenericEvents.HearingRange);

                            SoundManager.Play("Kobold", volume);
                            ai.CanMakeNoise = false;
                        }
                    }
                }
            }
        }
    }
}
