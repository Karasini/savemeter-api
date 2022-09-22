using System.Collections.Generic;
using System.Threading.Tasks;
using SaveMeter.Modules.Users.Core.Entities;

namespace SaveMeter.Modules.Users.Core.Repositories;

internal interface IRoleRepository
{
    Task<Role> GetAsync(string name);
    Task<IReadOnlyList<Role>> GetAllAsync();
    Task AddAsync(Role role);
}