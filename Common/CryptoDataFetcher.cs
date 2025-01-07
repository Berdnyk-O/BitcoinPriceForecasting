using Common.Entities;
using System.Text.Json;

namespace Common
{
    public class CryptoDataFetcher
    {
        private const string ApiUrl = "https://api.coingecko.com/api/v3";
        private const string CoinsHistoricalDataEndpoint = "/coins/{0}/market_chart?vs_currency=usd&days={1}";
        private const string CoinsDataEndpoint = "/coins/{0}";

        private readonly HttpClient _client;
        private readonly string _apiKey;

        public CryptoDataFetcher(HttpClient client, string apiKey)
        {
            _client = client;
            _apiKey = apiKey;
        }

        public async Task<CoinData?> GetData(string coinId)
        {
            var url = string.Format(ApiUrl + CoinsDataEndpoint, coinId);
            var res = await _client.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await res.Content.ReadAsStringAsync();
            var coindata = JsonSerializer.Deserialize<CoinData>(content);

            return coindata;
        }

        public async Task<CoinHistoricalData?> GetHistoricalData(string coinId, int days = 300)
        {
            var url = string.Format(ApiUrl + CoinsHistoricalDataEndpoint, coinId, 300);
            var res = await _client.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await res.Content.ReadAsStringAsync();
            var coindata = JsonSerializer.Deserialize<CoinHistoricalData>(content);

            return coindata;
        }
    }
}
