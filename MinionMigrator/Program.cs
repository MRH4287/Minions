using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

using OldAsset = Minions.Data.Asset;
using OldCampaign = Minions.Data.Campaign;
using OldWorker = Minions.Data.Worker;
using OldGenericWorker = Minions.Data.GenericWorker;

using NewAsset = MAAM.Models.Asset;
using NewJob = MAAM.Models.Job;
using NewRace = MAAM.Models.Race;
using NewWorker = MAAM.Models.Worker;
using NewSex = MAAM.Models.Sex;

using DataAccess.Contracts;
using Microsoft.Extensions.Logging;


namespace MinionMigrator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var app = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddLogging();

                    services.AddSingleton<Migrator>();


                    services.AddMinionsData();

                }).Build();

            var migrator = app.Services.GetRequiredService<Migrator>();

            await migrator.Run();

            Console.ReadLine();
        }
    }

    internal class Migrator(
        ILogger<Migrator> logger,
        ICollectionRepository<OldWorker> OldWorkerRepo,
        IRepository<NewAsset> NewAssetRepo,
        IRepository<NewRace> NewRaceRepo,
        IRepository<NewJob> NewJobRepo
    )
    {

        public async Task Run()
        {
            logger.LogInformation("Start Migration ...");

            OldWorkerRepo.SetCollectionId("Test");
            OldWorkerRepo.SetStrictTypeName(true);

            var oldWorker = (await OldWorkerRepo.GetAll()).ToList();
            logger.LogInformation("Found {count} old Worker", oldWorker.Count);

            var newAsset = new NewAsset()
            {
                Name = "Test",
            };

            var allRaces = (await NewRaceRepo.GetAll()).Select(r => r.Id).ToHashSet();
            var allJobs = (await NewJobRepo.GetAll()).Select(r => r.Description).ToHashSet();

            foreach (var item in oldWorker)
            {
                logger.LogInformation("Migrate {name} {surname}", item.Name, item.Surname);

                var raceName = item.Race.ToString();
                if (raceName.Contains("_"))
                {
                    raceName = raceName.Replace("_", " ");
                }

                if (!allRaces.Contains(raceName))
                {
                    var newRace = new NewRace() { Id = raceName };
                    await NewRaceRepo.Save(newRace);
                    allRaces.Add(raceName);
                }

                var jobName = item.Job?.Name;
                if (jobName != null && jobName.Contains("_"))
                {
                    jobName = jobName.Replace("_", " ");
                }

                if (jobName != null && !allJobs.Contains(jobName))
                {
                    var newJob = new NewJob() { Id = jobName };
                    await NewJobRepo.Save(newJob);
                    allJobs.Add(jobName);
                }

                var newWorker = new NewWorker()
                {
                    Name = item.Name,
                    Surname = item.Surname,
                    Age = item.Age,
                    Race = raceName,
                    Job = jobName,
                    Sex =  (NewSex)(int)item.Sex,
                    Rank = item.Rank,
                    Rating = item.Rating,
                    Payment = item.Payment,
                    CurrentPayment = item.CurrentPayment,
                    DayWithoutPay = item.DayWithoutPay,
                    ServiceStarted = item.ServiceStarted,
                    ServiceEnded = item.ServiceEnded,
                    TimeOnBord = item.TimeOnBord,
                    Inventory = item.Inventory,
                    Skills = item.Skillls,
                    Notes = item.Notes,
                    Deleted = item.Deleted,
                };

                newAsset.Workers ??= new List<NewWorker>();
                newAsset.Workers.Add(newWorker);
            }
            logger.LogInformation("Migrated {count} items", newAsset.Workers.Count);

            await NewAssetRepo.Save(newAsset);

            logger.LogInformation("Done");
        }

    }
}
