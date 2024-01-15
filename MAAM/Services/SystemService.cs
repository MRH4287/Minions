
using DataAccess.Contracts;
using MAAM.Models;

namespace MAAM.Services
{
    public class SystemService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
           var assetRepo = Provider.GetRequiredService<IRepository<Worker>>();
            await assetRepo.GetAll();

           
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public IServiceProvider Provider { get; set; }

        public SystemService(IServiceProvider provider)
        {
            Provider= provider.CreateScope().ServiceProvider;
        }
           

    }
}



