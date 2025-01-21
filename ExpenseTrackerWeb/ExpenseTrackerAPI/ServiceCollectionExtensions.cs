using ExpenseTracker.Application.Services;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Infrastructure.Data.Repositories;

namespace ExpenseTrackerAPI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services) 
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            
            return services;
        }
    }
}
