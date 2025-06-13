using BL.Api;
using BL.Models;
using BL.Services;
using Dal;
using Dal.Api;
using Dal.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BL
{
    public class BLManager : IBL
    {

        public IBLUser User { get;  }

        public IBLCategory Category { get; }

        public IBLPrompt Prompt { get; }

        public IBLSubCategory SubCategory { get; }

       
        public BLManager(string connectiondb, IOptions<OpenAiSettings> openAiSettings) {


            ServiceCollection services = new ServiceCollection();
     
            services.AddSingleton<IDal>(d => new DalManager(connectiondb));
            services.AddScoped<IBLUser, UserManagement>();
            services.AddScoped<IBLCategory, CategoryManagement>();
            services.AddScoped<IBLPrompt,PromptHandling>();
            services.AddScoped<IBLPrompt>(provider => new PromptHandling(openAiSettings, provider.GetRequiredService<IDal>()));
            

            services.AddScoped<IBLSubCategory, SubCategoryManagment>();




           


            ServiceProvider serviceProvider = services.BuildServiceProvider();
            User = serviceProvider.GetRequiredService<IBLUser>();
            Category = serviceProvider.GetRequiredService<IBLCategory>();
            Prompt = serviceProvider.GetRequiredService<IBLPrompt>();
            SubCategory = serviceProvider.GetRequiredService<IBLSubCategory>();

        }

    }
}
