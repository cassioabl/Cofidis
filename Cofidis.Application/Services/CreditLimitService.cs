using Cofidis.Domain.ValueObjects;

namespace Cofidis.Application.Services;

public class CreditLimitService
{
    public CreditLimit GetLimit(decimal monthlyIncome)
    {
        if (monthlyIncome <= 0)
            throw new ArgumentException("Requested amount must be greater than 0.");

        return monthlyIncome switch
        {
            <= 1000 => new CreditLimit(monthlyIncome, 1000),
            <= 2000 => new CreditLimit(monthlyIncome, 2000),
            _ => new CreditLimit(monthlyIncome, 5000)
        };
    }
}