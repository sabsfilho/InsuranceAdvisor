using System.Text.Json.Serialization;

namespace BankRisk;
public class Input
{
    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("dependents")]
    public int Dependents { get; set; }
    
    [JsonPropertyName("houses")]
    public House[]? Houses { get; set; }
    
    [JsonPropertyName("income")]
    public decimal Income { get; set; }
    
    [JsonPropertyName("marital_status")]
    public string? MaritalStatus { get; set; }
    
    [JsonPropertyName("risk_questions")]
    public int[]? RiskQuestions { get; set; }
    
    [JsonPropertyName("vehicles")]
    public Vehicle[]? Vehicles { get; set; }

}
