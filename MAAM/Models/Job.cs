using DataAccess.Contracts;
namespace MAAM.Models

{
    public class Job : IModel
    {
        public required string Id { get; set; } 

        

        public string? Description { get; set; }

        public double Payment { get; set; }

        public string? Requirement { get; set; }



        public override string ToString()
        {
            return Id ?? "";
        }




    }
}
