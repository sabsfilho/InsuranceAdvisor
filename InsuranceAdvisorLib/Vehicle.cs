using System.Text.Json.Serialization;

namespace BankRisk;
public class Vehicle : ACollectionRiskItem
{    
    [JsonPropertyName("year")]
    public int Year { get; set; }
}