using CurrencyLayerBackend.Commons.DataModels;
using CurrencyLayerBackend.Core.Controllers;
using CurrencyLayerBackend.Core.Processors;
using Moq;
using Xunit;

namespace CurrencyLayerBackend.Core.UnitTest
{
    public class CurrencyRateControllerTest
    {
        [Fact]
        public void GetHistoricalRateEndpointShouldCallProcessorOnlyOnce()
        {
            //ARRANGE
            var historicalRateProcessorMock = new Mock<IHistoricalRateProcessor>(MockBehavior.Strict);
            historicalRateProcessorMock.Setup(x => x.GetHistoricalRatesForGivenCurrency(It.IsAny<string>())).Returns(new HistoricalRateResponse());
            string currency = "USD";

            //ACT
            var controller = new CurrencyRateController(historicalRateProcessorMock.Object);
            controller.GetHistoricalRateEndpoint(currency);

            //ASSERT
            historicalRateProcessorMock.Verify(x => x.GetHistoricalRatesForGivenCurrency(currency), Times.Once);
        }

        [Fact]
        public void GetHistoricalRateEndpointShouldReturnNonNullResponse()
        {
            //ARRANGE
            var historicalRateProcessorMock = new Mock<IHistoricalRateProcessor>(MockBehavior.Strict);
            historicalRateProcessorMock.Setup(x => x.GetHistoricalRatesForGivenCurrency(It.IsAny<string>())).Returns(new HistoricalRateResponse());
            string currency = "USD";

            //ACT
            var controller = new CurrencyRateController(historicalRateProcessorMock.Object);
            var result = controller.GetHistoricalRateEndpoint(currency);

            //ASSERT
            Assert.NotNull(result);
        }
    }
}
