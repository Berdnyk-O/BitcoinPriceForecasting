using Microsoft.ML.Data;
namespace BitcoinPriceForecastingTaining.Entities
{
    public class HistoricalDataRecord
    {
        [LoadColumn(0)]
        public float Price { get; set; }

        [LoadColumn(1)]
        public float MarketCap { get; set; }

        [LoadColumn(2)]
        public float TotalVolume { get; set; }
    }
}
