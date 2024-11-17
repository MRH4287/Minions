namespace MAAM.Models;


public class Worker : BaseWorker
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }
    public string? Race { get; set; }
    public Sex Sex { get; set; }
    public string? Rank { get; set; }
    public int Rating { get; set; }
    public string? Job { get; set; }
    public string? Inventory { get; set; }
    public string? Skills { get; set; }
    public string? Condition { get; set; }
    public string? ImageUri { get; set; }
    public string? ExternalLink { get; set; }
}


public enum Sex { Male, Female, Other };