using DataAccess.Contracts;

namespace MAAM.Models
{
    public class Race: IModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
    }
}
