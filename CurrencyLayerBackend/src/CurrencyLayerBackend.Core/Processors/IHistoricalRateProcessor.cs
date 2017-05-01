using CurrencyLayerBackend.Commons.DataModels;

namespace CurrencyLayerBackend.Core.Processors
{
    public interface IHistoricalRateProcessor
    {
        HistoricalRateResponse GetHistoricalRatesForGivenCurrency(string currency);
    }
}
