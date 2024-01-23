using MAAM.Models;

namespace MAAM.Services
{
    public class AssetService
    {
        
        
        public void AddTimeCat(Asset element, int time)
        {

            foreach (var item in element.Workers )
            {
                item.DayWithoutPay = item.DayWithoutPay + time;
                item.TimeOnBord = item.TimeOnBord + time;
                item.CurrentPayment = item.CurrentPayment + (item.Payment * time);

            }

        }






















    }
}
