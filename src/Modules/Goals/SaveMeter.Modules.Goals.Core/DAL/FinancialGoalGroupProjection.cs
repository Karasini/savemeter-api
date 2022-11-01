using System.Linq;
using MongoDB.Driver;
using SaveMeter.Modules.Goals.Core.DTO;
using SaveMeter.Modules.Goals.Core.Entities;

namespace SaveMeter.Modules.Goals.Core.DAL;

internal static class FinancialGoalGroupProjection
{
    private static ProjectionDefinition<FinancialGoalGroup, FinancialGoalGroupDto> Projection =>
        Builders<FinancialGoalGroup>.Projection.Expression(entity => new FinancialGoalGroupDto
        {
            Id = entity.Id,
            TotalAmount = entity.TotalAmount,
            Title = entity.Title,
            Goals = entity.Goals.Select(x => new FinancialGoalDto
            {
                Title = x.Title,
                Amount = x.Amount,
                Id = x.Id,
                Rank = x.Rank,
            }).ToList()
        });

    public static IFindFluent<FinancialGoalGroup, FinancialGoalGroupDto> ProjectToFinancialGoalGroupDto(this IFindFluent<FinancialGoalGroup, FinancialGoalGroup> fluent)
    {
        return fluent.Project(Projection);
    }
}