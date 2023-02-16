using System;
using System.Linq;
using SaveMeter.Shared.Abstractions.Kernel.Types;

namespace SaveMeter.Modules.Transactions.Core.Entities;

internal class Category : Entity
{
    public static Guid IncomeId = Guid.Parse("5820CC56-2EE2-44F2-A23C-3E8E6AA6E187");
    public static Guid InternalTransferId = Guid.Parse("4F664D11-310E-46A9-AFAB-19C5DEDF529E");
    public static Guid SkipId = Guid.Parse("D676EA72-09D3-4892-B0EF-1DFDC7E19177");

    private static Guid[] CategoriesNotForOutcome = { IncomeId, SkipId, InternalTransferId };
    
    public string Name { get; set; }
    public Guid? ParentId { get; set; }

    private Category(Guid id, string name, Guid? parentId) : base(id)
    {
        Name = name;
        ParentId = parentId;
    }

    public static Category Create(string id, string name) => new(Guid.Parse(id), name, null);

    public static bool IsCategoryIdOutcome(Guid categoryId) => CategoriesNotForOutcome.All(y => y != categoryId);
}
