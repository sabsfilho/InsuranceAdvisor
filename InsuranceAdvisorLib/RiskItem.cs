namespace BankRisk;
public class RiskItem
{
    public required int key { get; set; }
    public string? value { get; set; }
    internal bool IsEconomic { get; set; }
}