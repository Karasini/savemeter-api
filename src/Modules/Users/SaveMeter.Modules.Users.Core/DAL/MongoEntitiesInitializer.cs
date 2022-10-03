using SaveMeter.Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Shared.Infrastructure.Mongo;

namespace SaveMeter.Modules.Users.Core.DAL;
internal class MongoEntitiesInitializer : ISchemaInitializer
{
    public void Initialize()
    {
        BsonClassMap.RegisterClassMap<User>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
            map.MapProperty(x => x.RoleIds);
            map.GetMemberMap(x => x.Roles).SetShouldSerializeMethod(_ => false);
        });

        BsonClassMap.RegisterClassMap<Role>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
        });
    }
}
