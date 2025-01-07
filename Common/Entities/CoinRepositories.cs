using System.Text.Json.Serialization;

namespace Common.Entities
{
    public class CoinRepositories
    {
        [JsonPropertyName("github")]
        public List<string> GitHub { get; set; } = new();

        [JsonPropertyName("bitbucket")]
        public List<string> Bitbucket { get; set; } = new();
    }
}
