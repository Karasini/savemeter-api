using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Modules.Users.Core.Repositories;

namespace SaveMeter.Modules.Users.Core.DAL.Repositories;
internal class RoleRepository : IRoleRepository
{
    private readonly List<Role> _roles = new List<Role>();

    public Task<Role> GetAsync(string name)
    {
        return Task.FromResult(_roles.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase)));
    }

    public async Task<IReadOnlyList<Role>> GetAllAsync()
    {
        return _roles;
    }

    public Task AddAsync(Role role)
    {
        _roles.Add(role);
        return Task.CompletedTask;
    }
}
