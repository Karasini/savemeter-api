using System.Threading.Tasks;
using SaveMeter.Shared.Infrastructure;

namespace SaveMeter.Modules.MoneySources.Core.DAL;

internal class MoneySourceInitializer : IInitializer
{
    public Task InitAsync()
    {
        return Task.CompletedTask;
    }
}