using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Dynamics;
using GameLibrary.Dependencies.Physics.Factories;
using GameLibrary.Entities.Components;
using GameLibrary.Entities.Components.Physics;
using GameLibrary.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates.Terrain
{
    class TreeTemplate : IEntityTemplate
    {
        BoblinWorld bworld;
        public TreeTemplate(BoblinWorld world)
        {
            this.bworld = world;
        }

        static Random r = new Random();

        /// <summary>
        /// Makes the tree
        /// </summary>
        /// <param name="e"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Entity BuildEntity(Entity e, params object[] args)
        {
            int num = r.Next(2) + 1;

            

            //gotta lok pretty. its firday put makeup on
            Sprite sex = new Sprite(ScreenHelper.SpriteSheet, "tree" + num, 0.6f);



            e.AddComponent<Sprite>(sex);

            //damn look at dat body its oak
            Body bitch = e.AddComponent<Body>(new Body(bworld, e));
            //Create a circle with radius average of height and width over 2
            FixtureFactory.AttachCircle(0.2f, 
                1f, bitch, new Vector2(0, ConvertUnits.ToSimUnits(11)));

            
            bitch.Position = (Vector2)args[0];
            bitch.FixedRotation = true;
            

            e.Refresh();
            return e;
        }
    }
}
