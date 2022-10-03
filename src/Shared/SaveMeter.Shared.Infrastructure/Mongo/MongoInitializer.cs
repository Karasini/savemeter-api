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
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        BsonClassMap.RegisterClassMap<Entity>(map =>
        {
            map.SetIsRootClass(true);
            map.MapIdMember(x => x.Id);
            map.MapMember(x => x.CreatedAt);
            map.MapMember(x => x.UpdatedAt);
        });
    }
}
