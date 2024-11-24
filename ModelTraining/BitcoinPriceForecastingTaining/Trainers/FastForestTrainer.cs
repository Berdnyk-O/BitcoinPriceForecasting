using BitcoinPriceForecastingTaining.Entities;
using Microsoft.ML;

namespace BitcoinPriceForecastingTaining.Trainers
{
    internal class FastForestTrainer : BaseTrainer
    {
        public override string TrainerType => "FastForest";

        public FastForestTrainer(MLContext context) : base(context)
        {
        }
        

        public override string Train(IDataView trainDataView)
        {
            var modelId = DateTime.Now.ToString("dd.MM.yyyy_HH.mm.ss");
            _dataSplit = _context.Data.TrainTestSplit(trainDataView, 0.1);

            IEstimator<ITransformer> dataProcessPipeline =
                _context.Transforms.CopyColumns("Label", nameof(HistoricalDataRecord.Price))
                .Append(_context.Transforms.Concatenate("Features",
                nameof(HistoricalDataRecord.Date),
                nameof(HistoricalDataRecord.MarketCap),
                nameof(HistoricalDataRecord.TotalVolume)));

            var trainer = _context.Regression.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features");
            var trainingPipeLine = dataProcessPipeline.Append(trainer);

            _trainedModel = trainingPipeLine.Fit(_dataSplit.TrainSet);
            Save($"{TrainerType}_{modelId}.zip", _trainedModel, _dataSplit.TrainSet.Schema);

            return modelId;
        }
    }
}
