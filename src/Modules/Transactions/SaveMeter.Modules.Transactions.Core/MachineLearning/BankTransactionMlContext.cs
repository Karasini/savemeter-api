using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            [ColumnName("PredictedLabel")] public string Category;
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

        private readonly IPredictionModelRepository _predictionModelRepository;
        private readonly ILogger _logger;

        public BankTransactionMlContext(ILogger logger, IPredictionModelRepository predictionModelRepository)
        {
            _logger = logger;
            _predictionModelRepository = predictionModelRepository;
        }

        public byte[] TrainModel(IEnumerable<BankTransaction> bankTransactions)
        {
            var trainingData = bankTransactions.Select(x => new BankTransactionForNetwork()
            {
                Description = x.Description,
                Customer = x.Customer,
                CategoryId = x.CategoryId.ToString() ?? string.Empty,
            }).ToList();

            _mlContext = new MLContext(seed: 0);

            var trainingDataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = ProcessData();

            BuildAndTrainModel(trainingDataView, pipeline);

            return CreateModel(trainingDataView.Schema);
        }

        public async Task<Guid> Predicate(string customer, string description, Guid userId)
        {
            await CreateContextForPrediction(userId);

            var bankTransactionForNetwork = new BankTransactionForNetwork()
            {
                Customer = customer,
                Description = description
            };


            return Guid.Parse(_predEngine.Predict(bankTransactionForNetwork).Category);
        }

        private async Task CreateContextForPrediction(Guid userId)
        {
            if (_mlContext is not null)
            {
                return;
            }

            _mlContext = new MLContext(seed: 0);
            var model = await _predictionModelRepository.GetByUserId(userId);
            using var stream = new MemoryStream(model.Model);
            var loadedModel = _mlContext.Model.Load(stream, out _);
            _predEngine =
                _mlContext.Model.CreatePredictionEngine<BankTransactionForNetwork, CategoryPrediction>(loadedModel);
        }

        private IEstimator<ITransformer> ProcessData()
        {
            var pipeline = _mlContext.Transforms.Text
                .FeaturizeText(inputColumnName: @"Customer", outputColumnName: @"Customer")
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"Description",
                    outputColumnName: @"Description"))
                .Append(_mlContext.Transforms.Concatenate(@"Features", new[] { @"Customer", @"Description" }))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: @"CategoryId",
                    inputColumnName: @"CategoryId"))
                .Append(_mlContext.Transforms.NormalizeMinMax(@"Features", @"Features"))
                .Append(_mlContext.MulticlassClassification.Trainers.OneVersusAll(
                    binaryEstimator: _mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression(
                        new LbfgsLogisticRegressionBinaryTrainer.Options()
                        {
                            L1Regularization = 0.03125F, L2Regularization = 1.093764F, LabelColumnName = @"CategoryId",
                            FeatureColumnName = @"Features"
                        }), labelColumnName: @"CategoryId"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: @"PredictedLabel",
                    inputColumnName: @"PredictedLabel"));

            return pipeline;
        }

        private void BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
        {
            _trainedModel = pipeline.Fit(trainingDataView);
            _predEngine =
                _mlContext.Model.CreatePredictionEngine<BankTransactionForNetwork, CategoryPrediction>(_trainedModel);
        }

        private byte[] CreateModel(DataViewSchema trainingDataViewSchema)
        {
            using var stream = new MemoryStream();
            _mlContext.Model.Save(_trainedModel, trainingDataViewSchema, stream);
            return stream.ToArray();
        }
    }
}