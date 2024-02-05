using Clay.Domain.Entities;
using Clay.Domain.Repositories;
using Clay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clay.Infrastructure.Repositories
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity: BaseEntity
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();

        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
