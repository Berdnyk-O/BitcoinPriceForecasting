using Microsoft.ML;
using static Microsoft.ML.DataOperationsCatalog;

namespace BitcoinPriceForecastingTaining.Trainers
{
    internal abstract class BaseTrainer
    {
        protected MLContext _context = null!;

        protected ITransformer _trainedModel = null!;
        protected TrainTestData _dataSplit;

        protected BaseTrainer(MLContext context)
        {
            _context = context;
        }

        public abstract void Train(IDataView trainDataView);

        public virtual void Evaluate()
        {
            var testSetTransform = _trainedModel.Transform(_dataSplit.TestSet);

            var modelMetrics = _context.Regression.Evaluate(testSetTransform, labelColumnName: "Label", scoreColumnName: "Score");

            Console.WriteLine($"Loss Function: {modelMetrics.LossFunction}\n" +
                $"Mean Absolute Error: {modelMetrics.MeanAbsoluteError}\n" +
                $"Mean Squared Error: {modelMetrics.MeanSquaredError}\n" +
                $"Rsquared: {modelMetrics.RSquared}\n" +
                $"Root Mean Squared Error: {modelMetrics.RootMeanSquaredError}");

            Console.ReadLine();
        }
    }
}
