using Minions.Data;
using Minions.Components.Pages.Dialog;
using MudBlazor;
using System.Net.NetworkInformation;

namespace Minions.Components.Pages.Minions_Start
{
    public partial class Start_Test_1
    {
        // This is just for now, later this will be changed
        public string AssetId { get; set; } = "Test";
        public Asset? Asset { get; set; }
        public List<Job> Jobs { get; set; } = new List<Job>();

        public int Time { get; set; } = 1;
        public double InPay { get; set; }
        public double Extract { get; set; }
        public int broke => (int)Double.Floor((Asset.Money - CrewCurentPayment) / CrewPaymentDay);

        public int Sailors => (Liste?.Count(x => x.Job?.Name != "Rower") ?? 0) + (Sailorselement);
        public int Rower => (Liste?.Count(x => x.Job?.Name == "Rower") ?? 0) + (Rowerelement);
        public double CrewPaymentDay => Liste?.Sum(X => X.Payment) ?? 0;
        public double CrewCurentPayment => Liste?.Sum(x => x.CurrentPayment) ?? 0;
        bool open;

        public int Sailorselement { get; set; }
        public int Rowerelement { get; set; }

        private List<GenericWorker>? ZweiteListe { get; set; } = new List<GenericWorker>();
        private List<Worker>? Liste { get; set; } = new List<Worker>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (!assetRepo.Exists(AssetId))
            {
                var newAsset = new Asset()
                {
                    Id = AssetId
                };
                await assetRepo.Save(newAsset);
            }
            Asset = await assetRepo.Get(AssetId);
            repo.SetCollectionId(AssetId);
            await UpdateList();
            Jobs = (await RepoJob.GetAll()).ToList();

            if (Jobs.Count == 0)
            {
                await RepoJob.Save(new Job
                {
                    Name = "Kartzenhüter",
                    Description = "Wacht Über Kätzechen",
                    Payment = 5,
                    Requirement = "Katzenlibhaber",
                });
                Jobs = (await RepoJob.GetAll()).ToList();

            }

            if (Liste.Count == 0)
            {
                await repo.Save(new Worker
                {

                    Name = "Test",
                    Age = 12,
                    Race = Race.Nekomata,
                    Sex = Sex.Female,
                    Payment = 30,
                    Job = Jobs.FirstOrDefault(),

                });
                await repo.Save(new Worker
                {
                    Name = "Igor",
                    Age = 50,
                    Race = Race.Dragonborn,
                    Sex = Sex.Male,
                    Payment = 50,
                    Job = Jobs.FirstOrDefault(),
                });
                await UpdateList();
            }

            // ZweiteListe.Add(Rowerelement = new GenericWorker() { Name = "Rower" });
            // ZweiteListe.Add(Sailorselement = new GenericWorker() { Name = "Sailor" });


        }

        private async Task UpdateList()
        {
            Liste = (await repo.GetAll()).Where(i => !i.Deleted).ToList();
        }






        public async Task TimeButten()
        {

            foreach (var item in Liste)
            {
                item.DayWithoutPay = item.DayWithoutPay + Time;
                item.TimeOnBord = item.TimeOnBord + Time;
                item.CurrentPayment = item.CurrentPayment + (item.Payment * Time);

                await repo.Save(item);
            }

            Time = 1;
        }
        public async Task AddChar()
        {
            var options = new DialogOptions()
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
            };

            var parameter = new DialogParameters<AddCrewDialog>();
            var element = new Worker();
            parameter.Add(x => x.Element, element);
            var dialog = await Dialog.ShowAsync<AddCrewDialog>("AddCrew", parameter, options);
            var result = await dialog.Result;
            if (!result.Canceled)
            {

                element.TimeOnBord = element.DayWithoutPay;

                Liste.Add(element);
                await repo.Save(element);
            }




        }
        private async Task CharacterSheet(Worker element)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true, };
            var parameter = new DialogParameters<CharacterSheet>();


            parameter.Add(x => x.Element, element);


            var dialog = await Dialog.ShowAsync<CharacterSheet>("CharacterSheet", parameter, options);
            var result = await dialog.Result;
            await repo.Save(element);
        }
        //public async void Fire(Worker element)
        //{
        //    element.Deleted = true;
        //    await repo.Save(element);
        //    await UpdateList();

        //    StateHasChanged();
        //    // Liste.Remove(element);
        //    // repo.Delete(element.Id);
        //}


        //public async Task Pay(Worker element)
        //{

        //    Asset.Money = Asset.Money - element.CurrentPayment;
        //    element.DayWithoutPay = 0;
        //    element.CurrentPayment = 0;
        //    await repo.Save(element);
        //    await assetRepo.Save(Asset);

        //}



        //public async Task GetMoney()
        //{
        //    Asset.Money = Asset.Money + InPay;
        //    InPay = 0;
        //    await assetRepo.Save(Asset);
        //}
        //public async Task ExtractMoney()
        //{
        //    Asset.Money = Asset.Money - Extract;
        //    Extract = 0;
        //    await assetRepo.Save(Asset);
        //}
        public async Task SaveAsset()
        {
            if (Asset == null)
            {
                return;
            }
            await assetRepo.Save(Asset);
        }
        void OpenDrawer()
        {
            open = true;

        }

        //public async Task addSailor()
        //{
        //    var result = await Notify.Prompt("Count");
        //    if (!int.TryParse(result, out var number))
        //    {
        //        return;
        //    }

        //    Sailorselement += number;

        //}
        public async Task FireRower(GenericWorker element)
        {
            var result = await Notify.Prompt("Count");
            if (!int.TryParse(result, out var number))
            {
                return;
            }

            element.Count -= number;

        }

        public async Task paymentRower()
        {
            var result = await Notify.Prompt("Count");
            if (!int.TryParse(result, out var number))
            {
                return;
            }

            Rowerelement += number;
        }
        public async Task paymentSailor()
        {
            var result = await Notify.Prompt("Count");
            if (!int.TryParse(result, out var number))
            {
                return;
            }

            Sailorselement += number;
        }

        public async Task Switsh()
        {
            var result = await Notify.Prompt("New Name");

            Asset.Name = result;

            StateHasChanged();
        }


        //public async Task addRower()
        //{
        //    var result = await Notify.Prompt("Count");
        //    if (!int.TryParse(result, out var number))
        //    {
        //        return;
        //    }

        //    Rowerelement += number;


        //}









    }
}
