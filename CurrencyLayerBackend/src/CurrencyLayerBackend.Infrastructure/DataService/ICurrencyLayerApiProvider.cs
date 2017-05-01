using CurrencyLayerBackend.Commons.DataModels;
using System;

namespace CurrencyLayerBackend.Infrastructure.DataService
{
    public interface ICurrencyLayerApiProvider
    {
        HistoricalRateApiResult GetHistoricalRatesForGivenCurrency(string currency, DateTime inputDateTime);
    }
}
