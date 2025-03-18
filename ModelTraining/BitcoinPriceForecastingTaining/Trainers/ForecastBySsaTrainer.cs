using Common.Entities;
using Microsoft.ML;

namespace BitcoinPriceForecastingTaining.Trainers
{
    internal class ForecastBySsaTrainer : BaseTrainer
    {
        public override string TrainerType => "ForecastBySsa";

        public ForecastBySsaTrainer(MLContext context, string resourceFolderPath) 
            : base(context, resourceFolderPath)
        {
        }

        public override string Train(IDataView trainDataView)
        {
            var modelId = DateTime.Now.ToString("dd.MM.yyyy_HH.mm.ss");
            _dataSplit = _context.Data.TrainTestSplit(trainDataView, 0.1);

            IEstimator<ITransformer> forecastingPipeline =
                _context.Forecasting.ForecastBySsa(
                    outputColumnName: nameof(HictoricalDataSsaPredictionResult.Forecast),
                    inputColumnName: nameof(HistoricalDataRecord.Price),
                    windowSize: 7,
                    seriesLength: 31,
                    trainSize: 365,
                    horizon: 31,
                    confidenceLevel: 0.95f,
                    confidenceLowerBoundColumn: nameof(HictoricalDataSsaPredictionResult.LowerBoundPrices),
                    confidenceUpperBoundColumn: nameof(HictoricalDataSsaPredictionResult.UpperBoundPrices));

            _trainedModel = forecastingPipeline.Fit(_dataSplit.TrainSet);
            Save($"{TrainerType}_{modelId}.zip", _trainedModel, trainDataView.Schema);

            return modelId;
        }

        public override void Evaluate()
        {
            var testSetTransform = _trainedModel.Transform(_dataSplit.TestSet);

            IEnumerable<float> actual = _context.Data.CreateEnumerable<HistoricalDataRecord>(_dataSplit.TestSet, true)
                .Select(observed => observed.Price);

            IEnumerable<float> forecast = _context.Data.CreateEnumerable<HictoricalDataSsaPredictionResult>(testSetTransform, true)
                .Select(prediction => prediction.Forecast[0]);

            var metrics = actual.Zip(forecast, (actualValue, forecastValue) => actualValue - forecastValue);

            var MAE = metrics.Average(error => Math.Abs(error)); // Mean Absolute Error
            var RMSE = Math.Sqrt(metrics.Average(error => Math.Pow(error, 2))); // Root Mean Squared Error

            Console.WriteLine("Evaluation Metrics");
            Console.WriteLine("---------------------");
            Console.WriteLine($"Mean Absolute Error: {MAE:F3}");
            Console.WriteLine($"Root Mean Squared Error: {RMSE:F3}\n");
        }
    }
}
