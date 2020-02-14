namespace InfraManager.WebApi.BLL.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepositoryBase<TEntity>
    {
        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(int id);

        Task DeleteAsync(TEntity entityToDelete);

        /// <summary>
        /// Get entity [order, including]
        /// </summary>
        /// <param name="orderBy">The order</param>
        /// <param name="includeProperties">The include property</param>
        /// <returns>
        /// The <see cref=" IQueryable"/>.
        /// /// </returns>
        IQueryable<TEntity> Get(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] includeProperties);

        Task<TEntity> GetByIdAsync(int id);
    }
}