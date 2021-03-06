﻿using System;
using System.Collections.Generic;

namespace GameLibrary.Dependencies.Entities
{
    public abstract class HybridQueueSystemProcessing : EntityProcessingSystem
    {
        public HybridQueueSystemProcessing(Type requiredType, params Type[] otherTypes)
            : base(requiredType, otherTypes)
        {
            foreach (var item in GetMergedTypes(requiredType, otherTypes))
            {
                compTypes.Add(ComponentTypeManager.GetTypeFor(item));
            }
        }

        private List<ComponentType> compTypes = new List<ComponentType>();
        public int EntitiesToProcessEachFrame = 50;
        private Queue<Entity> queue = new Queue<Entity>();

        public void AddToQueue(Entity ent)
        {
            foreach (var item in compTypes)
            {
                if (ent.GetComponent(item) == null)
                {
                    throw new Exception("You need to have the " + item + " Component to be able to use this queue");
                }
            }
            queue.Enqueue(ent);
        }

        public int QueueCount
        {
            get
            {
                return queue.Count;
            }
        }

        protected override void ProcessEntities(Dictionary<int, Entity> entities)
        {
            if (!enabled)
                return;

            int size = queue.Count > EntitiesToProcessEachFrame ? EntitiesToProcessEachFrame : queue.Count;
            for (int i = 0; i < size; i++)
            {
                Process(queue.Dequeue());
            }
            base.ProcessEntities(entities);
        }
    }
}