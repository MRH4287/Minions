using DataAccess.Contracts;
using MAAM.Components.Pages;
using System.Linq;
using System.Text.Json.Serialization;

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
        public List<GenericWorker> GenericWorkers { get; set; } = new List<GenericWorker>();
        public List<DisplayBlockDefinition>? DisplayBlocks { get; set; }


        #region HeaderItemProperties
        [JsonIgnore]
        public int GenericCrewCount => GenericWorkers?.Sum(x => x.Amount) ?? 0;
        [JsonIgnore]
        public double GenericCrewPayment => GenericWorkers?.Sum(x => x.Payment) ?? 0;

        #endregion

        #region DiplayBlockTemplateProperties
        [JsonIgnore]
        public int Sailors => ((Workers?.Count(x => x.Tags?.Any(y => y.Equals("Sailor")) ?? false) ?? 0) + (GenericWorkers?.Where(x => x.WorkerType == "Sailor").Sum(x => x.Amount)) ?? 0);
        [JsonIgnore]
        public int Rower => ((Workers?.Count(x => x.Job == "Rower") ?? 0) + (GenericWorkers?.Where(x => x.WorkerType == "Rower").Sum(x => x.Amount)) ?? 0);
        #endregion
    }
}
