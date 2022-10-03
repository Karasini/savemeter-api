using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SaveMeter.Shared.Abstractions.DAL
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        TEntity Update(TEntity entity);
        void Remove(TEntity obj);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);
        Task<bool> AnyAsync();
    }
}