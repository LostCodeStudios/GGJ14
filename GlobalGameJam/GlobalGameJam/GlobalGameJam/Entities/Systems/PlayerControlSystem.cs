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

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && elapsedCharge < chargeMax && elapsedCooldown == -1)
            {
                Vector2 mouseLoc = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                Vector2 mouseWorldLoc = mouseLoc - ScreenHelper.Center;
                Vector2 aiming = b.Position - camera.ConvertScreenToWorld(mouseLoc);
                aiming.Normalize();
                b.LinearVelocity = -aiming * PLAYER_SPEED * 2;

                e.GetComponent<Health>().SetHealth(e, 10);
                elapsedCharge += world.Delta / 1000f;
            }
            else if(elapsedCharge >= chargeMax)
            {
                elapsedCharge = -1;
                elapsedCooldown = 0;
            }
            else if(elapsedCooldown > -1){
                 elapsedCooldown += world.Delta / 1000f;
                 if (elapsedCooldown >= chargeCooldown)
                 {
                     elapsedCooldown = -1;
                     elapsedCharge = 0;
                 }
            }

        }


        float elapsedCharge = 0;
        float chargeCooldown = 1;
        float chargeMax = 0.2f;
        float elapsedCooldown = 0;
    }
}
