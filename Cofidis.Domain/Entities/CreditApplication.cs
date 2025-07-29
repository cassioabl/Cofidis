using Cofidis.Domain.ValueObjects;

namespace Cofidis.Domain.Entities;

public class CreditApplication(
    UnemploymentRate unemployment,
    InflationRate inflation,
    CreditHistoryStatus creditHistoryStatus,
    int term,
    decimal requestedAmount
)
{
    public UnemploymentRate UnemploymentRate { get; } = unemployment;
    public InflationRate InflationRate { get; } = inflation;
    public CreditHistoryStatus CreditHistoryStatus { get; } = creditHistoryStatus;
    public int Term { get; } = term;
    public decimal RequestedAmount { get; } = requestedAmount;
}
