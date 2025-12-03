using System.Text.Json.Serialization;

namespace Common.Entities
{
    public class MarketData
    {
        [JsonPropertyName("current_price")]
        public Dictionary<string, float> CurrentPrice { get; set; } = null!;

        [JsonPropertyName("high_24h")]
        public Dictionary<string, float> High24h { get; set; } = null!;

        [JsonPropertyName("low_24h")]
        public Dictionary<string, float> Low24h { get; set; } = null!;

        [JsonPropertyName("ath")]
        public Dictionary<string, float> AthPrice { get; set; } = null!;
        [JsonPropertyName("atl")]
        public Dictionary<string, float> AtlPrice { get; set; } = null!;

        [JsonPropertyName("price_change_24h")]
        public float PriceChange24h { get; set; }

        [JsonPropertyName("price_change_percentage_24h")]
        public float PriceChangePercentage24h { get; set; }

        [JsonPropertyName("price_change_percentage_7d")]
        public float PriceChangePercentage7d { get; set; }

        [JsonPropertyName("price_change_percentage_14d")]
        public float PriceChangePercentage14d { get; set; }

        [JsonPropertyName("price_change_percentage_30d")]
        public float PriceChangePercentage30d { get; set; }

        [JsonPropertyName("price_change_percentage_60d")]
        public float PriceChangePercentage60d { get; set; }

        [JsonPropertyName("price_change_percentage_200d")]
        public float PriceChangePercentage200d { get; set; }
    }
}
