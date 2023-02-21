using System;
using SaveMeter.Modules.Users.Core.DTO;
using SaveMeter.Shared.Abstractions.Queries;

namespace SaveMeter.Modules.Users.Core.Queries;

internal record GetUser : IQuery<UserDetailsDto>
{
    public Guid UserId { get; set; }
}