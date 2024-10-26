using System.Text.Json.Serialization;

namespace BitcoinPriceForecastingTaining.Entities
{
    public class MarketData
    {
        [JsonPropertyName("current_price")]
        public Dictionary<string, double> CurrentPrice { get; set; } = null!;

        [JsonPropertyName("high_24h")]
        public Dictionary<string, double> High24h { get; set; } = null!;

        [JsonPropertyName("low_24h")]
        public Dictionary<string, double> Low24h { get; set; } = null!;

        [JsonPropertyName("ath")]
        public Dictionary<string, double> AthPrice { get; set; } = null!;
        [JsonPropertyName("atl")]
        public Dictionary<string, double> AtlPrice { get; set; } = null!;

        [JsonPropertyName("price_change_24h")]
        public double PriceChange24h { get; set; }

        [JsonPropertyName("price_change_percentage_24h")]
        public double PriceChangePercentage24h { get; set; }

        [JsonPropertyName("price_change_percentage_7d")]
        public double PriceChangePercentage7d { get; set; }

        [JsonPropertyName("price_change_percentage_14d")]
        public double PriceChangePercentage14d { get; set; }

        [JsonPropertyName("price_change_percentage_30d")]
        public double PriceChangePercentage30d { get; set; }

        [JsonPropertyName("price_change_percentage_60d")]
        public double PriceChangePercentage60d { get; set; }

        [JsonPropertyName("price_change_percentage_200d")]
        public double PriceChangePercentage200d { get; set; }
    }
}
