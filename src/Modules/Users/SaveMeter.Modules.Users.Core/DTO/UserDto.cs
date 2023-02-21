using System;
using System.Collections.Generic;

namespace SaveMeter.Modules.Users.Core.DTO;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public string State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}