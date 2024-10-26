using BitcoinPriceForecastingTaining;
using Microsoft.Extensions.Configuration;

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
var coinData = await cryptoDataFetcher.GetData("bitcoin");

Console.WriteLine();