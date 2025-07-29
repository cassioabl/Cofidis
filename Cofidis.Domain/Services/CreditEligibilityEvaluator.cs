using Cofidis.Domain.Entities;
using Cofidis.Domain.Exceptions;
using Cofidis.Domain.ValueObjects;

namespace Cofidis.Domain.Services;

public class CreditEligibilityEvaluator
{
    private decimal MaxCustomerEffort = 0.3m;
    public CreditHistoryStatus EvaluateCustomerHistory(Customer customer)
    {
        return customer.CreditHistory.Any(p => p.TotalOverdue > 0) ? CreditHistoryStatus.Overdue :
                customer.CreditHistory.Any(p => p.TotalLate30Days > 0) ? CreditHistoryStatus.Late30Days :
                CreditHistoryStatus.OnTime;
    }
    
    public void EvaluateRequestedAmount(decimal requestedAmount, CreditLimit limit)
    {
        if (requestedAmount > limit.MaxAllowed)
            throw new CreditLimitExceededException();
    }

    public void EvaluateCustomerEffort(Customer customer, decimal monthlyPayment)
    {
        var totalMonthlyPayment = customer.MonthlyDebt + monthlyPayment;
        var effortRatio = totalMonthlyPayment / customer.MonthlyIncome;

        if (effortRatio > MaxCustomerEffort)
        {
            throw new ExcessiveEffortException();
        }
    }
}