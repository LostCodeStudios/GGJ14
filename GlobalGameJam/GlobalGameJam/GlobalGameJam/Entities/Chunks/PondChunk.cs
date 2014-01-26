using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Factories;
using GameLibrary.Entities.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities
{
    class PondChunk : Chunk
    {
        public PondChunk(Vector2 position, List<Entity> terrain, BoblinWorld bworld) : base(position, terrain, bworld) { }

        public override void Generate()
        {
            int radius = r.Next(2, (int)(SIZE / 6));
            bworld.ClearArea(Position, radius * 2, radius * 2);
            float xo = r.Next(2+radius, (int)SIZE-radius-2);
            float yo = r.Next(2+radius, (int)SIZE-radius-2);
 	         for(int x = 0; x < SIZE; x++)
                 for (int y = 0; y <SIZE; y++)
                 {
                     //Satisfy x^2+y^2=(size/2)^2
                     if (Math.Abs(Math.Pow(x - xo, 2) + Math.Pow(y - yo, 2) - Math.Pow(radius, 2)) < radius*radius)
                         tiles[x, y] = 2 + r.Next(1);
                     else
                         tiles[x, y] = 0 + r.Next(1);
                 }

             Entity e = bworld.CreateEntity();
            Body b = e.AddComponent<Body>(new Body(bworld, e));
            FixtureFactory.AttachCircle(radius+ (float)Math.Sqrt(radius)/2f, 1f, b);
            b.Position = this.Position +  new Vector2(xo, yo) - new Vector2(SIZE/2);


            foreach (Entity t in Terrain)
            {
                Body bt = t.GetComponent<Body>();

                if (Vector2.Distance(bt.Position, this.Position) < radius + (float)Math.Sqrt(radius) / 2f)
                {
                    t.Delete();
                }
            }

             e.Refresh();
             Terrain.Add(e);
        }
    }
}
