using System;
using System.Collections.Generic;

namespace SaveMeter.Shared.Abstractions.Kernel.Types
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        protected Entity(Guid id)
        {
            Id = id;
        }
    }
}
