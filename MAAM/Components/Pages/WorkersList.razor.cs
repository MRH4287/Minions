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
        public double RowerPayment {  get; set; }
        public double fu { get; set; }







        protected override async Task OnInitializedAsync()
        {
            Asset = (await Repo.GetAll()).First();
 
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




























    }
}
