using Cofidis.Domain.Entities;

namespace Cofidis.Application.Services;

public class CustomerService
{
    public Customer GetCustomer(string tin)
    {
        return new Customer
        {
            TIN = "298884789",
            MonthlyIncome = 1500,
            MonthlyDebt = 300,
            CreditHistory =
            [
                new CustomerCredit
                {
                    MonthlyPayment = 100,
                    Principal = 2400,
                    TotalOverdue = 0,
                    TotalLate30Days = 0
                }
            ]
        };
    }
}