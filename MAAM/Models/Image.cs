using DataAccess.Contracts;

namespace MAAM.Models
{
    public class Image : IModel
    {
        public required string Id { get; init; }

        public required byte[] Data { get; set; }

        public required string ContentType { get; set; }
    }
}
