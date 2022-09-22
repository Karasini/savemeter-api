using System.Collections.Generic;

namespace SaveMeter.Modules.Users.Core.DTO;

public class UserDetailsDto : UserDto
{
    public IEnumerable<string> Permissions { get; set; }
}