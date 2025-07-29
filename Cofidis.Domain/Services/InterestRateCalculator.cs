using Cofidis.Domain.Exceptions;

namespace Cofidis.Domain.Services;

public class InterestRateCalculator(decimal baseRate = 0.05M, decimal incrementPerRiskPoint = 0.02M)
{
    public decimal Calculate(int riskIndex)
    {
        return riskIndex switch
        {
            < 1 or > 5 => throw new ArgumentOutOfRangeException(nameof(riskIndex),
                "Risk index must be between 1 and 5."),
            5 => throw new CreditDeniedException(),
            _ => baseRate + (riskIndex - 1) * incrementPerRiskPoint
        };
    }
}