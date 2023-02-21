using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Shared.Abstractions.DAL;
using SaveMeter.Shared.Abstractions.Kernel.Types;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Models;

namespace SaveMeter.Shared.Infrastructure.Mongo.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DbCollection;

        protected BaseRepository(IMongoContext context)
        {
            Context = context;

            DbCollection = Context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual TEntity Add(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            Context.AddCommand(() => DbCollection.InsertOneAsync(entity), entity);

            return entity;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var data = await DbCollection.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return (await DbCollection.FindAsync(Builders<TEntity>.Filter.Empty)).ToList();
        }


        public virtual TEntity Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            Context.AddCommand(() => DbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id), entity), entity);

            return entity;
        }

        public virtual void Remove(TEntity obj)
        {
            Context.AddCommand(() => DbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.Id)), obj);
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await DbCollection.Find(filter).AnyAsync();
        }

        public async Task<bool> AnyAsync()
        {
            return await DbCollection.Find(x => x.Id != Guid.Empty).AnyAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
