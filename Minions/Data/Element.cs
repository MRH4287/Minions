namespace Minions.Data;

public class Element
{
    public required string Name { get; set; }
    public int Age { get; set; }
    public required string Race { get; set; }
    public required string Sex { get; set; }
    public float Payment { get; set; }
    public required string Job { get; set; }
    public float CurrentPayment => Payment * DayWihtoutPay;
    public int DayWihtoutPay { get; set; }

}
