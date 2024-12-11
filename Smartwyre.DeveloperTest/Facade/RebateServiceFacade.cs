using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Facade;

public class RebateServiceFacade : IRebateServiceFacade
{
    private readonly RebateDataStore _rebateDataStore;
    private readonly ProductDataStore _productDataStore;

    public RebateServiceFacade()
    {
        _rebateDataStore = new RebateDataStore();
        _productDataStore = new ProductDataStore();
    }

    public Rebate GetRebate(string rebateIdentifier)
    {
        return _rebateDataStore.GetRebate(rebateIdentifier);
    }

    public Product GetProduct(string productIdentifier)
    {
        return _productDataStore.GetProduct(productIdentifier);
    }

    public void StoreCalculationResult(Rebate rebate, decimal rebateAmount)
    {
        _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
    }

    public CalculateRebateResult CreateResult(bool success)
    {
        return new CalculateRebateResult { Success = success };
    }
}

