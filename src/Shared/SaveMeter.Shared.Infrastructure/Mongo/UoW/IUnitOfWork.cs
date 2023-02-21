using System;
using System.Threading.Tasks;

namespace SaveMeter.Shared.Infrastructure.Mongo.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}