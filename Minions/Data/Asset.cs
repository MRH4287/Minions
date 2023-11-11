using DataAccess.Contracts;

namespace Minions.Data
{
    public class Asset : IModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public string? Type { get; set; }

    }
}
