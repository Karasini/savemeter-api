using Instapp.Common.MongoDb.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SaveMeter.Services.Finances.Domain.Aggregates.CategoryAggregate;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo
{
    static class MongoConfiguration
    {
        public static void RegisterEntities()
        {
            BsonClassMap.RegisterClassMap<Entity>(map =>
            {
                map.SetIsRootClass(true);
                map.MapIdMember(x => x.Id).SetSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
                map.MapMember(x => x.CreatedAt);
                map.MapMember(x => x.UpdatedAt);
            });

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

            BsonClassMap.RegisterClassMap<MoneySource>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.Amount).SetSerializer(new DecimalSerializer(BsonType.Decimal128));
                map.MapMember(x => x.NativeAmount).SetSerializer(new DecimalSerializer(BsonType.Decimal128));
            });
        }
    }
}