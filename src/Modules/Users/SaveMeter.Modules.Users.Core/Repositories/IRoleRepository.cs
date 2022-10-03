using System.Collections.Generic;
using System.Threading.Tasks;
using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Shared.Abstractions.DAL;

namespace SaveMeter.Modules.Users.Core.Repositories;

internal interface IRoleRepository : IRepository<Role>
{
    Task<Role> GetAsync(string name);
}