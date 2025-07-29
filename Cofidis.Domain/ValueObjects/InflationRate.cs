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
            > 0.05M => 2,
            > 0.02M => 1,
            _ => 0
        };
    }
}