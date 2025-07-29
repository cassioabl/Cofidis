using Cofidis.Domain.Interfaces;

namespace Cofidis.Domain.ValueObjects;

public record UnemploymentRate : IRiskFactor
{
    public decimal Value { get; }
    
    public UnemploymentRate(decimal value)
    {
        if (value < 0) throw new ArgumentException("Invalid rate");
        Value = value;
    }

    public int EvaluateRisk()
    {
        return Value switch
        {
            > 0.1M => 2,
            > 0.05M => 1,
            _ => 0
        };
    }
}