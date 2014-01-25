using GameLibrary.Dependencies.Entities;
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
            Sprite sex = e.AddComponent<Sprite>(new Sprite(ScreenHelper.SpriteSheet, "tree", 0.1f));

            //damn look at dat body its oak
            Body bitch = e.AddComponent<Body>(new Body(world, e));
            //Create a circle with radius average of height and width over 2
            FixtureFactory.AttachCircle(ConvertUnits.ToSimUnits(sex.Source[0].Width/2), 
                1f, bitch, new Vector2(0, ConvertUnits.ToSimUnits(21)));


            bitch.Position = (Vector2)args[0];
            
            e.Refresh();
            return e;
        }
    }
}
