using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Shared.Infrastructure.Mongo;

public interface ISchemaInitializer
{
    void Initialize();
}
