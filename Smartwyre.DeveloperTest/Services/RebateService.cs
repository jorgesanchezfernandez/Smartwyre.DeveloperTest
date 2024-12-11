using Smartwyre.DeveloperTest.Facade;
using Smartwyre.DeveloperTest.Factory;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService(IRebateServiceFacade facade, IRebateCalculationFactory factory) : IRebateService
{
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = facade.GetRebate(request.RebateIdentifier);
        Product product = facade.GetProduct(request.ProductIdentifier);

        var strategy = factory.GetStrategy(rebate.Incentive);

        if (strategy != null && strategy.IsApplicable(rebate, product, request))
        {
            var rebateAmount = strategy.CalculateRebateAmount(rebate, product, request);
            facade.StoreCalculationResult(rebate, rebateAmount);
            return facade.CreateResult(true);
        }
        else
        {
            return facade.CreateResult(false);
        }
    }
}


