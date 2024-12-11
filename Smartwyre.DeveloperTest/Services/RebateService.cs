using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factory;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService(RebateCalculationFactory factory) : IRebateService
{
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();

        Rebate rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = productDataStore.GetProduct(request.ProductIdentifier);

        var strategy = factory.GetStrategy(rebate.Incentive);

        var result = new CalculateRebateResult();

        if (strategy != null && strategy.IsApplicable(rebate, product, request))
        {
            result.Success = true;
            decimal rebateAmount = strategy.CalculateRebateAmount(rebate, product, request);
            rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
        }
        else
        {
            result.Success = false;
        }

        return result;
    }
}
