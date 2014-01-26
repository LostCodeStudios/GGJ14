﻿using GameLibrary.Dependencies.Entities;
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

        const float PLAYER_SPEED = 9f;

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

                e.GetComponent<Health>().MaxHealth = 10;
                Sprite s = e.GetComponent<Sprite>();
                s.Color= Color.Lerp(s.Color, new Color(255, 128, 128), 0.1f);
                e.RemoveComponent<Sprite>(e.GetComponent<Sprite>());
                e.AddComponent<Sprite>(s);

                charge -= world.Delta / 300f;
            }
            else
            {
                recharge = true;
                e.GetComponent<Health>().MaxHealth = 3;
                Sprite s = e.GetComponent<Sprite>();
                s.Color.B = 255;
                s.Color.G = 255;
                e.RemoveComponent<Sprite>(e.GetComponent<Sprite>());
                e.AddComponent<Sprite>(s);
            }

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
