using DataAccess.Contracts;
using MAAM.Components.Pages;

namespace MAAM.Models
{
    public class Asset : IModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Type { get; set; }
        public double Money { get; set; }

        public double CurrentPayment { get; set; }
        public int DayWithoutPay { get; set; }
        public List<Worker> Workers { get; set; } = new List<Worker>();

        public List<DisplayBlockDefinition>? DisplayBlocks { get; set; }


        #region Crow
        public int UnnamedSailor { get; set; }
        public int UnnamedRower { get; set; }
        public double UnnamedRowerPayment { get; set; }
        public double UnnamedSailorsPayment { get; set; }
        public int Sailors => (Workers?.Count(x => x.Job != "Rower") ?? 0) + (UnnamedSailor);
        public int Rower => (Workers?.Count(x => x.Job == "Rower") ?? 0) + (UnnamedRower);

        #endregion

        public double SumOfCostsOfAllRowers => double.Round((UnnamedRowerPayment * UnnamedRower),2);

        public double SumOfAllSailorCosts => double.Round((UnnamedSailorsPayment * UnnamedSailor),2);
    }
}
