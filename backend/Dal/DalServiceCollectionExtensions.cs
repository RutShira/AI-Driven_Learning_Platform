// filepath: Dal/ServiceCollectionExtensions.cs

using Dal.Api;
using Dal.Models;
using Dal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dal
{
    public static class DalServiceCollectionExtensions
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DatabaseManager>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUser, UserService>();
            services.AddScoped<ICategory, CategoryService>();
            services.AddScoped<IPrompt, PromptService>();
            services.AddScoped<ISubCategory, SubCategoryService>();
            services.AddScoped<IDal, DalManager>();
            return services;
        }
    }
}