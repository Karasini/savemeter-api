using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SaveMeter.Modules.MoneySources.Core.Entities;
using SaveMeter.Shared.Infrastructure.Mongo;

namespace SaveMeter.Modules.MoneySources.Core.DAL;

internal class MongoEntitiesInitializer : ISchemaInitializer
{
    public void Initialize()
    {
        
        BsonClassMap.RegisterClassMap<MoneySource>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
            map.MapMember(x => x.Amount).SetSerializer(new DecimalSerializer(BsonType.Decimal128));
            map.MapMember(x => x.NativeAmount).SetSerializer(new DecimalSerializer(BsonType.Decimal128));
        });
    }
}