using BitcoinPriceForecastingTaining;
using BitcoinPriceForecastingTaining.Trainers;
using BitcoinPriceForecastingTaining.TrainingDataSavers;
using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

var key = config["CoinGeckoApiSettings:ApiKey"];
var resourceFolderPath = config["ResourceFolderPath"];
if (string.IsNullOrEmpty(key))
{
    throw new ArgumentNullException(nameof(key), "The key cannot be null or empty.");
}
if (string.IsNullOrEmpty(resourceFolderPath))
{
    throw new ArgumentException(nameof(resourceFolderPath), "The path to the resource file cannot be empty.");
}

var cryptoDataFetcher = new CryptoDataFetcher(new HttpClient(), key);
var coinHistoricalData = await cryptoDataFetcher.GetHistoricalData("bitcoin");
//var coinData = await cryptoDataFetcher.GetData("bitcoin");
if (coinHistoricalData == null)
{
    throw new ArgumentNullException(nameof(coinHistoricalData), "Error when trying to get data for training.");
}

var converter = new DataConverter();
var context = new MLContext();
var trainer = new ForecastBySsaTrainer(context, resourceFolderPath);

var modelId = trainer.Train(converter.ConvertToIDataView(context, coinHistoricalData));
var saver = new TrainingDataSaver(resourceFolderPath);
await saver.SaveAsync(trainer.TrainerType, modelId, coinHistoricalData);
trainer.Evaluate();

Console.WriteLine($"\ntrainer: {trainer.GetType().Name}\nmodelId: {modelId}");