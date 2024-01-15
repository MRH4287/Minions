using DataAccess.Contracts;
namespace MAAM.Models

{
    public class Job : IModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double Payment { get; set; }

        public string? Requirement { get; set; }



        public override string ToString()
        {
            return Name ?? "";
        }




    }
}
