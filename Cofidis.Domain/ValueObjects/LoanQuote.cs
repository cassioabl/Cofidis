namespace Cofidis.Domain.ValueObjects;

public record LoanQuote(
    decimal Principal,
    int TermInMonths,
    decimal InterestRate,
    decimal MonthlyPayment,
    decimal TotalRepayment
);
