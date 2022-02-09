using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.Cqrs.Commands;

namespace SaveMeter.Services.Finances.Application.Commands.UpdateTransaction
{
    public class UpdateTransactionCommand : CommandBase<Guid>
    {
        public Guid Id { get; set; }
        public string Customer { get; init; }
        public string Description { get; init; }
        public Guid? CategoryId { get; init; }
        public bool SkipAnalysis { get; init; }
    }
}
