using Microsoft.Extensions.DependencyInjection;
using DataAccess.Repositories;
using DataAccess.Repositories.Contracts;

namespace CRUD.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}