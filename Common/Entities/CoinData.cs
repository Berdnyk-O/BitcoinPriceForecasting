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

        [JsonPropertyName("genesis_date")]
        public string GenesisDate { get; set; } = null!;

        [JsonPropertyName("market_data")]
        public MarketData MarketData { get; set; } = null!;

        [JsonPropertyName("image")]
        public CoinImage Image { get; set; } = null!;

        [JsonPropertyName("links")]
        public CoinLinks Links { get; set; } = null!;

        [JsonPropertyName("hashing_algorithm")]
        public string HashingAlgorithm { get; set; } = null!;

        [JsonPropertyName("description")]
        public CoinDescription Description { get; set; } = null!;
    }
}
