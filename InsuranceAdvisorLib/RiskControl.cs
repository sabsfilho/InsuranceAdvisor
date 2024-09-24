namespace BankRisk;
public static class RiskControl
{
    /*
    Determines the risk profile for **each** line of insurance
    and suggests an insurance plan to related risk profile

    returns RiskState

    #IMPORTANT !    
    RiskState.auto and RiskState.home are collections
        if null or empty, consider them ineligible
        or better check IsAutoIneligible, IsHomeIneligible

    input example:
{
  "age": 35,
  "dependents": 2,
  "houses": [
    {"key": 1, "ownership_status": "owned"},
    {"key": 2, "ownership_status": "mortgaged"}
  ],
  "income": 0,
  "marital_status": "married",
  "risk_questions": [0, 1, 0],
  "vehicles": [
    {"key": 1, "year": 2018}
  ]
}

    return example:
{
    "auto": [
        {
            "key": 1,
            "value": "regular"
        }
    ],
    "disability": "ineligible",
    "home": [
        {
            "key": 1,
            "value": "economic"
        },
        {
            "key": 2,
            "value": "regular"
        }
    ],
    "life": "regular",
    "umbrella": "regular"
}
    
    */
    public static RiskState Calculate(Input input)
    {
        var calcMap = new CalcMap(input);

        var home =  new HomeRisk(calcMap);

        var disability = new DisabilityRisk(home, calcMap);

        var life = new LifeRisk(calcMap);

        var auto = new AutoRisk(calcMap);

        var umbrella = new UmbrellaRisk(
            calcMap,
            auto,
            disability,
            life,
            home
        );

        var state = new RiskState()
        {
            auto = auto.Scores,
            IsAutoIneligible = auto.IsIneligible,
            disability = disability.ScoreText,
            home = home.Scores,
            IsHomeIneligible = home.IsIneligible,
            life = life.ScoreText,
            umbrella = umbrella.ScoreText
        };

        return state;
    }
}
