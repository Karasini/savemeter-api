using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Shared.Abstractions.Kernel.Types;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Models;

namespace SaveMeter.Shared.Infrastructure.Mongo.Repository
{
    public abstract class ReadBaseRepository<TEntity> where TEntity : Entity
    {
        protected readonly IMongoContext Context;
        public IMongoCollection<TEntity> Collection { get; }

        protected ReadBaseRepository(IMongoContext context)
        {
            Context = context;

            Collection = Context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return (await Collection.FindAsync(Builders<TEntity>.Filter.Eq("_id", id))).SingleOrDefault();
        }

        public virtual async Task<TProjection> GetById<TProjection>(Guid id, ProjectionDefinition<TEntity, TProjection> projection)
        {
            var options = new FindOptions<TEntity, TProjection>()
            {
                Projection = projection
            };

            return (await Collection.FindAsync(Builders<TEntity>.Filter.Eq("_id", id), options)).SingleOrDefault();
        }

        public IFindFluent<TEntity, TEntity> Find(Expression<Func<TEntity, bool>> filter) => Collection.Find(filter);

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
