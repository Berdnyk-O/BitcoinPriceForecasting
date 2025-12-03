using Common;
using Common.Entities;

namespace BitcoinPriceForecasting
{
    public class CryptoDataStore
    {
        private string _bitcoinId = "bitcoin";

        public CoinData CoinData { get; private set; }
        public CoinHistoricalData CoinHistoricalData { get; private set; }

        private readonly CryptoDataFetcher _dataFetcher;
        private bool _isInitialized;

        public CryptoDataStore(CryptoDataFetcher dataFetcher)
        {
            _dataFetcher = dataFetcher;
        }

        public async Task InitializeAsync()
        {
            if (_isInitialized) return;

            CoinData = await _dataFetcher.GetData(_bitcoinId);
            CoinHistoricalData = await _dataFetcher.GetHistoricalData(_bitcoinId);

            _isInitialized = true;
        }
    }
}
