using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;
using SaveMeter.Services.Finances.Domain.Repositories;
using Serilog;

namespace SaveMeter.Services.Finances.Infrastructure.MachineLearning
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
            public string Category { get; set; }
            public string Customer { get; set; }
            public string Description { get; set; }
        }

        private MLContext _mlContext;
        private ITransformer _trainedModel;
        private PredictionEngine<BankTransactionForNetwork, CategoryPrediction> _predEngine;
        private List<BankTransactionForNetwork> _testData;
        private readonly ILogger _logger;
        private string _modelPath => "mlModel.zip";

        public BankTransactionMlContext(ILogger logger)
        {
            _logger = logger;
        }

        public void TrainModel(IEnumerable<BankTransaction> bankTransactions)
        {
            _testData = bankTransactions.Select(x => new BankTransactionForNetwork()
            {
                Description = x.Description,
                Customer = x.Customer,
                Category = x.CategoryId.ToString() ?? string.Empty
            }).ToList();

            _mlContext = new MLContext(seed: 0);

            var trainingDataView = _mlContext.Data.LoadFromEnumerable(_testData);

            var pipeline = ProcessData();

            var trainingPipeline = BuildAndTrainModel(trainingDataView, pipeline);

            Evaluate(trainingDataView.Schema);
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
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Category", outputColumnName: "Label")
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Customer", outputColumnName: "CustomerFeaturized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Description", outputColumnName: "DescriptionFeaturized"))
                .Append(_mlContext.Transforms.Concatenate("Features", "CustomerFeaturized", "DescriptionFeaturized"))
                .AppendCacheCheckpoint(_mlContext);

            return pipeline;
        }

        IEstimator<ITransformer> BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
        {
            var trainingPipeline = pipeline.Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            _trainedModel = trainingPipeline.Fit(trainingDataView);
            _predEngine = _mlContext.Model.CreatePredictionEngine<BankTransactionForNetwork, CategoryPrediction>(_trainedModel);

            var bankTransaction = new BankTransactionForNetwork()
            {
                Customer = "pyszne.pl PayU Grunwaldzka 186 60-166 Poznan",
                Description = "XX1231231301XX Pyszne.pl zamówienie nr EI45MT PayU S.A."
            };

            var prediction = _predEngine.Predict(bankTransaction);

            _logger.Information($"=============== Single Prediction just-trained-model - Result: {prediction.Category} ===============");
            return trainingPipeline;
        }

        void Evaluate(DataViewSchema trainingDataViewSchema)
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
