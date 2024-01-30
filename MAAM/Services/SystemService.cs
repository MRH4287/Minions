
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

            var RaceRepo = Provider.GetRequiredService<IRepository<Race>>();
            var Race = (await RaceRepo.GetAll()).ToList();






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
                                Race = "Nekomata",
                                Age = int.MaxValue,
                                Payment = 2,
                            },
                            new Worker()
                            {
                                Name="Tia",
                                Sex = Sex.Female,
                                Race = "Mew",
                                Age = int.MaxValue,
                                Payment = 2,
                            },
                             new Worker()
                            {
                                Name="Mew",
                                Sex = Sex.Female,
                                Race = "Mew",
                                Age = int.MaxValue,
                                Payment = 2,
                            },
                              new Worker()
                            {
                                Name="Mira",
                                Sex = Sex.Female,
                                Race = "Nekomata",
                                Age = int.MaxValue,
                                Payment = 2,
                            },
                               new Worker()
                            {
                                Name="Nie",
                                Sex = Sex.Female,
                                Race = "God",
                                Age = int.MaxValue,
                                Payment = 2,
                            }
                        }


                    }
                ) ; 
               
            }



            if (Race.Count == 0)
            {
                await RaceRepo.Save(
                    new Race()
                    {
                        Name="Nekomata"
                    }
                );

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



