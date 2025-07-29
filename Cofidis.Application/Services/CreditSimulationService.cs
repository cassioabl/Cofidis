using Cofidis.Domain.Services;
using Cofidis.Domain.ValueObjects;

namespace Cofidis.Application.Services;

public class CreditSimulationService(CreditEligibilityEvaluator creditEligibilityEvaluator, CustomerService customerService)
{
    private const decimal UnemploymentRate = 0.043M;
    private const decimal InflationRate = 0.0123M;
    
    public List<LoanQuote> Simulate(string customerTin)
    {
        var unemploymentRate = new UnemploymentRate(UnemploymentRate);
        var inflationRate = new InflationRate(InflationRate);
        var customer = customerService.GetCustomer(customerTin);
        var customerCreditHistory = creditEligibilityEvaluator.EvaluateCustomerHistory(customer);

        return null;
    }
}