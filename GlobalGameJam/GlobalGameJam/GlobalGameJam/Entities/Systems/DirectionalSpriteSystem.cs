using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GlobalGameJam.Entities.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Systems
{
    class DirectionalSpriteSystem : EntityProcessingSystem
    {
        public DirectionalSpriteSystem()
            : base(typeof(DirectionalSprite))
        {

        }

        public override void Process(Entity e)
        {
            DirectionalSprite ds = e.GetComponent<DirectionalSprite>();
            Sprite original = ds.CurrentSprite;
            Body b = e.GetComponent<Body>();

            Vector2 dir = b.LinearVelocity;

            if (dir == Vector2.Zero)
            {
                if (ds.CurrentSprite.Equals(ds.MoveRightSprite))
                {
                    ds.CurrentSprite = ds.RightSprite;
                }
                else if (ds.CurrentSprite.Equals(ds.MoveUpSprite))
                {
                    ds.CurrentSprite = ds.UpSprite;
                }
                else if (ds.CurrentSprite.Equals(ds.MoveLeftSprite))
                {
                    ds.CurrentSprite = ds.LeftSprite;
                }
                else if (ds.CurrentSprite.Equals(ds.MoveDownSprite))
                {
                    ds.CurrentSprite = ds.DownSprite;
                }

                //Ensure no animation while not moving
                if (e.HasComponent<Animation>())
                {
                    e.RemoveComponent<Animation>(e.GetComponent<Animation>());
                }
            }
            else
            {
                //pick a sprite based on the direction
                float angle = MathHelper.ToDegrees((float)Math.Atan2(-dir.Y, dir.X));

                while (angle < 0)
                {
                    angle += 360;
                }

                if (angle <= 45 || angle >= 315)
                {
                    ds.CurrentSprite = ds.MoveRightSprite;
                }
                else if (angle > 45 && angle < 135)
                {
                    ds.CurrentSprite = ds.MoveUpSprite;
                }
                else if (angle >= 135 && angle <= 225)
                {
                    ds.CurrentSprite = ds.MoveLeftSprite;
                }
                else
                {
                    ds.CurrentSprite = ds.MoveDownSprite;
                }

                //Ensure animation while moving
                if (!e.HasComponent<Animation>())
                {
                    Animation anim = new Animation(AnimationType.Loop, 500);
                    e.AddComponent<Animation>(anim);
                    e.Refresh();
                    Console.WriteLine("A new animation was created.");
                }
                
            }

            if (!original.Equals(ds.CurrentSprite))
            {
                e.RemoveComponent<Sprite>(original);
                e.AddComponent<Sprite>(ds.CurrentSprite);
            }
        }
    }
}
