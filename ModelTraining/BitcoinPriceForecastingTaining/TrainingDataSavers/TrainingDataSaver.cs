using Common.Entities;
using System.Globalization;
using System.Text;

namespace BitcoinPriceForecastingTaining.TrainingDataSavers
{
    internal class TrainingDataSaver
    {
        protected string BaseDirectory => Path.Combine(_resourceFolderPath, "Data");

        private string _resourceFolderPath;

        public TrainingDataSaver(string resourceFolderPath)
        {
            _resourceFolderPath = resourceFolderPath;
        }

        public async Task SaveAsync(string trainerType, string modelId, CoinHistoricalData data)
        {
            var directoryPath = Path.Combine(BaseDirectory, trainerType);
            Directory.CreateDirectory(directoryPath);

            var fileName = $"{trainerType}_{modelId}";
            var filePath = Path.Combine(directoryPath, fileName);

            var csvLines = new List<string>();

            csvLines.Add("Timestamp,Price,MarketCap,TotalVolume");

            int maxRows = Math.Max(data.Prices.Count, Math.Max(data.MarketCaps.Count, data.TotalVolumes.Count));

            for (int i = 0; i < maxRows; i++)
            {
                var price = i < data.Prices.Count ? data.Prices[i] : new List<float> { float.NaN, float.NaN };
                var marketCap = i < data.MarketCaps.Count ? data.MarketCaps[i] : new List<float> { float.NaN, float.NaN };
                var totalVolume = i < data.TotalVolumes.Count ? data.TotalVolumes[i] : new List<float> { float.NaN, float.NaN };

                string line = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0},{1},{2},{3}",
                    price[0],
                    price[1],
                    marketCap[1],
                    totalVolume[1]
                );

                csvLines.Add(line);
            }

            await File.WriteAllLinesAsync(filePath, csvLines, Encoding.UTF8);
        }
    }
}
