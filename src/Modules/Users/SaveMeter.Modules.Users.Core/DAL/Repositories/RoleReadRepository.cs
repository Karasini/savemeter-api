using SaveMeter.Shared.Infrastructure.Mongo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Shared.Infrastructure.Mongo.Context;

namespace SaveMeter.Modules.Users.Core.DAL.Repositories;
internal class RoleReadRepository : ReadBaseRepository<Role>
{
    public RoleReadRepository(IMongoContext context) : base(context)
    {
    }
}
