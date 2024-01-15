
using DataAccess.Contracts;

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
    }
}
