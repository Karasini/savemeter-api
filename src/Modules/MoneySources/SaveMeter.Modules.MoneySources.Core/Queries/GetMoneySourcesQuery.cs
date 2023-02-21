using System.Collections.Generic;
using SaveMeter.Modules.MoneySources.Core.DTO;
using SaveMeter.Shared.Abstractions.Queries;

namespace SaveMeter.Modules.MoneySources.Core.Queries;

internal class GetMoneySources : IQuery<List<MoneySourceDto>>
{
}