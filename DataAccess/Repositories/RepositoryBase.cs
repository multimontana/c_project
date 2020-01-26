using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        #region Initialization

        protected RepositoryBase(TmContext tmContext)
        {
            _dbContext = tmContext;
            _dbSet = tmContext.Set<TEntity>();
        }

        #endregion

        public async Task CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            _dbContext.Entry(entityToDelete).State = EntityState.Deleted;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            try
            {
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query);
                }
                else
                {
                    return query;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return query;
            }
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        private void Include(string includeProperties, ref IQueryable<TEntity> query)
        {
            foreach (var includeProperty in includeProperties.Split
                (new[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }
    }
}