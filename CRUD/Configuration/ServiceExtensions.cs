﻿namespace CRUD.Configuration
{
    using Auth.Services;

    using DataAccess.Repositories;
    using DataAccess.Repositories.Contracts;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// The configure repository wrapper.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<ICallRepository, CallRepository>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        /// <summary>
        /// The authorize configure.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public static void ConfigureAuthUsers(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticateService, TokenAuthenticationService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
        }
    }
}