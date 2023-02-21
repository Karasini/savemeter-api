using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Shared.Abstractions.Kernel.Types;
using SaveMeter.Shared.Infrastructure.Mongo.Models;

namespace SaveMeter.Shared.Infrastructure.Mongo.Context
{
    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func, Entity entity);
        Task<List<Entity>> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}