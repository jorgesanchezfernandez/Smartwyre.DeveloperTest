using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Factory;

public interface ICalculateRebateResultFactory
{
    CalculateRebateResult Create(bool success);
}

