using System.Threading.Tasks;

namespace SaveMeter.Shared.Infrastructure;

public interface IInitializer
{
    Task InitAsync();
}