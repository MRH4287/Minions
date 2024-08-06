using MAAM.Components.Dialog;
using MAAM.Models;
using MudBlazor;
using System.Linq.Expressions;

namespace MAAM.Components.Pages
{
    public partial class WorkersList
    {
        #region Liste

        public Asset Asset { get; set; } = new Asset();

        #endregion




        public double Payment => double.Round(( Asset.Workers?.Sum(x => x.Payment) ?? 0) + Asset.SumOfCostsOfAllRowers + Asset.SumOfAllSailorCosts ,2);

        public double CurrentPayment => double.Round( Asset.Workers?.Sum(x => x.CurrentPayment) ?? 0,2);

        public int Broke => (int)Double.Floor((Asset.Money - CurrentPayment) / Payment);






        protected override async Task OnInitializedAsync()
        {
            Asset = (await Repo.GetAll()).First();

        }

        public async Task Fire(Worker element)
        {
            Asset.Workers.Remove(element);

            await Repo.Save(Asset);
            //StateHasChanged();
        }
        public async Task Pay(Worker element)
        {

            Asset.Money = Asset.Money - element.CurrentPayment;
            element.DayWithoutPay = 0;
            element.CurrentPayment = 0;
            await Repo.Save(Asset);
        }

        public async Task Time()
        {

            AssetService.AddTimeCat(Asset, 1 );
            await Repo.Save(Asset);
        }






        private async Task EditChar(Worker element)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true,  FullWidth = true, CloseButton = true };
            var parameter = new DialogParameters<CharacterDialog>();

            
            parameter.Add(x => x.Element, element);
            parameter.Add(x => x.AssetId, Asset.Id);


            var dialog = await Dialog.ShowAsync<CharacterDialog>("", parameter, options);
            var result = await dialog.Result;
            await Repo.Save(Asset);
        }

        private async Task AddChar()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true,  FullWidth = true, CloseButton = true };
            var parameter = new DialogParameters<CharacterDialog>();
            var element = new Worker();


            parameter.Add(x => x.Element, element);
            parameter.Add(x => x.AssetId, Asset.Id);




            var dialog = await Dialog.ShowAsync<CharacterDialog>("", parameter, options);
            var result = await dialog.Result;
            if (result != null && !result.Canceled)
            {
                Asset.Workers.Add(element);
                await Repo.Save(Asset);
            }
           
        }










        public async Task AddUnnamedRower(int count)
        {
            Asset.UnnamedRower += count;
            await Repo.Save(Asset);
        }

        public async Task AddUnnamedRowerPayment(double payment)
        {
            Asset.UnnamedRowerPayment += payment;
            await Repo.Save(Asset);
        }

        public async Task AddUnnamedSailor(int count)
        {
            Asset.UnnamedSailor += count;
            await Repo.Save(Asset);
        }

        public async Task AddUnnamedSailorPayment(double payment)
        {
            Asset.UnnamedSailorsPayment += payment;
            await Repo.Save(Asset);
        }

        public async Task AddMoney(double money)
        {
            Asset.Money += money;
            await Repo.Save(Asset);
        }
     
    }
}
