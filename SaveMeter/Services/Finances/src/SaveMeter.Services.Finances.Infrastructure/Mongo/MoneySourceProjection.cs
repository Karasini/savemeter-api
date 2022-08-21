using MongoDB.Driver;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Domain.Aggregates.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo
{
    static class MoneySourceProjection
    {
        public static ProjectionDefinition<MoneySource, MoneySourceDto> Projection =>
            Builders<MoneySource>.Projection.Expression(category => new MoneySourceDto
            {
                Id = category.Id,
                NativeAmount = category.NativeAmount,
                StatusDate = category.StatusDate,
                Amount = category.Amount == 0 ? category.NativeAmount : category.Amount,
                Currency = string.IsNullOrEmpty(category.Currency) ? category.NativeCurrency : category.Currency,
                NativeCurrency = category.NativeCurrency,
                Title = category.Title,
                Type = category.Type.ToString()
            });

        public static IFindFluent<MoneySource, MoneySourceDto> ProjectToCategoryDto(this IFindFluent<MoneySource, MoneySource> fluent)
        {
            return fluent.Project(Projection);
        }
    }
}
