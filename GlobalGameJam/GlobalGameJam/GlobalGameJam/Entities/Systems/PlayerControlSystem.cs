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
        public const float CLICK_TOO_CLOSE = 1f;

        public const float SOUND_DELAY = 0.58f;

        float duration = 0f;

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
            duration -= (float)world.Delta / 1000f;

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

            #region Gamepad Movement

            GamePadState padState = GamePad.GetState(PlayerIndex.One);
            if (padState.IsConnected)
            {
                dir = new Vector2(padState.ThumbSticks.Left.X, -padState.ThumbSticks.Left.Y);

                dir *= PLAYER_SPEED;

                b.LinearVelocity = dir;
            }
            

            #endregion

            Sprite s = e.GetComponent<Sprite>();
            if ((!padState.IsConnected && Mouse.GetState().LeftButton == ButtonState.Pressed || AnyButtonPressed(padState) && padState.ThumbSticks.Left != Vector2.Zero)&& charge > 0 && !recharge)
            {
                Vector2 aiming = Vector2.Zero;
                if (padState.IsConnected)
                {
                    aiming = -b.LinearVelocity;
                }
                else
                {
                    Vector2 mouseLoc = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                    Vector2 mouseWorldLoc = camera.ConvertScreenToWorld(mouseLoc);

                    if (Vector2.Distance(b.Position, mouseWorldLoc) < CLICK_TOO_CLOSE)
                    {
                        recharge = true;
                        return;
                    }

                    aiming = b.Position - mouseWorldLoc;
                }
                aiming.Normalize();
                b.LinearVelocity = -aiming * PLAYER_SPEED * 1.5f; //dude please no "shit nigga" find the off switch please 
                //for today and for the future where it afctually is an important skill 

                if (e.GetComponent<Health>().MaxHealth == 3)
                {
                    e.GetComponent<Health>().MaxHealth = 10;
                    SoundManager.Play("Dash");
                }
                //s.Color= Color.Lerp(s.Color, new Color(255, 126, 126), 0.1f);
                //e.RemoveComponent<Sprite>(e.GetComponent<Sprite>());
                //e.AddComponent<Sprite>(s);

                charge -= world.Delta / 300f;
            }
            else
            {
                recharge = true;
                //s.Color.B = 255;
                //s.Color.G = 255;

                //e.RemoveComponent<Sprite>(e.GetComponent<Sprite>());
                //e.AddComponent<Sprite>(s);
                e.GetComponent<Health>().MaxHealth = 3;

                if (dir != Vector2.Zero)
                {


                    if (duration <= 0f)
                    {
                        SoundManager.Play("Footstep");
                        duration = SOUND_DELAY;
                    }
                }
            }

            if (recharge)
            {
                charge += world.Delta / 2000f;
                if (charge >= 1)
                {
                    charge = 1;
                    recharge = false;
                }
            }

            if (charge != 1)
            {
                (world as BoblinWorld).EnergyBar = charge;
            }
            else
            {
                (world as BoblinWorld).EnergyBar = -1;
            }
        }

        public float charge = 1;
        bool recharge = false;

        bool AnyButtonPressed(GamePadState padState)
        {
            if (padState.Buttons.A == ButtonState.Pressed)
            {
                return true;
            }

            if (padState.Buttons.B == ButtonState.Pressed)
            {
                return true;
            }

            if (padState.Buttons.X == ButtonState.Pressed)
            {
                return true;
            }

            if (padState.Buttons.Y == ButtonState.Pressed)
            {
                return true;
            }

            if (padState.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                return true;
            }

            if (padState.Buttons.RightShoulder == ButtonState.Pressed)
            {
                return true;
            }

            if (padState.Triggers.Left != 0)
            {
                return true;
            }

            if (padState.Triggers.Right != 0)
            {
                return true;
            }

            return false;
        }
    }
}
