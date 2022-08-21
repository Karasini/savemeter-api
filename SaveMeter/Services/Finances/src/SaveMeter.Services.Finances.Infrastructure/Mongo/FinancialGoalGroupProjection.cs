using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Domain.Aggregates.FinancialGoal;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo
{
    public static class FinancialGoalGroupProjection
    {
        public static ProjectionDefinition<FinancialGoalGroup, FinancialGoalGroupDto> Projection =>
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
                }).ToList()
            });

        public static IFindFluent<FinancialGoalGroup, FinancialGoalGroupDto> ProjectToFinancialGoalGroupDto(this IFindFluent<FinancialGoalGroup, FinancialGoalGroup> fluent)
        {
            return fluent.Project(Projection);
        }
    }
}
