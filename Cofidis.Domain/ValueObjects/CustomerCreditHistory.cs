using Cofidis.Domain.Interfaces;

namespace Cofidis.Domain.ValueObjects;

public record CreditHistoryStatus(CreditHistoryStatusEnum Value) : IRiskFactor
{
    public static readonly CreditHistoryStatus OnTime = new(CreditHistoryStatusEnum.OnTime);
    public static readonly CreditHistoryStatus Late30Days = new(CreditHistoryStatusEnum.Late30Days);
    public static readonly CreditHistoryStatus Overdue = new(CreditHistoryStatusEnum.Overdue);
    
    public int EvaluateRisk()
    {
        return Value switch
        {
            CreditHistoryStatusEnum.OnTime => 0,
            CreditHistoryStatusEnum.Late30Days => 1,
            CreditHistoryStatusEnum.Overdue => 2,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum CreditHistoryStatusEnum
{
    OnTime,
    Late30Days,
    Overdue
}
