using System.Text.Json.Serialization;

namespace BitcoinPriceForecastingTaining.Entities
{
    public class CoinHistoricalData
    {
        [JsonPropertyName("prices")]
        public List<List<float>> Prices { get; set; } = null!;

        [JsonPropertyName("market_caps")]
        public List<List<float>> MarketCaps { get; set; } = null!;

        [JsonPropertyName("total_volumes")]
        public List<List<float>> TotalVolumes { get; set; } = null!;
    }
}
