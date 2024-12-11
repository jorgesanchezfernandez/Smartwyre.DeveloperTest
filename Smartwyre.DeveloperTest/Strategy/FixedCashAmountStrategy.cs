using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Strategy;

public class FixedCashAmountStrategy : IRebateCalculationStrategy
{
    public bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate != null
               && product != null
               && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount)
               && rebate.Amount > 0;
    }

    public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Amount;
    }
}

