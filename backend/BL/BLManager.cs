using BL.Api;
using BL.Services;
using Dal;
using Dal.Api;
using Dal.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BL
{
    public class BLManager : IBL
    {

        public IBLUser User { get;  }

        public IBLCategory Category { get; }

        public IBLPrompt Prompt { get; }

        public IBLSubCategory SubCategory { get; }
        public BLManager(string connectiondb) {

            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IDal>(d => new DalManager(connectiondb));
            services.AddScoped<IBLUser, UserManagement>();
            services.AddScoped<IBLCategory, CategoryManagement>();
            services.AddScoped<IBLPrompt, PromptHandling>();
          

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            User = serviceProvider.GetRequiredService<IBLUser>();
            Category = serviceProvider.GetRequiredService<IBLCategory>();
            Prompt = serviceProvider.GetRequiredService<IBLPrompt>();
            
        }

    }
}
