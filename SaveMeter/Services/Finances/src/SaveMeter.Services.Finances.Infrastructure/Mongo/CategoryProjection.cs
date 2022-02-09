using MongoDB.Driver;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Domain.Aggregates.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo
{
    static class CategoryProjection
    {
        public static ProjectionDefinition<Category, CategoryDto> Projection =>
            Builders<Category>.Projection.Expression(category => new CategoryDto
            {
                Id = category.Id,
                CategoryName = category.Name,
            });

        public static IFindFluent<Category, CategoryDto> ProjectToCategoryDto(this IFindFluent<Category, Category> fluent)
        {
            return fluent.Project(Projection);
        }
    }
}
