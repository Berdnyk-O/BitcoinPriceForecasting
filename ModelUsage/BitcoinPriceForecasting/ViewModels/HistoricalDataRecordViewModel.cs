namespace BitcoinPriceForecasting.ViewModels
{
    public class HistoricalDataRecordViewModel
    {
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public float MarketCap { get; set; }
        public float TotalVolume { get; set; }
    }
}
