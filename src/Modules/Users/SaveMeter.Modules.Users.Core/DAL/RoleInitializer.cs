using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using SaveMeter.Modules.Users.Core.DAL.Repositories;
using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Modules.Users.Core.Repositories;
using SaveMeter.Shared.Infrastructure;
using SaveMeter.Shared.Infrastructure.Mongo.UoW;

namespace SaveMeter.Modules.Users.Core.DAL;
internal class RoleInitializer : IInitializer
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly HashSet<string> _permissions = new()
    {
        "categories.crud", "categories.r",
        "transactions.crud", "transactions.r"
    };

    public RoleInitializer(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task InitAsync()
    {
        if (!await _roleRepository.AnyAsync())
        {
            _roleRepository.Add(new Role
            {
                Name = "user",
                Permissions = _permissions,
            });
        }
        else
        {
            var role = await _roleRepository.GetAsync(Role.Default);
            role.Permissions = _permissions;

            _roleRepository.Update(role);
        }

        await _unitOfWork.Commit();
    }
}
