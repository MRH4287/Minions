using MAAM.Models;
using System.Security.Cryptography.X509Certificates;

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
                worker.CurrentPayment = worker.CurrentPayment + (worker.Payment * time);

            }

            //#region Schleifen
            //for (int i = 0; i < element.Workers.Count; i++ ) 

            //{
            //    var worker = element.Workers[i];

            //    worker.DayWithoutPay = worker.DayWithoutPay + time;
            //    worker.TimeOnBord = worker.TimeOnBord + time;
            //    worker.CurrentPayment = worker.CurrentPayment + (worker.Payment * time);

            //}

            //int j = 0;

            //while (j < element.Workers.Count)
            //{
            //    var worker = element.Workers[j];

            //    worker.DayWithoutPay = worker.DayWithoutPay + time;
            //    worker.TimeOnBord = worker.TimeOnBord + time;
            //    worker.CurrentPayment = worker.CurrentPayment + (worker.Payment * time);

            //    j++;
            //}
            //#endregion

        }






















    }
}
