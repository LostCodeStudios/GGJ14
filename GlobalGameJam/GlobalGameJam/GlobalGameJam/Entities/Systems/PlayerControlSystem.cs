using GameLibrary.Dependencies.Entities;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using GlobalGameJam.Entities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Systems
{
    public class PlayerControlSystem : TagSystem
    {
        ComponentMapper<Body> bodyMapper;

#if DEBUG
        const float PLAYER_SPEED = 9f;
#else
        const float PLAYER_SPEED = 3f;
#endif

        public PlayerControlSystem(Camera c)
            : base("Player")
        {
            this.camera = c;
        }

        Camera camera;

        public override void Initialize()
        {
            bodyMapper = new ComponentMapper<Body>(world);
        }

        public override void Process(Entity e)
        {
            Body b = bodyMapper.Get(e);

            KeyboardState keyState = Keyboard.GetState();

            #region Standard Movement
            Vector2 dir = Vector2.Zero;
            if (keyState.IsKeyDown(Keys.A))
            {
                dir.X = -1;
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                dir.X = 1;
            }

            if (keyState.IsKeyDown(Keys.W))
            {
                dir.Y = -1;
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                dir.Y = 1;
            }

            if (dir != Vector2.Zero) dir.Normalize();
            dir *= PLAYER_SPEED;

            b.LinearVelocity = dir;

            #endregion

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && charge > 0 && !recharge)
            {
                Vector2 mouseLoc = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                Vector2 mouseWorldLoc = mouseLoc - ScreenHelper.Center;
                Vector2 aiming = b.Position - camera.ConvertScreenToWorld(mouseLoc);
                aiming.Normalize();
                b.LinearVelocity = -aiming * PLAYER_SPEED * 1.5f;

                e.GetComponent<Health>().SetHealth(e, 10);
                charge -= world.Delta / 300f;
            }
            else
                recharge = true;

            if (recharge)
            {
                charge += world.Delta / 2000f;
                if (charge >= 1)
                    recharge = false;
            }


        }

        float charge = 1;
        bool recharge = false;
    }
}
