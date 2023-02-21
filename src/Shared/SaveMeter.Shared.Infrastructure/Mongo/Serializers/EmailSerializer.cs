using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Shared.Abstractions.Kernel.ValueObjects;
using MongoDB.Bson.Serialization;

namespace SaveMeter.Shared.Infrastructure.Mongo.Serializers;
internal class EmailSerializer : SerializerBase<Email>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Email email)
    {
        context.Writer.WriteString(email.Value);
    }

    public override Email Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var stringEmail = context.Reader.ReadString();

        return new Email(stringEmail);
    }
}
