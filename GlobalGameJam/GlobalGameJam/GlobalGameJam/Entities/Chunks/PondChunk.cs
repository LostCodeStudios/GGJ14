using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Factories;
using GameLibrary.Entities.Components.Physics;
using GlobalGameJam.Entities.Components;
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
            base.Generate();
            int radius = r.Next(2, (int)(SIZE / 6));
            float xo = r.Next(2 + radius, (int)SIZE - radius - 2);
            float yo = r.Next(2 + radius, (int)SIZE - radius - 2);

            bool lava = bworld.Evil > BoblinWorld.LAVA_EVIL;
            
            for(int i = 0; i < Terrain.Count; i++)
            {
                Entity t = Terrain[i];
                if (t.HasComponent<Body>())
                {
                    Body bitch = t.GetComponent<Body>();
                    if (Vector2.Distance(bitch.Position, Position + new Vector2(xo, yo) - new Vector2(SIZE / 2)) < radius + (float)Math.Sqrt(radius))
                    {
                        t.Delete();
                        Terrain.RemoveAt(i);
                        i--;
                    }
                }
            }
 	         for(int x = 0; x < SIZE; x++)
                 for (int y = 0; y <SIZE; y++)
                 {
                     //Satisfy x^2+y^2=(size/2)^2
                     if (Math.Abs(Math.Pow(x - xo, 2) + Math.Pow(y - yo, 2) - Math.Pow(radius, 2)) < radius*radius || (x == xo && y == yo))
                     {
                         if (lava)
                             tiles[x, y] = 5 + r.Next(1);
                         else
                             tiles[x, y] = 2 + r.Next(1);
                     }
                 }

             Entity e = bworld.CreateEntity();
            Body b = e.AddComponent<Body>(new Body(bworld, e));
            FixtureFactory.AttachCircle(radius+ (float)Math.Sqrt(radius)/(float)Math.Sqrt(2f), 1f, b);
            b.Position = this.Position +  new Vector2(xo, yo) - new Vector2(SIZE/2);

            if (lava)
                b.OnCollision += (f1, f2, c) =>
                {
                    Entity e1 = (f1.Body.UserData as Entity);
                    Entity e2 = (f2.Body.UserData as Entity);
                    if (e1.Tag == "Player")
                        e1.GetComponent<Health>().SetHealth(e2, 0);
                    else if (e2.Tag == "Player")
                        e2.GetComponent<Health>().SetHealth(e1, 0);
                    return true;
                };


             e.Refresh();
             Terrain.Add(e);
        }
    }
}
