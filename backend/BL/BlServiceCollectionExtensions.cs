// filepath: BL/BlServiceCollectionExtensions.cs

using BL.Api;
using BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BL
{
    public static class BlServiceCollectionExtensions
    {
        public static IServiceCollection AddBlServices(this IServiceCollection services)
        {
            services.AddScoped<IBLUser, UserManagement>();
            services.AddScoped<IBLCategory, CategoryManagement>();
            services.AddScoped<IBLPrompt, PromptHandling>();
            services.AddScoped<IBLSubCategory, SubCategoryManagment>();
            services.AddScoped<IBL, BLManager>();
            return services;
        }
    }
}