using Cofidis.Domain.Entities;
using Cofidis.Domain.Services;
using Cofidis.Domain.ValueObjects;

namespace Cofidis.Application.Services;

public class CreditApplicationService(
    RiskIndexCalculator riskIndexCalculator,
    InterestRateCalculator interestRateCalculator,
    LoanQuoteCalculator loanQuoteCalculator,
    CustomerService customerService,
    CreditEligibilityEvaluator creditEligibilityEvaluator,
    CreditLimitService creditLimitService
)
{
    private const decimal UnemploymentRate = 0.043M;
    private const decimal InflationRate = 0.0123M;

    public LoanQuote Evaluate(decimal requestedAmount, int termInMonths, string customerTin)
    {
        var unemploymentRate = new UnemploymentRate(UnemploymentRate);
        var inflationRate = new InflationRate(InflationRate);
        var customer = customerService.GetCustomer(customerTin);
        var creditLimit = creditLimitService.GetLimit(customer.MonthlyIncome);
        var customerCreditHistory = creditEligibilityEvaluator.EvaluateCustomerHistory(customer);

        creditEligibilityEvaluator.EvaluateRequestedAmount(requestedAmount, creditLimit);
        
        var creditApplication = new CreditApplication(
            unemploymentRate,
            inflationRate,
            customerCreditHistory,
            termInMonths,
            requestedAmount
        );

        var riskIndex = riskIndexCalculator.Calculate(creditApplication);
        var rate = interestRateCalculator.Calculate(riskIndex);

        var quote =  loanQuoteCalculator.Calculate(
            creditApplication.RequestedAmount,
            creditApplication.Term,
            rate
        );
        
        creditEligibilityEvaluator.EvaluateCustomerEffort(customer, quote.MonthlyPayment);

        return quote;
    }
}
