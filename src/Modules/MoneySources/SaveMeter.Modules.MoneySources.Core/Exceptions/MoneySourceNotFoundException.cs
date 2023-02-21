using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Modules.MoneySources.Core.Exceptions;

internal class MoneySourceNotFoundException : NotFoundException
{
    public override string Code => "category_not_found";
}