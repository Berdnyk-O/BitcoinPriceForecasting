using System.Text.Json.Serialization;

namespace BitcoinPriceForecastingTaining.Entities
{
    public class CoinHistoricalData
    {
        [JsonPropertyName("prices")]
        public List<List<double>> Prices { get; set; } = null!;

        [JsonPropertyName("market_caps")]
        public List<List<double>> MarketCaps { get; set; } = null!;

        [JsonPropertyName("total_volumes")]
        public List<List<double>> TotalVolumes { get; set; } = null!;
    }
}
