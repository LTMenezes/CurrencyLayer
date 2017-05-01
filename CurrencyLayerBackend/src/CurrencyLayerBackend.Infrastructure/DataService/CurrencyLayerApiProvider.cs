using CurrencyLayerBackend.Commons.DataModels;
using CurrencyLayerBackend.Commons.Settings;
using CurrencyLayerBackend.Infrastructure.HttpUtils;
using Newtonsoft.Json;
using System;
using System.Net.Http;


namespace CurrencyLayerBackend.Infrastructure.DataService
{
    public class CurrencyLayerApiProvider : ICurrencyLayerApiProvider
    {
        public static readonly HttpClient HttpClient = new HttpClient();
        private ICurrencyLayerConfig _currencyLayerConfig { get; set; }
        private ICurrencyLayerEndpoints _currencyLayerEndpoints { get; set; }

        public CurrencyLayerApiProvider(ICurrencyLayerEndpoints currencyLayerEndpoints, ICurrencyLayerConfig currencyLayerConfig)
        {
            this._currencyLayerConfig = currencyLayerConfig;
            this._currencyLayerEndpoints = currencyLayerEndpoints;
        }

        public HistoricalRateApiResult GetHistoricalRatesForGivenCurrency(string currency, DateTime wantedDateTime)
        {
            string accessKey = this._currencyLayerConfig.AccessKey;
            string dateAsString = wantedDateTime.ToString("yyyy-MM-dd");

            string requestUrl = string.Format(this._currencyLayerEndpoints.HistoricalRatesEndpoint, accessKey, dateAsString, currency);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(requestUrl),
                Method = HttpMethod.Get
            };

            var responseBody = HttpClient.SendAsync(request).Result.GetBody();

            return JsonConvert.DeserializeObject<HistoricalRateApiResult>(responseBody);
        }
    }
}
