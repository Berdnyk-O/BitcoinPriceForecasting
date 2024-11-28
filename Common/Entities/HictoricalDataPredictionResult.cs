using Microsoft.ML.Data;

namespace Common.Entities
{
    public class HictoricalDataPredictionResult
    {
        [ColumnName("Score")]
        public float PredictedValue { get; set; }
    }
}
