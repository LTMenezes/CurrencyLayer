using CurrencyLayerBackend.Commons.DataModels;
using CurrencyLayerBackend.Core.Processors;
using CurrencyLayerBackend.Infrastructure.HttpUtils;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Http;

namespace CurrencyLayerBackend.Core.Controllers
{
    [RoutePrefix("api")]
    public class CurrencyRateController : ApiController
    {
        private IHistoricalRateProcessor _historicalRateProcessor { get; set; }

        public CurrencyRateController(IHistoricalRateProcessor historicalRateProcessor)
        {
            this._historicalRateProcessor = historicalRateProcessor;
        }

        [HttpGet]
        [Route("historicalRate/{currency}")]
        public HttpResponseMessage GetHistoricalRateEndpoint([FromUri] string currency)
        {
            HistoricalRateResponse result = this._historicalRateProcessor.GetHistoricalRatesForGivenCurrency(currency);

            string serializedResult = JsonConvert.SerializeObject(result);
            HttpResponseMessage response = RequestUtils.CreateHttpResponse(serializedResult);

            return response;
        }
    }
}
