
using DataAccess.Contracts;
using MAAM.Models;

namespace MAAM.Services
{
    public class SystemService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
           var AssetRepo = Provider.GetRequiredService<IRepository<Asset>>();
            var Asset =  (await AssetRepo.GetAll()).ToList();
           

           
            



            if (Asset.Count == 0)
            {
                await AssetRepo.Save(
                    new Asset()
                    {

                        Name = "Insel",

                        Workers = new List<Worker>
                        {
                            new Worker()
                            {
                                Name ="Linda",
                                Sex = Sex.Female,
                                Race = Race.Nekomata,
                                Age = int.MaxValue,
                            },
                            new Worker()
                            {
                                Name="Tia",
                                Sex = Sex.Female,
                                Race = Race.Nekomata,
                                Age = int.MaxValue,
                                
                            },
                             new Worker()
                            {
                                Name="Mew",
                                Sex = Sex.Female,
                                Race = Race.Mew,
                                Age = int.MaxValue,
                            },
                              new Worker()
                            {
                                Name="Mira",
                                Sex = Sex.Female,
                                Race = Race.Mew,
                                Age = int.MaxValue,
                            },
                               new Worker()
                            {
                                Name="Nie",
                                Sex = Sex.Female,
                                Race = Race.God,
                                Age = int.MaxValue,
                            }
                        }


                    }
                ) ; 
               
            }








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



