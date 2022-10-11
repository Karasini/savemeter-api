using MongoDB.Bson.Serialization;
using SaveMeter.Modules.Categories.Core.Entities;
using SaveMeter.Shared.Infrastructure.Mongo;

namespace SaveMeter.Modules.Categories.Core.DAL;
internal class MongoEntitiesInitializer : ISchemaInitializer
{
    public void Initialize()
    {
        BsonClassMap.RegisterClassMap<Category>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
        });
    }
}
