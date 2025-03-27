using Common.Entities;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace BitcoinPriceForecasting
{
    public class TimeSeriesForecastingService
    {
        private readonly TimeSeriesPredictionEngine<HistoricalDataRecord, HictoricalDataSsaPredictionResult> _timeSeriesEngine;

        public TimeSeriesForecastingService(string filePath)
        {
            var mlContext = new MLContext();
            using var stream = File.OpenRead(filePath);
            var trainedModel = mlContext.Model.Load(stream, out var modelSchema);
            
            _timeSeriesEngine = trainedModel
                .CreateTimeSeriesEngine<HistoricalDataRecord, HictoricalDataSsaPredictionResult>(mlContext);
        }

        public HictoricalDataSsaPredictionResult Predict()
        {
            return _timeSeriesEngine.Predict();
        }
    }
}
