using System;
using System.Threading.Tasks;
using SaveMeter.Shared.Abstractions.Kernel.Types;
using SaveMeter.Shared.Infrastructure.Mongo.Models;

namespace SaveMeter.Shared.Infrastructure.Mongo.Context
{
    class EntityEntry
    {
        public Func<Task> Command { get; set; }
        public Entity Entity { get; set; }

        public EntityEntry(Func<Task> command, Entity entity)
        {
            Command = command;
            Entity = entity;
        }
    }
}