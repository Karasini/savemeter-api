using System.Collections.Generic;

namespace SaveMeter.Shared.Infrastructure.Modules;

internal record ModuleInfo(string Name, IEnumerable<string> Policies);