using System.Collections.Generic;

namespace CurrencyLayerBackend.Commons.DataModels
{
    public class HistoricalRateResponse
    {
        public HistoricalRateResponse()
        {
            this.Quotes = new List<List<double>>();
        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public List<List<double>> Quotes { get; set; }    
    }
}
