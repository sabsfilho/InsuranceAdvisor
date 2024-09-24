namespace BankRisk;
public abstract class ACollectionRisk<T> : ABase where T : ACollectionRiskItem
{
    protected abstract T[]? GetItems();
    protected abstract void CalcItemScore(T item, ref int score);

    private RiskItem[]? scores = null;

    protected CalcMap CalcMap { get; private set; }
    protected Input Input => CalcMap.Input;
    protected T[]? Items { get; private set; }
    public bool IsIneligible { get; private set; }
    public bool IsEconomic => !IsIneligible && Scores!.Any(x=>x.IsEconomic);
    public ACollectionRisk(CalcMap calcMap)
    {
        this.CalcMap = calcMap;

        Items = GetItems();

        IsIneligible = 
            Items == null ||
            Items.Length == 0;
    }
    public RiskItem[]? Scores
    {
        get
        {
            if (IsIneligible) return null;

            if (scores == null)
            {
                scores = CalcScores();
            }
            return scores;
        }
    }

    private RiskItem[] CalcScores()
    {
        return
            Items!
            .Select(x => CreateRiskItem(x))
            .ToArray();
    }

    private RiskItem CreateRiskItem(T item)
    {
        int score = CalcMap.BaseScore;

        score -= CalcMap.GeneralDeductionAmount;

        if (Items!.Length == 1)
            score++;

        CalcItemScore(item, ref score);

        var riskProfile = MapScore(score);
        
        return 
            new RiskItem()
            {
                key = item.Key,
                value = riskProfile.ToString(),
                IsEconomic =  riskProfile == RiskProfileEnum.economic
            };
    }
}