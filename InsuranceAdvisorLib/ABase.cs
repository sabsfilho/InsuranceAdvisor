namespace BankRisk;
public abstract class ABase
{
    protected static RiskProfileEnum MapScore(int score)
    {
        return score switch
        {
            <= 0 => RiskProfileEnum.economic,
            <= 2 => RiskProfileEnum.regular,
            _ => RiskProfileEnum.responsible
        };
    }

}