using System.Text.Json.Serialization;

namespace Common.Entities
{
    public class CoinLinks
    {
        [JsonPropertyName("homepage")]
        public string[] HomePage { get; set; } = null!;

        [JsonPropertyName("whitepaper")]
        public string Whitepaper { get; set; } = null!;

        [JsonPropertyName("repos_url")]
        public CoinRepositories Repositories { get; set; } = null!;
    }
}
