using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Strategy;

public class AmountPerUomStrategy : IRebateCalculationStrategy
{
    public bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate != null
               && product != null
               && product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom)
               && rebate.Amount > 0
               && request.Volume > 0;
    }

    public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Amount * request.Volume;
    }
}

