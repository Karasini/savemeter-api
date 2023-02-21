using System;
using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Modules.Transactions.Core.Exceptions
{
    internal class CategoryNotFoundException : NotFoundException
    {
        public override string Code => "category_not_found";

        public CategoryNotFoundException(Guid categoryId) : base($"Category with id {categoryId} not found")
        {
        }
    }
}
