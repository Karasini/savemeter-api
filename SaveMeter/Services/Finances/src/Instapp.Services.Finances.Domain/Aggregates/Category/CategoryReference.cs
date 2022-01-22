using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Models;

namespace Instapp.Services.Finances.Domain.Aggregates.Category
{
    public class CategoryReference : Entity
    {
        public Guid CategoryId { get; set; }
        public string Key { get; set; }

        private CategoryReference(Guid categoryId, string key)
        {
            CategoryId = categoryId;
            Key = key;
        }

        public static CategoryReference Create(Guid categoryId, string key) => new(categoryId, key);
    }
}
