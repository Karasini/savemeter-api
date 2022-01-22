using Instapp.Common.Cqrs.Commands;
using Microsoft.ML;
using Microsoft.ML.Data;
using SaveMeter.Services.Finances.Domain.Repositories;
using Serilog;

namespace SaveMeter.Services.Finances.Application.Commands.TrainNetwork
{
    class TrainNetworkCommandHandler : ICommandHandler<TrainNetworkCommand, string>
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
        private ITransactionRepository _transactionRepository;
        private ITransformer _trainedModel;
        private PredictionEngine<BankTransactionForNetwork, CategoryPrediction> _predEngine;
        private List<BankTransactionForNetwork> _testData;
        private readonly ILogger _logger;

        public TrainNetworkCommandHandler(ITransactionRepository transactionRepository, ILogger logger)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        public async Task<string> Handle(TrainNetworkCommand request, CancellationToken cancellationToken)
        {
            _testData = (await _transactionRepository.GetAll()).Select(x => new BankTransactionForNetwork()
            {
                Description = x.Description,
                Customer = x.Customer,
                Category = x.CategoryId.ToString()
            }).ToList();

            _mlContext = new MLContext(seed: 0);
            var trainingDataView = _mlContext.Data.LoadFromEnumerable(_testData);

            var pipeline = ProcessData();

            var trainingPipeline = BuildAndTrainModel(trainingDataView, pipeline);

            Evaluate(trainingDataView.Schema);

            var prediction = _predEngine.Predict(new BankTransactionForNetwork()
            {
                Description = request.Description,
                Customer = request.Customer,
            });

            _logger.Information($"=============== Single Prediction just-trained-model - Result: {prediction.Category} ===============");

            return prediction.Category;
        }

        IEstimator<ITransformer> ProcessData()
        {
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Category", outputColumnName: "Label")
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Customer", outputColumnName: "CustomerFeaturized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Description", outputColumnName: "DescriptionFeaturized"))
                .Append(_mlContext.Transforms.Concatenate("Features", "CustomerFeaturized", "DescriptionFeaturized"))
                .AppendCacheCheckpoint(_mlContext);

            // <SnippetReturnPipeline>
            return pipeline;
            // </SnippetReturnPipeline>
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
            var testDataView = _mlContext.Data.LoadFromEnumerable(_testData);

            var testMetrics = _mlContext.MulticlassClassification.Evaluate(_trainedModel.Transform(testDataView));
            _logger.Information($"*************************************************************************************************************");
            _logger.Information($"*       Metrics for Multi-class Classification model - Test Data     ");
            _logger.Information($"*------------------------------------------------------------------------------------------------------------");
            _logger.Information($"*       MicroAccuracy:    {testMetrics.MicroAccuracy:0.###}");
            _logger.Information($"*       MacroAccuracy:    {testMetrics.MacroAccuracy:0.###}");
            _logger.Information($"*       LogLoss:          {testMetrics.LogLoss:#.###}");
            _logger.Information($"*       LogLossReduction: {testMetrics.LogLossReduction:#.###}");
            _logger.Information($"*************************************************************************************************************");
        }

        private static void SaveModelAsFile(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
        {
            // <SnippetSaveModel>
            //mlContext.Model.Save(model, trainingDataViewSchema, _modelPath);
            // </SnippetSaveModel>

            //Console.WriteLine("The model is saved to {0}", _modelPath);
        }
    }
}
