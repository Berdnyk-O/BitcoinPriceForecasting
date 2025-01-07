using System.Text.Json.Serialization;

namespace Common.Entities
{
    public class CoinImage
    {
        [JsonPropertyName("thumb")]
        public string Thumb { get; set; } = null!;

        [JsonPropertyName("small")]
        public string Small { get; set; } = null!;

        [JsonPropertyName("large")]
        public string Large { get; set; } = null!;
    }
}
