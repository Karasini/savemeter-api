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
    private readonly RoleReadRepository _roleRepository;

    public UserRepository(IMongoContext context, RoleReadRepository roleRepository) : base(context)
    {
        _roleRepository = roleRepository;
    }

    public async Task<User> GetAsync(string email)
    {
        return await DbCollection.Aggregate().Match(x => x.Email == email)
            .Lookup(_roleRepository.Collection, (x => x.RoleIds), (x => x.Id),
                @as: (User eo) => eo.Roles).SingleOrDefaultAsync();
    }
}
