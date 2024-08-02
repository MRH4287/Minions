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




        public double Payment => Asset.Workers?.Sum(x => x.Payment) ?? 0;

        public double CurrentPayment => Asset.Workers?.Sum(x => x.CurrentPayment) ?? 0;

        public int Broke => (int)Double.Floor((Asset.Money - CurrentPayment) / Payment);




        #region Crow
        public int Sailorselement { get; set; }
        public int Rowerelement { get; set; }
        public double RowerPayment { get; set; }
        public double SailorsPayment { get; set; }
        public int Sailors => (Asset.Workers?.Count(x => x.Job != "Rower") ?? 0) + (Sailorselement);
        public int Rower => (Asset.Workers?.Count(x => x.Job == "Rower") ?? 0) + (Rowerelement);

        #endregion


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
            //await repo.Save(element);
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
            if (!result.Canceled)
            {
                Asset.Workers.Add(element);
                //await repo.Save(element); 
            }
           
        }


















    }
}
