namespace Minions.Data;

public class Element
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Race { get; set; }
    public string? Sex { get; set; }
    public float Payment { get; set; }
    public string? Job { get; set; }
    public float CurrentPayment => Payment * DayWihtoutPay;
    public int DayWihtoutPay { get; set; }

}
