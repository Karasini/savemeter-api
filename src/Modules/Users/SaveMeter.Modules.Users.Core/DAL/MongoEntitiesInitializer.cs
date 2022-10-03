using SaveMeter.Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
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
            map.MapMember(x => x.Email.Value).SetElementName("Email");
            map.UnmapMember(x => x.Role);
        });

        BsonClassMap.RegisterClassMap<Role>(map =>
        {
            map.AutoMap();
            map.SetIgnoreExtraElements(true);
        });
    }
}
