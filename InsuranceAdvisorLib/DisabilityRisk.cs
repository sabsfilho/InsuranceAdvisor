namespace BankRisk;
public class DisabilityRisk : AInputRisk
{
    private HomeRisk HomeRisk { get; set; }
    public DisabilityRisk(HomeRisk homeRisk, CalcMap calcMap) : base(calcMap)
    {
        HomeRisk = homeRisk;
    }

    protected override RiskProfileEnum CalcScore()
    {
        if (
            Input.Income <= 0 ||
            Input.Age >= 60
        )
        {
            return RiskProfileEnum.ineligible;
        }

        int score = CalcMap.BaseScore;

        if (HomeRisk.HasMortgage)
        {
            score++;
        }

        score -= CalcMap.GeneralDeductionAmount;

        if (CalcMap.HasDependents)
            score++;
        
        if (CalcMap.IsMarried)
            score--;

        return 
            MapScore(score);
    }
}