using BitcoinPriceForecastingTaining.Entities;
using Microsoft.ML;
using System.Globalization;
using System.Text;

namespace BitcoinPriceForecastingTaining.TrainingDataSavers
{
    internal class TrainingDataSaver
    {
        protected string BaseDirectory => Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Data");

        public TrainingDataSaver()
        {
            
        }

        public async Task SaveAsync(string trainerType, string modelId, CoinHistoricalData data)
        {
            var directoryPath = Path.Combine(BaseDirectory, trainerType);
            Directory.CreateDirectory(directoryPath);

            var fileName = $"{trainerType}_{modelId}";
            var filePath = Path.Combine(directoryPath, fileName);

            var csvLines = new List<string>();

            csvLines.Add("Price_Timestamp,Price,MarketCap_Timestamp,MarketCap,TotalVolume_Timestamp,TotalVolume");

            int maxRows = Math.Max(data.Prices.Count, Math.Max(data.MarketCaps.Count, data.TotalVolumes.Count));

            for (int i = 0; i < maxRows; i++)
            {
                var price = i < data.Prices.Count ? data.Prices[i] : new List<float> { float.NaN, float.NaN };
                var marketCap = i < data.MarketCaps.Count ? data.MarketCaps[i] : new List<float> { float.NaN, float.NaN };
                var totalVolume = i < data.TotalVolumes.Count ? data.TotalVolumes[i] : new List<float> { float.NaN, float.NaN };

                string line = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0},{1},{2},{3},{4},{5}",
                    price[0], price[1],
                    marketCap[0], marketCap[1],
                    totalVolume[0], totalVolume[1]
                );

                csvLines.Add(line);
            }

            await File.WriteAllLinesAsync(filePath, csvLines, Encoding.UTF8);
        }
    }
}
