using GameLibrary.Dependencies.Entities;
using GameLibrary.Helpers;
using GlobalGameJam.Entities.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Systems
{
    public class ChunkRenderSystem : EntityProcessingSystem
    {
        SpriteBatch spriteBatch;
        Camera camera;

        public ChunkRenderSystem(SpriteBatch spriteBatch, Camera camera)
            : base(typeof(BloodTimer))
        {
            this.spriteBatch = spriteBatch;
            this.camera = camera;
        }

        protected override void ProcessEntities(Dictionary<int, Entity> entities)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, camera.View);
            
            foreach (Chunk chunk in (world as BoblinWorld).chunkUpdateSytem.chunks)
            {
                chunk.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        public override void Process(Entity e)
        {
            
        }
    }
}
