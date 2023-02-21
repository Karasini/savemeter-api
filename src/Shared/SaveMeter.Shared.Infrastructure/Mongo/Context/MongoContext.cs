using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Shared.Abstractions.Kernel.Types;
using SaveMeter.Shared.Infrastructure.Mongo.Models;

namespace SaveMeter.Shared.Infrastructure.Mongo.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<EntityEntry> _entries;
        private readonly MongoOptions _config;

        public MongoContext(MongoOptions config)
        {
            _config = config;

            // Every command will be stored and it'll be processed at SaveChanges
            _entries = new List<EntityEntry>();
        }

        public async Task<List<Entity>> SaveChanges()
        {
            ConfigureMongo();

            using var session = Session = await MongoClient.StartSessionAsync();

            if (_config.EnableTransactions)
            {
                Session.StartTransaction();
                await ProcessCommands();
                await Session.CommitTransactionAsync();
            }
            else
            {
                await ProcessCommands();
            }

            var changedEntities = _entries.Select(x => x.Entity).ToList();
            _entries.Clear();

            return changedEntities;
        }

        private async Task ProcessCommands()
        {
            var commandTasks = _entries.Select(e => e.Command());
            await Task.WhenAll(commandTasks);
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            MongoClient = new MongoClient(_config.Connection);

            Database = MongoClient.GetDatabase(_config.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func, Entity entity)
        {
            _entries.Add(new EntityEntry(func, entity));
        }
    }
}