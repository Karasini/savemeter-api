using MongoDB.Bson.Serialization;
using SaveMeter.Shared.Abstractions.Kernel.Types;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using SaveMeter.Shared.Infrastructure.Mongo.Serializers;

namespace SaveMeter.Shared.Infrastructure.Mongo;
internal class MongoInitializer : ISchemaInitializer
{
    public void Initialize()
    {
        BsonSerializer.RegisterSerializer(new EmailSerializer());
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;

        ConventionRegistry.Register("EnumStringConvention", new ConventionPack
        {
            new EnumRepresentationConvention(BsonType.String)
        }, t => true);

        BsonClassMap.RegisterClassMap<Entity>(map =>
        {
            map.SetIsRootClass(true);
            map.MapIdMember(x => x.Id);
            map.MapMember(x => x.CreatedAt);
            map.MapMember(x => x.UpdatedAt);
        });
    }
}
