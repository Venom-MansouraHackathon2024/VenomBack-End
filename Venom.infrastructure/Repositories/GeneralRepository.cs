using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Venom.Domain.IRepositories;
using Venom.infrastructure.Persistance;

namespace Venom.infrastructure.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        public GeneralRepository(VenomDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> condition)
        {
            return _dbSet.Where(condition);
        }

        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);  
            }
            return query;
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.Where(condition);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);

        }
    }
}
