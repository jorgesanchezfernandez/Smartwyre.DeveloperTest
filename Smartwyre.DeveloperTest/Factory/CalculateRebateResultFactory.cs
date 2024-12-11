using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Factory;

public class CalculateRebateResultFactory : ICalculateRebateResultFactory
{
    public CalculateRebateResult Create(bool success)
    {
        return new CalculateRebateResult { Success = success };
    }
}

