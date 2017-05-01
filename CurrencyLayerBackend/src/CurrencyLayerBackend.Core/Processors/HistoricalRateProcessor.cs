using CurrencyLayerBackend.Commons.DataModels;
using CurrencyLayerBackend.Infrastructure.DataService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyLayerBackend.Core.Processors
{
    public class HistoricalRateProcessor : IHistoricalRateProcessor
    {
        private ICurrencyLayerApiProvider _currencyLayerApiProvider { get; set; }

        public HistoricalRateProcessor(ICurrencyLayerApiProvider currencyLayerApiProvider)
        {
            this._currencyLayerApiProvider = currencyLayerApiProvider;
        }

        public HistoricalRateResponse GetHistoricalRatesForGivenCurrency(string currency)
        {
            HistoricalRateResponse response = new HistoricalRateResponse();

            try
            {
                response = GetHistoricalRateResponsesForSevenDaysInThePast(currency);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }

            return response;
        }

        private HistoricalRateResponse GetHistoricalRateResponsesForSevenDaysInThePast(string currency)
        {
            int daysToLoop = 7;
            DateTime loopDateTime = DateTime.UtcNow;
            HistoricalRateResponse result = new HistoricalRateResponse();

            for (int i = 0; i < daysToLoop; i++)
            {
                HistoricalRateApiResult apiResult = this._currencyLayerApiProvider.GetHistoricalRatesForGivenCurrency(currency, loopDateTime);
                if (apiResult.Success == false) { throw new Exception("Currency Layer Api returned error!"); }

                var List = new List<double>();

                var unixTimestamp = ((DateTimeOffset)loopDateTime).ToUnixTimeMilliseconds();
                List.Add(unixTimestamp);
                List.Add((double)apiResult.Quotes.First().Value);

                result.Quotes.Add(List);

                loopDateTime = loopDateTime.AddDays(-1);
            }

            result.Success = true;
            result.Message = "Successful Operation";

            return result;
        }
    }
}
