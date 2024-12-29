using DataAccess.Contracts;
namespace MAAM.Models;


public abstract class BaseWorker : IModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public double Payment { get; set; }
    public double CurrentPayment  => Payment * DayWithoutPay;
    public int DayWithoutPay { get; set; }
    public string? ServiceStarted { get; set; }
    public string? ServiceEnded { get; set; }
    public int TimeOnBord { get; set; }
    public string? Notes { get; set; }

    public List<string>? Tags { get; set; }

    public bool Deleted { get; set; }

}