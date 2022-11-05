using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Repository;

namespace SaveMeter.Modules.Users.Core.DAL.Repositories;

internal class UserReadRepository : ReadBaseRepository<User>
{
    public UserReadRepository(IMongoContext context) : base(context)
    {
    }
}