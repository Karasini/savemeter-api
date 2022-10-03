using MongoDB.Bson.Serialization;
using SaveMeter.Shared.Abstractions.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using SaveMeter.Shared.Abstractions.Kernel.ValueObjects;
using SaveMeter.Shared.Infrastructure.Mongo.Serializers;

namespace SaveMeter.Shared.Infrastructure.Mongo;
internal class MongoInitializer : ISchemaInitializer
{
    public void Initialize()
    {
        BsonSerializer.RegisterSerializer(new EmailSerializer());

        BsonClassMap.RegisterClassMap<Entity>(map =>
        {
            map.SetIsRootClass(true);
            map.MapIdMember(x => x.Id).SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            map.MapMember(x => x.CreatedAt);
            map.MapMember(x => x.UpdatedAt);
        });
    }
}
