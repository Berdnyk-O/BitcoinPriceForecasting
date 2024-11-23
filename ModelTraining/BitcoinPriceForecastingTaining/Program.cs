using BitcoinPriceForecastingTaining;
using BitcoinPriceForecastingTaining.Trainers;
using BitcoinPriceForecastingTaining.TrainingDataSavers;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

var key = config["CoinGeckoApiSettings:ApiKey"];
if (string.IsNullOrEmpty(key))
{
    throw new ArgumentNullException(nameof(key), "The key cannot be null or empty.");
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
var trainer = new FastTreeTrainer(context);

var modelId = trainer.Train(converter.ConvertToIDataView(context, coinHistoricalData));
var saver = new TrainingDataSaver();
await saver.SaveAsync(trainer.TrainerType, modelId, coinHistoricalData);
trainer.Evaluate();

Console.WriteLine();