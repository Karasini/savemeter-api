using System;
using System.Threading.Tasks;
using SaveMeter.Modules.Users.Core.Entities;

namespace SaveMeter.Modules.Users.Core.Repositories;

internal interface IUserRepository
{
    Task<User> GetAsync(Guid id);
    Task<User> GetAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}