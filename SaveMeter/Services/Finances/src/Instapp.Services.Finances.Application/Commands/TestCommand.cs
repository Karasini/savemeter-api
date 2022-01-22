using Instapp.Common.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instapp.Services.Finances.Application.Commands
{
    public class TestCommand<Guid> : CommandBase<Guid>
    {
        public string? TestProperty { get; set; }
    }
}
