using Auth.Services;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Repositories;
using DataAccess.Repositories.Contracts;

namespace CRUD.Configuration
{
    /// <summary>
    /// Registration Containers
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureAuthUsers(this IServiceCollection services)
        {
            // configure DI for application services
            services.AddScoped<IAuthenticateService, TokenAuthenticationService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
        }
    }
}