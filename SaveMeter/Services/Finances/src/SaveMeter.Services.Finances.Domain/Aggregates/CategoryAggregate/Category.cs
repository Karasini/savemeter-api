﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Models;

namespace SaveMeter.Services.Finances.Domain.Aggregates.CategoryAggregate
{
    public class Category : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }

        private Category(Guid id, string name, Guid? parentId) : base(id)
        {
            Name = name;
            ParentId = parentId;
        }

        public static Category Create(string id, string name) => new(Guid.Parse(id), name, null);
    }
}