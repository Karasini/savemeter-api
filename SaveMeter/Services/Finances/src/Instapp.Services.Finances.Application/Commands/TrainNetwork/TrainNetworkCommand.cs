using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.Cqrs.Commands;

namespace Instapp.Services.Finances.Application.Commands.TrainNetwork
{
    public class TrainNetworkCommand : CommandBase<string>
    {
        public string Customer { get; set; }
        public string Description { get; set; }
    }
}
