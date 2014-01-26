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
        EntityWorld world;
        public TreeTemplate(EntityWorld world)
        {
            this.world = world;
        }


        /// <summary>
        /// Makes the tree
        /// </summary>
        /// <param name="e"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Entity BuildEntity(Entity e, params object[] args)
        {
            //gotta lok pretty. its firday put makeup on
            Sprite sex = e.AddComponent<Sprite>(new Sprite(ScreenHelper.SpriteSheet, "tree", 0.6f));

            //damn look at dat body its oak
            Body bitch = e.AddComponent<Body>(new Body(world, e));
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
