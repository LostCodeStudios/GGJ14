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

        public const float PLAYER_SPEED = 9f;
        public const float FADE_RATE = 255f;

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

            Sprite s = e.GetComponent<Sprite>();
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && charge > 0 && !recharge)
            {
                Vector2 mouseLoc = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                Vector2 mouseWorldLoc = mouseLoc - ScreenHelper.Center;
                Vector2 aiming = b.Position - camera.ConvertScreenToWorld(mouseLoc);
                aiming.Normalize();
                b.LinearVelocity = -aiming * PLAYER_SPEED * 1.5f; //dude please no "shit nigga" find the off switch please 
                //for today and for the future where it afctually is an important skill 

                e.GetComponent<Health>().MaxHealth = 10;
                s.Color= Color.Lerp(Color.White, new Color(255, 0, 0), 0.5f);
                e.RemoveComponent<Sprite>(e.GetComponent<Sprite>());
                e.AddComponent<Sprite>(s);

                charge -= world.Delta / 300f;
            }
            else
            {
                recharge = true;
                e.GetComponent<Health>().MaxHealth = 3;
            }

            if (recharge)
            {
                charge += world.Delta / 2000f;
                if (charge >= 1)
                    recharge = false;

                if (s.Color != Color.White)
                {

                    int curr = s.Color.B;
                    int final = 255;

                    byte val = (byte)(curr + (final - curr) * FADE_RATE * (world.Delta / 1000f));
                    s.Color.B = val;
                    s.Color.G = val;

                    e.RemoveComponent<Sprite>(e.GetComponent<Sprite>());
                    e.AddComponent<Sprite>(s);
                }
            }
        }

        float charge = 1;
        bool recharge = false;
    }
}
