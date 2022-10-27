using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Shared.Infrastructure.Mongo;

namespace SaveMeter.Modules.Transactions.Core.DAL;
internal class MongoEntitiesInitializer : ISchemaInitializer
{
    public void Initialize()
    {
        BsonClassMap.RegisterClassMap<Category>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
        });
        
        BsonClassMap.RegisterClassMap<BankTransaction>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
            map.MapMember(x => x.Value).SetSerializer(new DecimalSerializer(BsonType.Decimal128));
        });
    }
}
