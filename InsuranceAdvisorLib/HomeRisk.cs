namespace BankRisk;
public class HomeRisk : ACollectionRisk<House>
{
    public bool HasMortgage { get; private set; }
    public HomeRisk(CalcMap calcMap) : base(calcMap)
    {
    }

    protected override void CalcItemScore(House item, ref int score)
    {
        if (item.IsMortgaged) 
        {
            score++;
            HasMortgage = true;
        }
    }

    protected override House[]? GetItems()
    {
        return Input.Houses;
    }
}