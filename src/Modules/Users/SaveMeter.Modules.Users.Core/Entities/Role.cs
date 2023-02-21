using System.Collections.Generic;
using SaveMeter.Shared.Abstractions.Kernel.Types;

namespace SaveMeter.Modules.Users.Core.Entities;

internal class Role : Entity
{
    public string Name { get; set; }
    public IEnumerable<string> Permissions { get; set; } = new List<string>();

    public static string Default => User;
    public const string User = "user";
    public const string Admin = "admin";
}