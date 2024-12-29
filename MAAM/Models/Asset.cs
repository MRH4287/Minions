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

        public List<Worker> Workers { get; set; } = new List<Worker>();
        public List<GenericWorker> GenericWorkers { get; set; } = new List<GenericWorker>();
        public List<DisplayBlockDefinition>? DisplayBlocks { get; set; }


        #region Crew

        public double Payment => Workers?.Sum(item => item.Payment) ?? 0;
        public double CurrentPayment => Workers?.Sum(item => item.CurrentPayment) ?? 0;

        public int UnnamedSailor => GenericWorkers?.Count(item => item.WorkerType != "Rower") ?? 0;
        public int UnnamedRower => GenericWorkers?.Count(item => item.WorkerType == "Rower") ?? 0;

        public double UnnamedRowerPayment => GenericWorkers
            ?.Where(item => item.WorkerType == "Rower")
            .Sum(item => item.Amount * item.Payment)
            ?? 0;

        public double UnnamedRowerCurrentPayment => GenericWorkers
            ?.Where(item => item.WorkerType == "Rower")
            .Sum(item => item.Amount * item.CurrentPayment)
            ?? 0;

        public double UnnamedSailorPayment => GenericWorkers
            ?.Where(item => item.WorkerType != "Rower")
            .Sum(item => item.Amount * item.Payment)
            ?? 0;

        public double UnnamedSailorCurrentPayment => GenericWorkers
            ?.Where(item => item.WorkerType != "Rower")
            .Sum(item => item.Amount * item.CurrentPayment)
            ?? 0;

        public int Sailors => (Workers?.Count(x => x.Job != "Rower") ?? 0) + (UnnamedSailor);
        public int Rower => (Workers?.Count(x => x.Job == "Rower") ?? 0) + (UnnamedRower);

        #endregion

        public double SumOfCostsOfAllRowers => double.Round((UnnamedRowerPayment), 2);

        public double SumOfAllSailorCosts => double.Round((UnnamedSailorPayment), 2);
    }
}
