using Dal.Api;
using Dal.Models;
using Dal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dal
{
    public class DalManager : IDal
    {
        public IUser User { get; }

        public ICategory Category { get; }

        public IPrompt Prompt { get; }

        public ISubCategory SubCategory { get; }

        public DalManager(string connectiondb)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<DatabaseManager>(options => options.UseSqlServer(connectiondb));
            services.AddScoped<IUser, UserService>();
            services.AddScoped<ICategory, CategoryService>();
            services.AddScoped<IPrompt, PromptService>();
            services.AddScoped<ISubCategory, SubCategoryService>();
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            User = serviceProvider.GetRequiredService<IUser>();
            Category = serviceProvider.GetRequiredService<ICategory>();
            Prompt = serviceProvider.GetRequiredService<IPrompt>();
            SubCategory = serviceProvider.GetRequiredService<ISubCategory>();

        }
    }
}
