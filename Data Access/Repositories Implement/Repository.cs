using Business_Core.IRepositories;
using Data_Access.DataContext_Class;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories_Implement
{
    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
    {
        private readonly DataContext _Context;

        public Repository(DataContext Context)
        {
            _Context = Context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _Context.Set<TEntity>().AddRangeAsync(entity);
            return entity;
        }

        public void DeleteAsync(TEntity entity)
        {
            _Context.Set<TEntity>().RemoveRange(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllSync()
        {
            return await _Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByKeyAsync(TKey Id)
        {
            return await _Context.Set<TEntity>().FindAsync(Id);
        }
    }
}
