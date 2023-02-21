using System;
using System.Threading.Tasks;
using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Shared.Abstractions.DAL;

namespace SaveMeter.Modules.Users.Core.Repositories;

internal interface IUserRepository : IRepository<User>
{
    Task<User> GetAsync(string email);
}