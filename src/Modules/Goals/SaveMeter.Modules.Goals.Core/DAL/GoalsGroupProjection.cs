using System.Linq;
using MongoDB.Driver;
using SaveMeter.Modules.Goals.Core.DTO;
using SaveMeter.Modules.Goals.Core.Entities;

namespace SaveMeter.Modules.Goals.Core.DAL;

internal static class GoalsGroupProjection
{
    private static ProjectionDefinition<GoalsGroup, GoalsGroupDto> Projection =>
        Builders<GoalsGroup>.Projection.Expression(entity => new GoalsGroupDto
        {
            Id = entity.Id,
            TotalAmount = entity.TotalAmount,
            Title = entity.Title,
            Goals = entity.Goals.Select(x => new GoalDto
            {
                Title = x.Title,
                Amount = x.Amount,
                Id = x.Id,
                Rank = x.Rank,
            }).ToList()
        });

    public static IFindFluent<GoalsGroup, GoalsGroupDto> ProjectToGoalsGroupDto(this IFindFluent<GoalsGroup, GoalsGroup> fluent)
    {
        return fluent.Project(Projection);
    }
}