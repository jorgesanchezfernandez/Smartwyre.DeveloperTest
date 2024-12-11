using Smartwyre.DeveloperTest.Strategy;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Factory;

public class RebateCalculationFactory(IEnumerable<IRebateCalculationStrategy> strategies)
{
    public IRebateCalculationStrategy GetStrategy(IncentiveType incentiveType)
    {
        return strategies.FirstOrDefault(strategy =>
            strategy.GetType().Name.Contains(incentiveType.ToString(), StringComparison.OrdinalIgnoreCase));
    }
}
