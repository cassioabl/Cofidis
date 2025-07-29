using Cofidis.Domain.ValueObjects;

namespace Cofidis.Domain.Services;

public class LoanQuoteCalculator
{
    public LoanQuote Calculate(decimal principal, int termInMonths, decimal annualRate)
    {
        var monthlyRate = annualRate / 12;
        
        // PMT formula
        var denominator = 1 - Math.Pow((double)(1 + monthlyRate), -termInMonths);
        var monthlyPayment = principal * monthlyRate / (decimal)denominator;
        var totalRepayment = monthlyPayment * termInMonths;

        return new LoanQuote(
            principal,
            termInMonths,
            annualRate,
            Math.Round(monthlyPayment, 2),
            Math.Round(totalRepayment, 2)
        );
    }
}