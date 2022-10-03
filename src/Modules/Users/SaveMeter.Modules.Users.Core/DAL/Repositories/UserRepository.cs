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
internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IMongoContext context) : base(context)
    {
    }

    public async Task<User> GetAsync(string email)
    {
        return await DbCollection.Find(x => x.Email.Value == email).SingleOrDefaultAsync();
    }
}
