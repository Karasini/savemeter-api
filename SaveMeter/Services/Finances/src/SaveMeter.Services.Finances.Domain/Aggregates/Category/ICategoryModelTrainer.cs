using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Domain.Aggregates.Category
{
    interface ICategoryModelTrainer
    {
        void TrainData();
    }
}
