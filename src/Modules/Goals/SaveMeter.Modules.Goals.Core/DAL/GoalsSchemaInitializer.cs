using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SaveMeter.Modules.Goals.Core.Entities;
using SaveMeter.Shared.Infrastructure.Mongo;

namespace SaveMeter.Modules.Goals.Core.DAL;

internal class GoalsSchemaInitializer : ISchemaInitializer
{
    public void Initialize()
    {
        
        BsonClassMap.RegisterClassMap<FinancialGoalGroup>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
            map.MapMember(x => x.TotalAmount).SetSerializer(new DecimalSerializer(BsonType.Decimal128));
        });

        BsonClassMap.RegisterClassMap<FinancialGoal>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
            map.MapMember(x => x.Amount).SetSerializer(new DecimalSerializer(BsonType.Decimal128));
        });
    }
}