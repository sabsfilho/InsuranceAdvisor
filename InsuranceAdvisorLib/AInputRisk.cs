namespace BankRisk;
public abstract class AInputRisk : ARisk
{
    protected CalcMap CalcMap { get; private set; }

    protected Input Input => CalcMap.Input;

    public AInputRisk(CalcMap calcMap)
    {
        CalcMap = calcMap;
    }

}