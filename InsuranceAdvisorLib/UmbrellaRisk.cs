namespace BankRisk;
public class UmbrellaRisk : AInputRisk
{
    private AutoRisk AutoRisk { get; set; }
    private DisabilityRisk DisabilityRisk { get; set; }
    private LifeRisk LifeRisk { get; set; }
    private HomeRisk HomeRisk { get; set; }
    public UmbrellaRisk(
        CalcMap calcMap,
        AutoRisk autoRisk,
        DisabilityRisk disabilityRisk,
        LifeRisk lifeRisk,
        HomeRisk homeRisk
    ) : base(calcMap)
    {
        AutoRisk = autoRisk;
        DisabilityRisk = disabilityRisk;
        LifeRisk = lifeRisk;
        HomeRisk = homeRisk;
    }

    protected override RiskProfileEnum CalcScore()
    {
        return
        (
            DisabilityRisk.IsEconomic ||
            LifeRisk.IsEconomic ||
            AutoRisk.IsEconomic ||
            HomeRisk.IsEconomic
        ) ?
        MapScore(CalcMap.BaseScore) :
        RiskProfileEnum.ineligible;
    }
}