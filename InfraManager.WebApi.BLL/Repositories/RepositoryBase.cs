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
        private readonly DbContext dbContext;

        private readonly DbSet<TEntity> dbSet;

        #region Initialization

        protected RepositoryBase(TmContext tmContext)
        {
            this.dbContext = tmContext;
            this.dbSet = tmContext.Set<TEntity>();
        }

        #endregion

        public async Task CreateAsync(TEntity entity)
        {
            

            this.dbSet.Add(entity);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            TEntity entityToDelete = this.dbSet.Find(id);
            this.dbContext.Entry(entityToDelete).State = EntityState.Deleted;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entityToDelete)
        {
            if (this.dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.dbSet.Attach(entityToDelete);
            }

            this.dbSet.Remove(entityToDelete);

            await this.dbContext.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(TEntity entity)
        {
            this.dbSet.Add(entity);
            await this.dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> Get(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dbSet;
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
            return await this.dbSet.FindAsync(id);
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