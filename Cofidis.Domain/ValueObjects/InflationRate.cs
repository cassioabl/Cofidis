using Cofidis.Domain.Interfaces;

namespace Cofidis.Domain.ValueObjects;

public record InflationRate : IRiskFactor
{
    public decimal Value { get; }
    
    public InflationRate(decimal value)
    {
        if (value < 0) throw new ArgumentException("Invalid rate");
        Value = value;
    }
    
    public int EvaluateRisk()
    {
        return Value switch
        {
            > 5 => 2,
            > 2 => 1,
            _ => 0
        };
    }
}