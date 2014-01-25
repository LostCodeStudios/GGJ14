using GameLibrary.Dependencies.Entities;
using GameLibrary.Dependencies.Physics.Collision;
using GameLibrary.Dependencies.Physics.Dynamics;
using GameLibrary.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities
{
    public class Chunk
    {
        public static float SIZE = 24;

        static Dictionary<int, string> tileKeys;
        static Random r = new Random();

        int[,] tiles = new int[(int)SIZE, (int)SIZE];

        public Chunk(Vector2 position, List<Entity> terrain)
        {
            this.Position = position;
            this.Terrain = terrain;

            if (tileKeys == null)
            {
                tileKeys = new Dictionary<int, string>();
                tileKeys.Add(0, "grass1");
                tileKeys.Add(1, "grass2");
                tileKeys.Add(2, "water1");
                tileKeys.Add(3, "water2");
                tileKeys.Add(4, "redground1");
                tileKeys.Add(5, "redground2");
                tileKeys.Add(6, "lava1");
                tileKeys.Add(7, "lava2");
            }

            for (int y = 0; y < SIZE; ++y)
            {
                for (int x = 0; x < SIZE; ++x)
                {
                    tiles[y, x] = r.Next(2);
                }
            }
        }
        /// <summary>
        /// The position of the chunk in world coordinates
        /// </summary>
        public Vector2 Position { set; get; }

        /// <summary>
        /// The terrain entities stored within the chunk.
        /// </summary>
        public List<Entity> Terrain { set; get; }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteSheet spriteSheet = ScreenHelper.SpriteSheet;
            Texture2D texture = spriteSheet.Texture;
            Vector2 offset = new Vector2(-16, -16);

            Rectangle source;
            for (int y = 0; y < SIZE; ++y)
            {
                for (int x = 0; x < SIZE; ++x)
                {
                    source = spriteSheet.Animations[tileKeys[tiles[y, x]]][0];

                    Vector2 position = new Vector2(Position.X - SIZE / 2 + x, Position.Y - SIZE / 2 + y);

                    spriteBatch.Draw(texture, ConvertUnits.ToDisplayUnits(position) + offset, source, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }

            //Vector2 corner1 = ConvertUnits.ToDisplayUnits(Position + new Vector2(-SIZE / 2, -SIZE / 2));
            //Vector2 corner2 = ConvertUnits.ToDisplayUnits(Position + new Vector2(SIZE / 2, -SIZE / 2));
            //Vector2 corner3 = ConvertUnits.ToDisplayUnits(Position + new Vector2(-SIZE / 2, SIZE / 2));
            //Vector2 corner4 = ConvertUnits.ToDisplayUnits(Position + new Vector2(SIZE / 2, SIZE / 2));

            //source = spriteSheet.Animations["bunny"][0];
            //spriteBatch.Draw(texture, corner1, source, Color.White);
            //spriteBatch.Draw(texture, corner2, source, Color.White);
            //spriteBatch.Draw(texture, corner3, source, Color.White);
            //spriteBatch.Draw(texture, corner4, source, Color.White);

            //spriteBatch.Draw(texture, ConvertUnits.ToDisplayUnits(Position), source, Color.White);
            
        }

        /// <summary>
        /// Deletes the chunk giving off a new position to generate a suceeding chunk based on an anchor and a new origin
        /// </summary>
        /// <param name="anchor">The old center origin</param>
        /// <param name="origin">The new origin</param>
        /// <returns></returns>
        public Vector2 Delete(Vector2 anchor, Vector2 origin, PhysicsWorld world)
        {
            foreach (Entity e in Terrain)
                e.Delete();

            AABB box = new AABB(Position, SIZE, SIZE);
            world.QueryAABB(
                (f) =>
                {
                    Entity e = f.Body.UserData as Entity;
                    e.Delete();

                    return true;
                }, ref box);

            return anchor - Position + origin;
        }
    }
}
