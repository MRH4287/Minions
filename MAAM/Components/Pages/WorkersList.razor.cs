using MAAM.Models;

namespace MAAM.Components.Pages
{
    public partial class WorkersList
    {
        #region Liste

        public Asset Asset { get; set; } = new Asset();

        #endregion





        //public int broke => (int)Double.Floor((Asset.Money - CrewCurentPayment) / CrewPaymentDay);

        public int Sailors => (Asset.Workers?.Count(x => x.Job?.Name != "Rower") ?? 0) + (Sailorselement);
        public int Rower => (Asset.Workers?.Count(x => x.Job?.Name == "Rower") ?? 0) + (Rowerelement);

        public int Sailorselement { get; set; }
        public int Rowerelement { get; set; }
        public double RowerPayment { get; set; }
        public double fu { get; set; }


        

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

            AssetService.AddTimeCat(Asset, 1);
            await Repo.Save(Asset);
        }



























    }
}
