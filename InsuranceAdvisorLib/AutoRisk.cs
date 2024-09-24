namespace BankRisk;
public class AutoRisk : ACollectionRisk<Vehicle>
{
    public AutoRisk(CalcMap calcMap) : base(calcMap)
    {
    }

    protected override void CalcItemScore(Vehicle item, ref int score)
    {
        if (DateTime.Today.Year - item.Year >= 5)
        {
            score++;
        }
    }

    protected override Vehicle[]? GetItems()
    {
        return Input.Vehicles;
    }
}