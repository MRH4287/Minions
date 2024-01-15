using MAAM.Models;

namespace MAAM.Components.Pages
{
    public partial class WorkersList
    {
        #region Liste
      
        public Asset Asset { get; set; } = new Asset();

        #endregion






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
