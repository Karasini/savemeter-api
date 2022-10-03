using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SaveMeter.Shared.Abstractions.Kernel.Types;
using SaveMeter.Shared.Abstractions.Kernel.ValueObjects;

namespace SaveMeter.Modules.Users.Core.Entities;

internal class User : Entity
{
    public Email Email { get; set; }
    public string Password { get; set; }
    public List<Role> Roles { get; set; }
    public List<Guid> RoleIds => Roles.Select(x => x.Id).ToList();
    public UserState State { get; set; }
}