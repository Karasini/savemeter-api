using System;
using SaveMeter.Shared.Abstractions.Kernel.Types;
using SaveMeter.Shared.Abstractions.Kernel.ValueObjects;

namespace SaveMeter.Modules.Users.Core.Entities;

internal class User : Entity
{
    public Email Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public string RoleId { get; set; }
    public UserState State { get; set; }
}