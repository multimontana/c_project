namespace InfraManager.WebApi.BLL.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InfraManager.WebApi.BLL.Repositories.Contracts;
    using InfraManager.WebApi.DAL;

    using Microsoft.EntityFrameworkCore;

    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<TEntity> _dbSet;

        #region Initialization

        protected RepositoryBase(TmContext tmContext)
        {
            this._dbContext = tmContext;
            this._dbSet = tmContext.Set<TEntity>();
        }

        #endregion

        public async Task CreateAsync(TEntity entity)
        {
            this._dbSet.Add(entity);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            TEntity entityToDelete = this._dbSet.Find(id);
            this._dbContext.Entry(entityToDelete).State = EntityState.Deleted;

            await this._dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entityToDelete)
        {
            if (this._dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                this._dbSet.Attach(entityToDelete);
            }

            this._dbSet.Remove(entityToDelete);

            await this._dbContext.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(TEntity entity)
        {
            this._dbSet.Add(entity);
            await this._dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> Get(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = this._dbSet;
            try
            {
                this.Include(ref query, includeProperties);

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

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await this._dbSet.FindAsync(id);
        }

        private void Include(ref IQueryable<TEntity> query, params string[] includeProperties)
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }
    }
}