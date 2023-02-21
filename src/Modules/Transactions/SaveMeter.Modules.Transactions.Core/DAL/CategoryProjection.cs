using MongoDB.Driver;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Modules.Transactions.Core.Entities;

namespace SaveMeter.Modules.Transactions.Core.DAL
{
    static class CategoryProjection
    {
        private static ProjectionDefinition<Category, CategoryDto> Projection =>
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
