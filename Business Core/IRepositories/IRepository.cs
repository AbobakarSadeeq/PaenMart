using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IRepositories
{
    public interface IRepository<TKey, TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllSync();
        Task<TEntity> GetByKeyAsync(TKey Id);
        Task<TEntity> AddAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
    }
}
