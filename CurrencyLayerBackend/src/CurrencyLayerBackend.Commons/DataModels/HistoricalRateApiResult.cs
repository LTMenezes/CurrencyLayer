using System;
using System.Collections.Generic;

namespace CurrencyLayerBackend.Commons.DataModels
{
    public class HistoricalRateApiResult
    {
        public bool Success { get; set; }

        public Dictionary<string, decimal> Quotes { get; set; }
    }
}
