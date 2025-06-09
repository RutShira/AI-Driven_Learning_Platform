using BL.Api;
using Dal;
using Dal.Api;
using Microsoft.Extensions.DependencyInjection;

namespace BL
{
    public class BLManager: IBL
    {

        public BLManager(string connectiondb) {

            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IDal>(d => new DalManager(connectiondb));
            ServiceProvider serviceProvider = services.BuildServiceProvider();
        }
    }
}
