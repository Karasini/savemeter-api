using System;
using SaveMeter.Shared.Abstractions.Kernel.Types;

namespace SaveMeter.Modules.Transactions.Core.Entities;

internal class Category : Entity
{
    public string Name { get; set; }
    public Guid? ParentId { get; set; }

    private Category(Guid id, string name, Guid? parentId) : base(id)
    {
        Name = name;
        ParentId = parentId;
    }

    public static Category Create(string id, string name) => new(Guid.Parse(id), name, null);
}
