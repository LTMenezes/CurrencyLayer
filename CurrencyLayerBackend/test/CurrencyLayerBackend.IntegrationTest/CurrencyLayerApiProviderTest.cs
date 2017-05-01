using CurrencyLayerBackend.Application.Setup;
using CurrencyLayerBackend.Commons.Settings;
using CurrencyLayerBackend.Infrastructure.DataService;
using System;
using Xunit;

namespace CurrencyLayerBackend.Core.UnitTest
{
    public class CurrencyLayerApiProviderTest
    {
        [Fact]
        public void CurrencyLayerApiProviderShouldReturnSuccessOnHistoricalRateEndpoint()
        {
            //ARRANGE
            var currencyLayerEndpoints = DependencyInjector.ApplicationContainer().GetInstance<ICurrencyLayerEndpoints>();
            var currencyLayerConfig= DependencyInjector.ApplicationContainer().GetInstance<ICurrencyLayerConfig>();

            string currency = "USD";
            DateTime inputDateTime = DateTime.UtcNow;

            //ACT
            var controller = new CurrencyLayerApiProvider(currencyLayerEndpoints, currencyLayerConfig);
            var result = controller.GetHistoricalRatesForGivenCurrency(currency, inputDateTime);

            //ASSERT
            Assert.Equal(true, result.Success);
        }

        [Fact]
        public void CurrencyLayerApiProviderShouldReturnSuccessOnHistoricalRateEndpointWithQuotes()
        {
            //ARRANGE
            var currencyLayerEndpoints = DependencyInjector.ApplicationContainer().GetInstance<ICurrencyLayerEndpoints>();
            var currencyLayerConfig = DependencyInjector.ApplicationContainer().GetInstance<ICurrencyLayerConfig>();

            string currency = "USD";
            DateTime inputDateTime = DateTime.UtcNow;

            //ACT
            var controller = new CurrencyLayerApiProvider(currencyLayerEndpoints, currencyLayerConfig);
            var result = controller.GetHistoricalRatesForGivenCurrency(currency, inputDateTime);

            //ASSERT
            Assert.NotNull(result.Quotes);
            Assert.NotEmpty(result.Quotes);
        }
    }
}
