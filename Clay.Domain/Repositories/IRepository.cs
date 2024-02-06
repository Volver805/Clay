using Clay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clay.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity: BaseEntity
    {
        public IQueryable<TEntity> GetAll();
        public Task<TEntity> GetByIdAsync(int id);
        public Task<TEntity> CreateAsync(TEntity entity);
        public Task UpdateAsync(TEntity entity);
        public Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    }
}
