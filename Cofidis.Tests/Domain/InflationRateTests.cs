using Cofidis.Domain.ValueObjects;
using FluentAssertions;

namespace Cofidis.Tests.Domain
{
    [TestClass]
    public class InflationRateTests
    {

        [TestMethod]
        public void EvaluateRisk_ShouldReturn0_WhenRateIsLessThanOrEqualTo2Percent()
        {
            var rate = new InflationRate(0.01m);

            rate.EvaluateRisk().Should().Be(0);
        }

        [TestMethod]
        public void EvaluateRisk_ShouldReturn1_WhenRateIsBetween2And5Percent()
        {
            var rate = new InflationRate(0.04m);

            rate.EvaluateRisk().Should().Be(1);
        }

        [TestMethod]
        public void EvaluateRisk_ShouldReturn2_WhenRateIsAbove5Percent()
        {
            var rate = new InflationRate(0.7m);

            rate.EvaluateRisk().Should().Be(2);
        }
    }
}
