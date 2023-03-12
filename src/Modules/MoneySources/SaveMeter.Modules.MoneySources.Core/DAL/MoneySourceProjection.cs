using MongoDB.Driver;
using SaveMeter.Modules.MoneySources.Core.DTO;
using SaveMeter.Modules.MoneySources.Core.Entities;

namespace SaveMeter.Modules.MoneySources.Core.DAL;

static class MoneySourceProjection
{
    private static ProjectionDefinition<MoneySource, MoneySourceDto> Projection =>
        Builders<MoneySource>.Projection.Expression(category => new MoneySourceDto
        {
            Id = category.Id,
            NativeAmount = category.NativeAmount,
            StatusDate = category.StatusDate,
            Amount = category.Amount,
            Currency = category.Currency,
            NativeCurrency = category.NativeCurrency,
            Title = category.Title,
            Type = category.Type.ToString()
        });

    internal static IFindFluent<MoneySource, MoneySourceDto> ProjectToMoneySourceDto(this IFindFluent<MoneySource, MoneySource> fluent)
    {
        return fluent.Project(Projection);
    }
}