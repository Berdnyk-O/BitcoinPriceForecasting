using BitcoinPriceForecastingTaining.Entities;
using Microsoft.ML;

namespace BitcoinPriceForecastingTaining.Trainers
{
    internal class FastTreeTrainer : BaseTrainer
    {
        public override string DirectoryPath => Path.Combine(BaseDirectory, "FastTree");

        public FastTreeTrainer(MLContext context) : base(context)
        {
        }

        public override void Train(IDataView trainDataView)
        {
            _dataSplit = _context.Data.TrainTestSplit(trainDataView, 0.1);

            IEstimator<ITransformer> dataProcessPipeline =
                _context.Transforms.CopyColumns("Label", nameof(HistoricalDataRecord.Price))
                .Append(_context.Transforms.Concatenate("Features",
                nameof(HistoricalDataRecord.MarketCap), 
                nameof(HistoricalDataRecord.TotalVolume)));

            var trainer = _context.Regression.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features");
            var trainingPipeLine = dataProcessPipeline.Append(trainer);

            _trainedModel = trainingPipeLine.Fit(_dataSplit.TrainSet);
            Save($"FastTree_{DateTime.Now.ToString("dd.MM.yyyy_HH.mm.ss")}.zip", _trainedModel, _dataSplit.TrainSet.Schema);
        }
    }
}
