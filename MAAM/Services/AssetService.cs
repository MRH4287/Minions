using MAAM.Models;

namespace MAAM.Services
{
    public class AssetService
    {   
        public void AddTimeCat(Asset element, int time)
        {

            foreach (var worker in element.Workers )
            {
                worker.DayWithoutPay = worker.DayWithoutPay + time;
                worker.TimeOnBord = worker.TimeOnBord + time;

            }
        }
    }
}
