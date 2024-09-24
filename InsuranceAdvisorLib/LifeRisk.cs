namespace BankRisk;
public class LifeRisk : AInputRisk
{
    public LifeRisk(CalcMap calcMap) : base(calcMap)
    {
    }
    protected override RiskProfileEnum CalcScore()
    {
        if (
            Input.Age >= 60
        )
        {
            return RiskProfileEnum.ineligible;
        }

        int score = CalcMap.BaseScore;

        score -= CalcMap.GeneralDeductionAmount;

        if (CalcMap.HasDependents)
            score++;
        
        if (CalcMap.IsMarried)
            score++;

        return 
            MapScore(score);
    }
}