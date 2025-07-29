namespace Cofidis.Domain.ValueObjects;

public record CreditLimit(decimal monthlyIncome, decimal maxAllowed)
{
    public decimal MonthlyIncome { get; } = monthlyIncome;
    public decimal MaxAllowed { get; } = maxAllowed;
}