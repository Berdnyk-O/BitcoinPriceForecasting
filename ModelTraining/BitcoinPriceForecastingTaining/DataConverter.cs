using BitcoinPriceForecastingTaining.Entities;
using Microsoft.ML;

namespace BitcoinPriceForecastingTaining
{
    internal class DataConverter
    {
        public IDataView ConvertToIDataView(MLContext mlContext, CoinHistoricalData data)
        {
            var records = new List<HistoricalDataRecord>();

            int count = Math.Min(data.Prices.Count, Math.Min(data.MarketCaps.Count, data.TotalVolumes.Count));

            for (int i = 0; i < count; i++)
            {
                records.Add(new HistoricalDataRecord
                {
                    Date = data.Prices[i][0],
                    Price = data.Prices[i][1],
                    MarketCap = data.MarketCaps[i][1],
                    TotalVolume = data.TotalVolumes[i][1]
                });
            }

            return mlContext.Data.LoadFromEnumerable(records);
        }
    }
}
