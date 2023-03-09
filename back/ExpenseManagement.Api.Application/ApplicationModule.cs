using ExpenseManagement.Api.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManagement.Api.Application
{
    /// <summary>
    /// Module Application
    /// </summary>
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddApplicationServices();
         
            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IExpenseManagementService, ExpenseManagementService>();
            services.AddScoped<ICategoryService, CategoryService>();
          
            return services;
        }
    }
}
