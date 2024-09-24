namespace BankRisk;
public abstract class ARisk : ABase
{
    private RiskProfileEnum? score = null;
    protected abstract RiskProfileEnum CalcScore();
    public string ScoreText => Score.ToString();
    public bool IsEconomic => Score == RiskProfileEnum.economic;
    public RiskProfileEnum Score 
    { 
        get
        {
            if (score == null)
            {
                score = CalcScore();
            }
            return score.Value;
        }
    }

}