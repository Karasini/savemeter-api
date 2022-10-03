using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Modules.Users.Core.Repositories;
using SaveMeter.Shared.Abstractions.Kernel.Types;
using SaveMeter.Shared.Infrastructure;

namespace SaveMeter.Modules.Users.Core.DAL;
internal class UsersInitializer : IInitializer
{
    public Task InitAsync()
    {
        return Task.CompletedTask;
    }
}
