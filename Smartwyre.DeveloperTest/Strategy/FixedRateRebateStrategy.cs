﻿using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Strategy;

public class FixedRateRebateStrategy : IRebateCalculationStrategy
{
    public bool IsApplicable(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate != null
               && product != null
               && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate)
               && rebate.Percentage > 0
               && product.Price > 0
               && request.Volume > 0;
    }

    public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return product.Price * rebate.Percentage * request.Volume;
    }
}

