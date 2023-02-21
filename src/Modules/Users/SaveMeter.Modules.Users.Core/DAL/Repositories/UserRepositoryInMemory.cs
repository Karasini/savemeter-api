using SaveMeter.Modules.Users.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Modules.Users.Core.Entities;

namespace SaveMeter.Modules.Users.Core.DAL.Repositories;
internal class UserRepositoryInMemory
{
    private readonly List<User> _users = new List<User>();

    public Task<User> GetAsync(Guid id)
    {
        return Task.FromResult(_users.FirstOrDefault(x => x.Id == id));
    }

    public Task<User> GetAsync(string email)
    {
        return Task.FromResult(_users.FirstOrDefault(x => string.Equals(x.Email, email, StringComparison.OrdinalIgnoreCase)));
    }

    public Task AddAsync(User user)
    {
        _users.Add(user);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        var index = _users.FindIndex(x => x.Id == user.Id);
        _users[index] = user;

        return Task.CompletedTask;
    }
}
