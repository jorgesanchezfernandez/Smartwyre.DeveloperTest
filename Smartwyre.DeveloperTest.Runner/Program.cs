using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Facade;
using Smartwyre.DeveloperTest.Factory;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Strategy;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        //Registering the dependency injection
        var serviceProvider = new ServiceCollection()
            .AddTransient<IRebateDataStore, RebateDataStore>()
            .AddTransient<IProductDataStore, ProductDataStore>()
            .AddTransient<IRebateServiceFacade, RebateServiceFacade>()
            .AddTransient<ICalculateRebateResultFactory, CalculateRebateResultFactory>()
            .AddSingleton<IRebateCalculationFactory, RebateCalculationFactory>()
            .AddTransient<IRebateService, RebateService>()
            .AddSingleton<IRebateCalculationStrategy, FixedCashAmountStrategy>()
            .AddSingleton<IRebateCalculationStrategy, FixedRateRebateStrategy>()
            .AddSingleton<IRebateCalculationStrategy, AmountPerUomStrategy>()
            .BuildServiceProvider();


        // Get the rebate service
        var rebateService = serviceProvider.GetService<IRebateService>();

        // Example
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "REBATE123",
            ProductIdentifier = "PRODUCT456",
            Volume = 10
        };

        var result = rebateService.Calculate(request);
        Console.WriteLine($"Calculation Success: {result.Success}");
    }
}
