using GameLibrary.Dependencies.Entities;
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

        public Chunk(Vector2 position, List<Entity> terrain)
        {
            this.Position = position;
            this.Terrain = terrain;
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

            
            Vector2 corner1 = ConvertUnits.ToDisplayUnits(Position + new Vector2(-SIZE / 2, -SIZE / 2));
            Vector2 corner2 = ConvertUnits.ToDisplayUnits(Position + new Vector2(SIZE / 2, -SIZE / 2));
            Vector2 corner3 = ConvertUnits.ToDisplayUnits(Position + new Vector2(-SIZE / 2, SIZE / 2));
            Vector2 corner4 = ConvertUnits.ToDisplayUnits(Position + new Vector2(SIZE / 2, SIZE / 2));

            Rectangle source = spriteSheet.Animations["bunny"][0];
            spriteBatch.Draw(texture, corner1, source, Color.White);
            spriteBatch.Draw(texture, corner2, source, Color.White);
            spriteBatch.Draw(texture, corner3, source, Color.White);
            spriteBatch.Draw(texture, corner4, source, Color.White);

            //spriteBatch.Draw(texture, ConvertUnits.ToDisplayUnits(Position), source, Color.White);
        }

        /// <summary>
        /// Deletes the chunk giving off a new position to generate a suceeding chunk based on an anchor and a new origin
        /// </summary>
        /// <param name="anchor">The old center origin</param>
        /// <param name="origin">The new origin</param>
        /// <returns></returns>
        public Vector2 Delete(Vector2 anchor, Vector2 origin)
        {
            foreach (Entity e in Terrain)
                e.Delete();

            return anchor - Position + origin;
        }
    }
}
