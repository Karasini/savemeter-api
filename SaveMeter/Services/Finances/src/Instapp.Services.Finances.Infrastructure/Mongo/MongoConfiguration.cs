﻿using Instapp.Common.MongoDb.Models;
using Instapp.Services.Finances.Domain.Aggregates.Category;
using Instapp.Services.Finances.Domain.Aggregates.Transaction;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Instapp.Services.Finances.Infrastructure.Mongo
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
            });
        }
    }
}