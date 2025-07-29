using Cofidis.Domain.Entities;

namespace Cofidis.Domain.Services;

public class RiskIndexCalculator
{
    public int Calculate(CreditApplication app)
    {
        var risk = 1;
        
        risk += app.UnemploymentRate.EvaluateRisk();
        risk += app.InflationRate.EvaluateRisk();
        risk += app.CreditHistoryStatus.EvaluateRisk();
        
        return risk;
    }
}