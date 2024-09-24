using System.Text.Json.Serialization;

namespace BankRisk;
public class House : ACollectionRiskItem
{    
    [JsonPropertyName("ownership_status")]
    public string? OwnershipStatus { get; set; }
    internal bool IsMortgaged => OwnershipStatus == "mortgaged";
}