using GameLibrary.Dependencies.Entities;
using GlobalGameJam.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalGameJam.Entities.Templates
{
    public class GuidesTemplate : IEntityTemplate
    {
        public Entity BuildEntity(Entity e, params object[] args)
        {
            e.AddComponent<GuideTimer>(new GuideTimer());

            return e;
        }
    }
}
