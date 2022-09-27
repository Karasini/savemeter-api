using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Models;

namespace SaveMeter.Shared.Infrastructure.Mongo.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;

        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            var changedEntities = await _context.SaveChanges();

            return changedEntities.Count > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
