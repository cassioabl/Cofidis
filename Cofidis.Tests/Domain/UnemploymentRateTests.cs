using Cofidis.Domain.ValueObjects;
using FluentAssertions;

namespace Cofidis.Tests.Domain
{
    [TestClass]
    public class UnemploymentRateTests
    {

        [TestMethod]
        public void EvaluateRisk_ShouldReturn0_WhenRateIsLessThanOrEqualTo5Percent()
        {
            var rate = new UnemploymentRate(0.04m);

            rate.EvaluateRisk().Should().Be(0);
        }

        [TestMethod]
        public void EvaluateRisk_ShouldReturn1_WhenRateIsBetween5And10Percent()
        {
            var rate = new UnemploymentRate(0.08m);

            rate.EvaluateRisk().Should().Be(1);
        }

        [TestMethod]
        public void EvaluateRisk_ShouldReturn2_WhenRateIsAbove10Percent()
        {
            var rate = new UnemploymentRate(0.12m);

            rate.EvaluateRisk().Should().Be(2);
        }
    }
}
