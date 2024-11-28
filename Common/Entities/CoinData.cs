using System.Text.Json.Serialization;

namespace Common.Entities
{
    public class CoinData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("market_data")]
        public MarketData MarketData { get; set; } = null!;
    }
}
