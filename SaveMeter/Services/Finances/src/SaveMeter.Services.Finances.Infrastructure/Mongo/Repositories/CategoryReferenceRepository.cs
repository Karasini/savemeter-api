using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Context;
using Instapp.Common.MongoDb.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using SaveMeter.Services.Finances.Domain.Aggregates.Category;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories
{
    class CategoryReferenceRepository : BaseRepository<CategoryReference>, ICategoryReferenceRepository
    {
        public CategoryReferenceRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<CategoryReference> GetIfExistsIn(string description)
        {
            if (string.IsNullOrEmpty(description)) return null;
            var preparedString = description
                .Replace("[", string.Empty)
                .Replace("]", string.Empty)
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Replace("*", string.Empty);
            var splitted = string.Join("|", preparedString.Split(" ").Select(x => $"({x})").ToList().Where(x => !string.IsNullOrEmpty(x)));
            var regex = new BsonRegularExpression(splitted, "/i");

            return await (await DbCollection.FindAsync(
                Builders<CategoryReference>.Filter.Regex(x => x.Key, regex))).FirstOrDefaultAsync();
        }
    }
}
