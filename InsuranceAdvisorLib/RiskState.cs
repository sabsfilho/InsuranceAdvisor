namespace BankRisk;
public class RiskState
{
    public bool IsAutoIneligible { get; set; }
    public RiskItem[]? auto { get; set; }
    public bool IsHomeIneligible { get; set; }
    public RiskItem[]? home { get; set; }
    public string? disability { get; set; }
    public string? life { get; set; }
    public string? umbrella { get; set; }
}