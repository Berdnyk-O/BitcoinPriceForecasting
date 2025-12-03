namespace BitcoinPriceForecasting.ViewModels
{
    public class HistoricalDataViewModel
    {
        public string[] Dates { get; set; }
        public float[] Prices { get; set; }
        public float[] MarketCaps { get; set; }
        public float[] TotalVolumes { get; set; }
    }
}
