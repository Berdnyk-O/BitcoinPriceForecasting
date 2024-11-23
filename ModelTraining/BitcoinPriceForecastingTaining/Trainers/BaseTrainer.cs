using BitcoinPriceForecastingTaining.Entities;
using BitcoinPriceForecastingTaining.TrainingDataSavers;
using Microsoft.ML;
using System.Reflection;
using static Microsoft.ML.DataOperationsCatalog;

namespace BitcoinPriceForecastingTaining.Trainers
{
    internal abstract class BaseTrainer
    {
        protected string BaseDirectory => Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Models");
        public abstract string DirectoryPath { get; }

        protected MLContext _context = null!;

        protected ITransformer _trainedModel = null!;
        protected TrainTestData _dataSplit;

        protected BaseTrainer(MLContext context)
        {
            _context = context;
        }

        public abstract string Train(IDataView trainDataView);

        public void Save(string modelName, ITransformer trainedModel, DataViewSchema schema)
        {
            Directory.CreateDirectory(DirectoryPath);

            var modelPath = Path.Combine(DirectoryPath, modelName);

            _context.Model.Save(_trainedModel, _dataSplit.TrainSet.Schema, modelPath);

            
        }

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
