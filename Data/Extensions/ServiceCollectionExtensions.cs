using DataAccess.Contracts;
using DataAccess.Models;
using DataAccess.Repository;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMinionsData(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddOptions<MinionsDataOptions>().Configure(data => data.BasePath = "./_data");
            serviceProvider.AddScoped(typeof(IRepository<>), typeof(FileBasedRepository<>));
            serviceProvider.AddScoped(typeof(ICollectionRepository<,>), typeof(FileBasedCollectionRepository<,>));
            serviceProvider.AddScoped(typeof(ICollectionRepository<>), typeof(FileBasedRepository<>));
        }
    }
}
