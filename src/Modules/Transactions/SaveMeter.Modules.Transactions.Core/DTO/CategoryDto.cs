using System;

namespace SaveMeter.Modules.Transactions.Core.DTO;
internal record CategoryDto
{
    public Guid Id { get; init; }
    public string CategoryName { get; init; }
}
