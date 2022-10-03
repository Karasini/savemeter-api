using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Modules.Users.Core.Repositories;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Repository;

namespace SaveMeter.Modules.Users.Core.DAL.Repositories;
internal class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(IMongoContext context) : base(context)
    {
    }

    public async Task<Role> GetAsync(string name)
    {
        return await DbCollection.Find(x => x.Name == name).SingleOrDefaultAsync();
    }
}
