namespace Common.Entities
{
    public class HictoricalDataSsaPredictionResult
    {
        public float[] Forecast { get; set; }

        public float[] LowerBoundPrices { get; set; }

        public float[] UpperBoundPrices { get; set; }
    }
}
