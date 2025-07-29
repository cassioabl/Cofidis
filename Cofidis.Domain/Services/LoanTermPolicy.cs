using Cofidis.Domain.Exceptions;

namespace Cofidis.Domain.Services;

public class LoanTermPolicy
{
    public IEnumerable<int> GetAllowedTerms(int riskIndex)
    {
        var (min, max) = riskIndex switch
        {
            1 => (12, 60),
            2 => (12, 54),
            3 => (12, 48),
            4 => (12, 42),
            5 => throw new CreditDeniedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(riskIndex), "Risk index must be between 1 and 5.")
        };

        for (var term = min; term <= max; term += 6)
            yield return term;
    }
}