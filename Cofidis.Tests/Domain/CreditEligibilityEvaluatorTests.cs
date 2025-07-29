using Cofidis.Domain.Entities;
using Cofidis.Domain.Exceptions;
using Cofidis.Domain.Services;
using Cofidis.Domain.ValueObjects;
using FluentAssertions;

namespace Cofidis.Tests.Domain
{

    [TestClass]
    public class CreditEligibilityEvaluatorTests
    {
        private readonly CreditEligibilityEvaluator _service = new();

        [TestMethod]
        public void EvaluateCustomerHistory_ShouldReturnOverdue_WhenAnyEntryIsOverdue()
        {
            var customer = new Customer
            {
                CreditHistory = new List<CustomerCredit>
                {
                    new() { TotalOverdue = 100 }
                }
            };

            var result = _service.EvaluateCustomerHistory(customer);

            result.Should().Be(CreditHistoryStatus.Overdue);
        }

        [TestMethod]
        public void EvaluateCustomerHistory_ShouldReturnLate30Days_WhenOnlyLateEntriesExist()
        {
            var customer = new Customer
            {
                CreditHistory = new List<CustomerCredit>
                {
                    new() { TotalOverdue = 0, TotalLate30Days = 200 }
                }
            };

            var result = _service.EvaluateCustomerHistory(customer);

            result.Should().Be(CreditHistoryStatus.Late30Days);
        }

        [TestMethod]
        public void EvaluateCustomerHistory_ShouldReturnOverdue_WhenIsLateAndOverdue()
        {
            var customer = new Customer
            {
                CreditHistory = new List<CustomerCredit>
                {
                    new() { TotalOverdue = 0, TotalLate30Days = 200 }
                }
            };

            var result = _service.EvaluateCustomerHistory(customer);

            result.Should().Be(CreditHistoryStatus.Late30Days);
        }

        [TestMethod]
        public void EvaluateCustomerHistory_ShouldReturnOnTime_WhenNoLateOrOverdueEntries()
        {
            var customer = new Customer
            {
                CreditHistory = new List<CustomerCredit>
                {
                    new() { TotalOverdue = 0, TotalLate30Days = 0 }
                }
            };

            var result = _service.EvaluateCustomerHistory(customer);

            result.Should().Be(CreditHistoryStatus.OnTime);
        }

        [TestMethod]
        public void EvaluateRequestedAmount_ShouldThrow_WhenAmountExceedsLimit()
        {
            var limit = new CreditLimit(1200, 2000);

            Action act = () => _service.EvaluateRequestedAmount(2500, limit);

            act.Should().Throw<CreditLimitExceededException>();
        }

        [TestMethod]
        public void EvaluateRequestedAmount_ShouldNotThrow_WhenAmountIsWithinLimit()
        {
            var limit = new CreditLimit(1500, 2000);

            Action act = () => _service.EvaluateRequestedAmount(1500, limit);

            act.Should().NotThrow();
        }

        [TestMethod]
        public void EvaluateCustomerEffort_ShouldThrow_WhenEffortExceeds30Percent()
        {
            var customer = new Customer
            {
                MonthlyIncome = 1000,
                MonthlyDebt = 250
            };

            var monthlyPayment = 100;

            Action act = () => _service.EvaluateCustomerEffort(customer, monthlyPayment);

            act.Should().Throw<ExcessiveEffortException>();
        }

        [TestMethod]
        public void EvaluateCustomerEffort_ShouldNotThrow_WhenEffortIsWithinLimit()
        {
            var customer = new Customer
            {
                MonthlyIncome = 2000,
                MonthlyDebt = 200
            };

            var monthlyPayment = 200;

            Action act = () => _service.EvaluateCustomerEffort(customer, monthlyPayment);

            act.Should().NotThrow();
        }
    }
}
