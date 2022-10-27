using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Modules.Transactions.Core.Repositories;
using Serilog;

namespace SaveMeter.Modules.Transactions.Core.MachineLearning
{
    class BankTransactionMlContext : IBankTransactionMlContext
    {
        class CategoryPrediction
        {
            [ColumnName("PredictedLabel")]
            public string Category;
        }

        public class BankTransactionForNetwork
        {
            public string CategoryId { get; set; }
            public string Customer { get; set; }
            public string Description { get; set; }
        }

        private MLContext _mlContext;
        private ITransformer _trainedModel;
        private PredictionEngine<BankTransactionForNetwork, CategoryPrediction> _predEngine;
        private List<BankTransactionForNetwork> _trainingData;

        private readonly ILogger _logger;
        private string _modelPath => "mlModel.zip";

        public BankTransactionMlContext(ILogger logger)
        {
            _logger = logger;
        }

        public void TrainModel(IEnumerable<BankTransaction> bankTransactions)
        {
            var _trainingData = bankTransactions.Select(x => new BankTransactionForNetwork()
            {
                Description = x.Description,
                Customer = x.Customer,
                CategoryId = x.CategoryId.ToString() ?? string.Empty,
            }).ToList();

            _mlContext = new MLContext();

            var trainingDataView = _mlContext.Data.LoadFromEnumerable(_trainingData);

            var pipeline = ProcessData();

            BuildAndTrainModel(trainingDataView, pipeline);

            EvaluateAndSave(trainingDataView.Schema);
        }

        public string Predicate(string customer, string description)
        {
            _mlContext = new MLContext(seed: 0);
            var loadedModel = _mlContext.Model.Load(_modelPath, out var modelInputSchema);
            var bankTransactionForNetwork = new BankTransactionForNetwork()
            {
                Customer = customer,
                Description = description
            };

            _predEngine = _mlContext.Model.CreatePredictionEngine<BankTransactionForNetwork, CategoryPrediction>(loadedModel);

            return _predEngine.Predict(bankTransactionForNetwork).Category;
        }

        IEstimator<ITransformer> ProcessData()
        {
            var pipeline = _mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"Customer", outputColumnName: @"Customer")
                                    .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"Description", outputColumnName: @"Description"))
                                    .Append(_mlContext.Transforms.Concatenate(@"Features", new[] { @"Customer", @"Description" }))
                                    .Append(_mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: @"CategoryId", inputColumnName: @"CategoryId"))
                                    .Append(_mlContext.Transforms.NormalizeMinMax(@"Features", @"Features"))
                                    .Append(_mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator: _mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression(new LbfgsLogisticRegressionBinaryTrainer.Options() { L1Regularization = 0.03125F, L2Regularization = 1.093764F, LabelColumnName = @"CategoryId", FeatureColumnName = @"Features" }), labelColumnName: @"CategoryId"))
                                    .Append(_mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: @"PredictedLabel", inputColumnName: @"PredictedLabel"));

            return pipeline;
        }

        void BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
        {
            _trainedModel = pipeline.Fit(trainingDataView);
            _predEngine = _mlContext.Model.CreatePredictionEngine<BankTransactionForNetwork, CategoryPrediction>(_trainedModel);
        }

        void EvaluateAndSave(DataViewSchema trainingDataViewSchema)
        {
            SaveModelAsFile(_mlContext, trainingDataViewSchema, _trainedModel);
        }

        private void SaveModelAsFile(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
        {
            mlContext.Model.Save(model, trainingDataViewSchema, _modelPath);

            Console.WriteLine("The model is saved to {0}", _modelPath);
        }
    }
}
