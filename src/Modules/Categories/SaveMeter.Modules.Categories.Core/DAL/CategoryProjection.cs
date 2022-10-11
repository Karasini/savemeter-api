using MongoDB.Driver;
using SaveMeter.Modules.Categories.Core.DTO;
using SaveMeter.Modules.Categories.Core.Entities;

namespace SaveMeter.Modules.Categories.Core.DAL
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
