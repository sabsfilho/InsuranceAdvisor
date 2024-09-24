namespace BankRisk;
public class CalcMap
{
    public Input Input { get; private set; }
    public int AgePointsToDeduct { get; private set; }
    public int BaseScore { get; private set; }
    public int GeneralDeductionAmount { get; private set; }
    public bool HasDependents => Input.Dependents > 0;
    public bool HasHighIncome => Input.Income > 200_000;
    public bool IsMarried => Input.MaritalStatus == "married";
    
    public CalcMap(Input input)
    {
        this.Input = input;

        CalcBaseScore();

        CalcGeneralDeductionAmount();
    }
    private void CalcBaseScore()
    {
        BaseScore = 
            Input.RiskQuestions == null ? 
            0 :
            Input.RiskQuestions.Sum();
    }

    private void CalcGeneralDeductionAmount()
    {
        AgePointsToDeduct =
            Input.Age < 30 ? 2 :
            Input.Age is >= 30 and < 40 ? 1 :
            0;
        
        GeneralDeductionAmount = AgePointsToDeduct;

        if (HasHighIncome) 
            GeneralDeductionAmount--;

    }
}
