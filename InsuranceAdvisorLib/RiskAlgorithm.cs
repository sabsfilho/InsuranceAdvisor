namespace BankRiskLegacy;

public static class RiskAlgorithm
{
    public static Dictionary<string, object> Calculate(Input input)
    {
        int baseScore = input.RiskQuestions.Sum();
        int disabilityScore = baseScore;
        int lifeScore = baseScore;
        int[] autoScore = input.Vehicles?.Select(_ => baseScore).ToArray() ?? [];
        int[] homeScore = input.Houses?.Select(_ => baseScore).ToArray() ?? [];
        
        var output = new Dictionary<string, object>();

        if (input.Income <= 0)
        {
            output["disability"] = "ineligible";
        }

        if (input.Age >= 60)
        {
            output["disability"] = "ineligible";
            output["life"] = "ineligible";
        }

        if (input.Vehicles is null || input.Vehicles.Length == 0)
        {
            output.Add("auto", "ineligible");
        }
        else
        {
            if (input.Vehicles.Length == 1)
            {
                autoScore[0] += 1;
            }

            for (int i = 0; i < input.Vehicles.Length; i++)
            {
                if (DateTime.Today.Year - input.Vehicles[i].Year >= 5)
                {
                    autoScore[i] += 1;
                }
            }
        }

        if (input.Houses is null || input.Houses.Length == 0)
        {
            output.Add("home", "ineligible");
        }
        else
        {
            if (input.Houses.Length == 1)
            {
                homeScore[0] += 1;
            }
            
            bool hasMortgage = false;
            
            for (var i = 0; i < input.Houses.Length; i++)
            {
                if (input.Houses[i].OwnershipStatus == "mortgaged")
                {
                    homeScore[i] += 1;
                    hasMortgage = true;
                }
            }

            if (hasMortgage)
            {
                disabilityScore += 1;
            }
        }

        int agePointsToDeduct = 0;
        
        if (input.Age < 30)
        {
            agePointsToDeduct = 2;
        }

        if (input.Age is >= 30 and < 40)
        {
            agePointsToDeduct = 1;
        }

        if (agePointsToDeduct > 0)
        {
            DeductFromAll(ref disabilityScore, ref lifeScore, autoScore, homeScore, agePointsToDeduct);
        }

        if (input.Income > 200_000)
        {
            DeductFromAll(ref disabilityScore, ref lifeScore, autoScore, homeScore, 1);
        }

        if (input.Dependents > 0)
        {
            disabilityScore += 1;
            lifeScore += 1;
        }

        if (input.MaritalStatus == "married")
        {
            lifeScore += 1;
            disabilityScore -= 1;
        }

        //not ineligible
        if (!output.ContainsKey("disability"))
        {
            output["disability"] = MapScore(disabilityScore);
        }
        
        if (!output.ContainsKey("life"))
        {
            output["life"] = MapScore(lifeScore);
        }
        
        if (!output.ContainsKey("auto"))
        {
            var vehicles = new List<VehicleOutput>();

            for (var i = 0; i < input.Vehicles!.Length; i++)
            {
                vehicles.Add(new VehicleOutput
                {
                    Key = input.Vehicles[i].Key,
                    Value = MapScore(autoScore[i]) 
                });
            }

            output["auto"] = vehicles;
        }
        
        if (!output.ContainsKey("home"))
        {
            var houses = new List<HomeOutput>();

            for (var i = 0; i < input.Houses!.Length; i++)
            {
                houses.Add(new HomeOutput
                {
                    Key = input.Houses[i].Key,
                    Value = MapScore(homeScore[i]) 
                });
            }

            output["home"] = houses;
        }

        if (output["life"] == "economic" || output["disability"] == "economic")
        {
            output["umbrella"] = MapScore(baseScore);
        }
        else if (output["home"] != "ineligible" && ((List<HomeOutput>)output["home"]).Any(it => it.Value == "economic"))
        {
            output["umbrella"] = MapScore(baseScore);
        }
        else if (output["auto"] != "ineligible" &&
                 ((List<VehicleOutput>)output["auto"]).Any(it => it.Value == "economic"))
        {
            output["umbrella"] = MapScore(baseScore);
        }
        else
        {
            output["umbrella"] = "ineligible";
        }

        return output;
    }

    public static string MapScore(int score)
    {
        return score switch
        {
            <= 0 => "economic",
            <= 2 => "regular",
            _ => "responsible"
        };
    }
    
    private static void DeductFromAll(ref int disabilityScore, ref int lifeScore, int[] autoScore, int[] homeScore,
        int pointsToDeduct)
    {
        disabilityScore -= pointsToDeduct;
        lifeScore -= pointsToDeduct;
            
        for (var i = 0; i < autoScore.Length; i++)
        {
            autoScore[i] -= pointsToDeduct;
        }
            
        for (var i = 0; i < homeScore.Length; i++)
        {
            homeScore[i] -= pointsToDeduct;
        }
    }
}


public class Input
{
    public int Age { get; set; }
    
    public int Dependents { get; set; }
    
    public House[]? Houses { get; set; }
    
    public decimal Income { get; set; }
    
    public string MaritalStatus { get; set; }
    
    public int[] RiskQuestions { get; set; }
    
    public Vehicle[]? Vehicles { get; set; }
}

public class House
{
    public int Key { get; set; }
    
    public string OwnershipStatus { get; set; }
}

public class Vehicle
{
    public int Key { get; set; }
    
    public int Year { get; set; }
}

public class VehicleOutput
{
    public int Key { get; set; }
    
    public string Value { get; set; }
}

public class HomeOutput
{
    public int Key { get; set; }
    
    public string Value { get; set; }
}