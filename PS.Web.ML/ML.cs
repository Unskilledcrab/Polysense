using Microsoft.ML;
using System;
using System.Collections.Generic;

namespace PS.Web.ML
{
    public interface IMLInput
    {
        string Classification { get; set; }
        string Text { get; set; }
    }

    public interface IMLPrediction
    {
        string Classification { get; set; }
        float[] Probabilities { get; set; }
    }

    public static class ML
    {
        public static readonly string MLModelPath = "../model.zip";

        public static void Train<T>(IEnumerable<T> inputData, string path) where T : MLInput
        {
            var context = new MLContext();
            var data = context.Data.LoadFromEnumerable(inputData);

            var trainTestData = context.Data.TrainTestSplit(data, 0.2);
            var trainData = trainTestData.TrainSet;
            var testData = trainTestData.TestSet;
#if DEBUG
            Console.WriteLine("Creating pipeline...");
#endif
            var pipeline = context.Transforms.Text.FeaturizeText("Features", nameof(MLInput.Text))
                .Append(context.Transforms.Conversion.MapValueToKey("Label",
                                                                    nameof(MLInput.Classification),
                                                                    keyOrdinality: Microsoft.ML.Transforms.ValueToKeyMappingEstimator.KeyOrdinality.ByValue))
                .Append(context.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(context.Transforms.Conversion.MapKeyToValue(nameof(MLPrediction.Classification)));
#if DEBUG
            Console.WriteLine("Training the model...");
#endif
            var model = pipeline.Fit(trainData);

            var predictions = model.Transform(testData);
            var metrics = context.MulticlassClassification.Evaluate(predictions);
#if DEBUG
            Console.WriteLine();
            Console.WriteLine($"Macro Accuracy: {(metrics.MacroAccuracy * 100):0.##}%");
            Console.WriteLine($"Micro Accuracy: {(metrics.MicroAccuracy * 100):0.##}%");
            Console.WriteLine(metrics.ConfusionMatrix.GetFormattedConfusionTable());
            Console.WriteLine();
#endif
            // Here we can evaluate if the model is scoring better than the previous model, if so
            // save the score in the database and update the model being used

            context.Model.Save(model, data.Schema, MLModelPath);
        }

        public static IMLPrediction Predict<Input, Prediction>(string inputText)
            where Input : MLInput
            where Prediction : MLPrediction
        {
            var context = new MLContext();

            DataViewSchema modelSchema;
#if DEBUG
            Console.WriteLine("Loading model...");
#endif
            ITransformer trainedModel = context.Model.Load(MLModelPath, out modelSchema);

            var predictor = context.Model.CreatePredictionEngine<MLInput, MLPrediction>(trainedModel);

            var input = new MLInput { Text = inputText };
            var prediction = predictor.Predict(input);

#if DEBUG
            Console.WriteLine();
            var i = 0;
            foreach (var probability in prediction.Probabilities)
            {
                Console.WriteLine($"{i++}: {probability:P2}");
            }
            Console.WriteLine();
            Console.WriteLine($"Predicted Classification: {prediction.Classification}");
            Console.WriteLine();
#endif
            return prediction;
        }
    }

    public class MLInput : IMLInput
    {
        public string Classification { get; set; }
        public string Text { get; set; }
    }

    public class MLPrediction : IMLPrediction
    {
        public string Classification { get; set; }
        public float[] Probabilities { get; set; }
    }
}