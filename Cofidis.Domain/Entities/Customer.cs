namespace Cofidis.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public string TIN { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal MonthlyIncome { get; set; }
    public decimal MonthlyDebt { get; set; }
    public IEnumerable<CustomerCredit> CreditHistory { get; set; }
}

public class CustomerCredit
{
    public decimal Principal { get; set; }
    public decimal MonthlyPayment { get; set; }
    public decimal TotalOverdue { get; set; }
    public decimal TotalLate30Days { get; set; }
}
