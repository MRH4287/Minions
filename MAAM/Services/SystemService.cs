
using DataAccess.Contracts;
using MAAM.Models;

namespace MAAM.Services
{
    public class SystemService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var AssetRepo = Provider.GetRequiredService<IRepository<Asset>>();
            var Asset = (await AssetRepo.GetAll()).ToList();

            var RaceRepo = Provider.GetRequiredService<IRepository<Race>>();
            var Race = (await RaceRepo.GetAll()).ToList();

            var JobRepo = Provider.GetRequiredService<IRepository<Job>>();
            var Job = (await JobRepo.GetAll()).ToList();



            #region Stard Chars


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
                );

            }

            #endregion
            #region Stard Race Lsit 

            if (Race.Count == 0)
            {

                #region List
                List<string> list =
                [
                "Aarakocra",
                    "Aasimar",
                    "Aetherborn",
                    "Astral_Elf",
                    "Autognome",
                    "Aven",
                    "Bugbear",
                    "Bullywug",
                    "Centaur",
                    "Changeling",
                    "Deep_Gnome",
                    "Dhampir",
                    "Dragonborn",
                    "Duergar",
                    "Dwarf",
                    "Eladrin",
                    "Elf",
                    "Fairy",
                    "Firbolg",
                    "Genasi",
                    "God",
                    "Giff",
                    "Gith",
                    "Githyanki",
                    "Githzerai",
                    "Glitchling",
                    "Gnoll",
                    "Gnome",
                    "Goblin",
                    "Goliath",
                    "Grimlock",
                    "Grung",
                    "Hadozee",
                    "Half_Elf",
                    "Half_Orc",
                    "Halfling",
                    "Harengon",
                    "Hexblood",
                    "Hobgoblin",
                    "Human",
                    "Kalashtar",
                    "Kender",
                    "Kenku",
                    "Khenra",
                    "Kobold",
                    "Kor",
                    "Kuo_Toa",
                    "Leonin",
                    "Lizardfolk",
                    "Locathah",
                    "Loxodon",
                    "Merfolk",
                    "Minotaur",
                    "Naga",
                    "Nekomata",
                    "Orc",
                    "Owlfolk",
                    "Owlin",
                    "Plasmoid",
                    "Rabbitfolk",
                    "Reborn",
                    "Revenant",
                    "Satyr",
                    "Sea_Elf",
                    "Shadar_Kai",
                    "Shifter",
                    "Simic_Hybrid",
                    "Siren",
                    "Skeleton",
                    "Tabaxi",
                    "Thri_kreen",
                    "Tiefling",
                    "Tortle",
                    "Triton",
                    "Troglodyte",
                    "Vampire",
                    "Vedalken",
                    "Verdan",
                    "Viashino",
                    "Warforged",
                    "Yuan_Ti",
                    "Yuan_ti_Pureblood",
                    "Zombie",
                ];

                #endregion

                foreach (var item in list)
                {
                    await RaceRepo.Save(
                                  new Race()
                                  {
                                      Name = item,
                                  }
                   );
                }













            }

            #endregion
            #region Stard Job Lsit 

            if (Job.Count == 0)
            {

                #region List
                List<string> list =
                [
                    "Rower",
                    "Sailor"
                ];

                #endregion

                foreach (var item in list)
                {
                    await JobRepo.Save(
                                  new Job()
                                  {
                                      Id = item,
                                  }
                   );
                }













            }

            #endregion


        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public IServiceProvider Provider { get; set; }

        public SystemService(IServiceProvider provider)
        {
            Provider = provider.CreateScope().ServiceProvider;
        }







    }
}



