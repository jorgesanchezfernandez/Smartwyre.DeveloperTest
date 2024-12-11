using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Strategy;

public interface IRebateCalculationStrategy
{
    bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request);
    decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request);
}

