using System.Text.Json.Serialization;

namespace BankRisk;
public abstract class ACollectionRiskItem
{
    [JsonPropertyName("key")]
    public int Key { get; set; }
}