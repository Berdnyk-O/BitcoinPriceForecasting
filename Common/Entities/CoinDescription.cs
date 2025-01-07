using System.Text.Json.Serialization;

namespace Common.Entities
{
    public class CoinDescription
    {
        [JsonPropertyName("en")]
        public string English { get; set; } = null!;

        [JsonPropertyName("ua")]
        public string Ukrainian { get; set; } = null!;
    }
}
