using CurrencyLayerBackend.Commons.DataModels;
using CurrencyLayerBackend.Core.Processors;
using CurrencyLayerBackend.Infrastructure.DataService;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CurrencyLayerBackend.Core.UnitTest
{
    public class HistoricalRateProcessorTest
    {
        [Fact]
        public void GetHistoricalRateEndpointShouldReturnSuccessFalseOnException()
        {
            //ARRANGE
            var currencyLayerApiProviderMock = new Mock<ICurrencyLayerApiProvider>(MockBehavior.Strict);
            currencyLayerApiProviderMock.Setup(x => x.GetHistoricalRatesForGivenCurrency(It.IsAny<string>(), It.IsAny<DateTime>())).Throws(new Exception());
            string currency = "USD";

            //ACT
            var controller = new HistoricalRateProcessor(currencyLayerApiProviderMock.Object);
            var result = controller.GetHistoricalRatesForGivenCurrency(currency);

            //ASSERT
            Assert.Equal(false, result.Success);
        }

        [Fact]
        public void GetHistoricalRateEndpointShouldReturnSuccessFalseOnEmptyQuotes()
        {
            //ARRANGE
            var currencyLayerApiProviderMock = new Mock<ICurrencyLayerApiProvider>(MockBehavior.Strict);
            var apiResponseMock = new HistoricalRateApiResult();
            apiResponseMock.Success = true;
            apiResponseMock.Quotes = new Dictionary<string, decimal>();
            currencyLayerApiProviderMock.Setup(x => x.GetHistoricalRatesForGivenCurrency(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(apiResponseMock);
            string currency = "USD";

            //ACT
            var controller = new HistoricalRateProcessor(currencyLayerApiProviderMock.Object);
            var result = controller.GetHistoricalRatesForGivenCurrency(currency);

            //ASSERT
            Assert.Equal(false, result.Success);
        }

        [Fact]
        public void GetHistoricalRateEndpointShouldReturnSuccessFalseOnNullQuotes()
        {
            //ARRANGE
            var currencyLayerApiProviderMock = new Mock<ICurrencyLayerApiProvider>(MockBehavior.Strict);
            var apiResponseMock = new HistoricalRateApiResult();
            apiResponseMock.Success = true;
            currencyLayerApiProviderMock.Setup(x => x.GetHistoricalRatesForGivenCurrency(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(apiResponseMock);
            string currency = "USD";

            //ACT
            var controller = new HistoricalRateProcessor(currencyLayerApiProviderMock.Object);
            var result = controller.GetHistoricalRatesForGivenCurrency(currency);

            //ASSERT
            Assert.Equal(false, result.Success);
        }

        [Fact]
        public void GetHistoricalRateEndpointShouldReturnSuccessFalseOnApiResultFalse()
        {
            //ARRANGE
            var currencyLayerApiProviderMock = new Mock<ICurrencyLayerApiProvider>(MockBehavior.Strict);
            var apiResponseMock = new HistoricalRateApiResult();
            apiResponseMock.Success = false;
            apiResponseMock.Quotes = new Dictionary<string, decimal>();
            apiResponseMock.Quotes.Add("teste", 9.0m);
            currencyLayerApiProviderMock.Setup(x => x.GetHistoricalRatesForGivenCurrency(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(apiResponseMock);
            string currency = "USD";

            //ACT
            var controller = new HistoricalRateProcessor(currencyLayerApiProviderMock.Object);
            var result = controller.GetHistoricalRatesForGivenCurrency(currency);

            //ASSERT
            Assert.Equal(false, result.Success);
        }

        [Fact]
        public void GetHistoricalRateEndpointShouldNotReturnSuccessMessageOnFailure()
        {
            //ARRANGE
            var currencyLayerApiProviderMock = new Mock<ICurrencyLayerApiProvider>(MockBehavior.Strict);
            var apiResponseMock = new HistoricalRateApiResult();
            apiResponseMock.Success = false;
            apiResponseMock.Quotes = new Dictionary<string, decimal>();
            apiResponseMock.Quotes.Add("teste", 9.0m);
            currencyLayerApiProviderMock.Setup(x => x.GetHistoricalRatesForGivenCurrency(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(apiResponseMock);
            string currency = "USD";

            //ACT
            var controller = new HistoricalRateProcessor(currencyLayerApiProviderMock.Object);
            var result = controller.GetHistoricalRatesForGivenCurrency(currency);

            //ASSERT
            Assert.NotEqual("Successful Operation", result.Message);
        }

        [Fact]
        public void GetHistoricalRateEndpointShouldReturnSuccessTrueOnSuccess()
        {
            //ARRANGE
            var currencyLayerApiProviderMock = new Mock<ICurrencyLayerApiProvider>(MockBehavior.Strict);
            var apiResponseMock = new HistoricalRateApiResult();
            apiResponseMock.Success = true;
            apiResponseMock.Quotes = new Dictionary<string, decimal>();
            apiResponseMock.Quotes.Add("teste", 9.0m);
            currencyLayerApiProviderMock.Setup(x => x.GetHistoricalRatesForGivenCurrency(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(apiResponseMock);
            string currency = "USD";

            //ACT
            var controller = new HistoricalRateProcessor(currencyLayerApiProviderMock.Object);
            var result = controller.GetHistoricalRatesForGivenCurrency(currency);

            //ASSERT
            Assert.Equal(true, result.Success);
        }

        [Fact]
        public void GetHistoricalRateEndpointShouldReturnAllSevenQuotesOnSuccess()
        {
            //ARRANGE
            var currencyLayerApiProviderMock = new Mock<ICurrencyLayerApiProvider>(MockBehavior.Strict);
            var apiResponseMock = new HistoricalRateApiResult();
            apiResponseMock.Success = true;
            apiResponseMock.Quotes = new Dictionary<string, decimal>();
            apiResponseMock.Quotes.Add("teste", 9.0m);
            currencyLayerApiProviderMock.Setup(x => x.GetHistoricalRatesForGivenCurrency(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(apiResponseMock);
            string currency = "USD";

            //ACT
            var controller = new HistoricalRateProcessor(currencyLayerApiProviderMock.Object);
            var result = controller.GetHistoricalRatesForGivenCurrency(currency);

            //ASSERT
            Assert.Equal(7, result.Quotes.Count);
        }

        [Fact]
        public void GetHistoricalRateEndpointShouldReturnAllSevenQuotesWithCorrectValuesOnSuccess()
        {
            //ARRANGE
            var currencyLayerApiProviderMock = new Mock<ICurrencyLayerApiProvider>(MockBehavior.Strict);
            var apiResponseMock = new HistoricalRateApiResult();
            apiResponseMock.Success = true;
            apiResponseMock.Quotes = new Dictionary<string, decimal>();
            apiResponseMock.Quotes.Add("teste", 9.0m);
            currencyLayerApiProviderMock.Setup(x => x.GetHistoricalRatesForGivenCurrency(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(apiResponseMock);
            string currency = "USD";

            //ACT
            var controller = new HistoricalRateProcessor(currencyLayerApiProviderMock.Object);
            var result = controller.GetHistoricalRatesForGivenCurrency(currency);

            //ASSERT
            foreach(List<double> innerResult in result.Quotes)
            {
                Assert.Equal(2, innerResult.Count);
                Assert.True(innerResult.Contains(9.0));
            }            
        }
    }
}
