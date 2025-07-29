using Cofidis.Domain.ValueObjects;
using Cofidis.Infra.Repositories;

namespace Cofidis.Application.Services;

public class CreditLimitService(CreditLimitRepository creditLimitRepository)
{
    public async Task<CreditLimit> GetLimit(decimal monthlyIncome)
    {
        return await creditLimitRepository.GetCreditLimit(monthlyIncome);
    }
}