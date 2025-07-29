using Cofidis.Domain.Services;
using Cofidis.Domain.Exceptions;
using FluentAssertions;

namespace Cofidis.Tests.Domain
{
    [TestClass]
    public class InterestRateCalculatorTests
    {
        [TestMethod]
        public void Calculate_ShouldReturnBaseRate_WhenRiskIndexIs1()
        {
            var calculator = new InterestRateCalculator();

            var result = calculator.Calculate(1);

            result.Should().Be(0.05m);
        }

        [TestMethod]
        public void Calculate_ShouldIncrementRateByRiskPoints()
        {
            var calculator = new InterestRateCalculator();

            calculator.Calculate(2).Should().Be(0.07m);
        }

        [TestMethod]
        public void Calculate_ShouldThrowCreditDeniedException_WhenRiskIs5()
        {
            var calculator = new InterestRateCalculator();

            Action act = () => calculator.Calculate(5);

            act.Should().Throw<CreditDeniedException>();
        }

        [TestMethod]
        public void Calculate_ShouldThrowArgumentOutOfRange_WhenRiskIsInvalid()
        {
            var calculator = new InterestRateCalculator();

            Action below = () => calculator.Calculate(0);
            Action above = () => calculator.Calculate(6);

            below.Should().Throw<ArgumentOutOfRangeException>();
            above.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void Calculate_ShouldRespectCustomBaseRateAndIncrement()
        {
            var calculator = new InterestRateCalculator(baseRate: 0.1m, incrementPerRiskPoint: 0.01m);

            var result = calculator.Calculate(3); // 0.1 + (3 - 1) * 0.01 = 0.12

            result.Should().Be(0.12m);
        }
    }
}
