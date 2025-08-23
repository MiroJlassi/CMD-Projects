// DataSet path : https://www.kaggle.com/datasets/oddrationale/mnist-in-csv
class Program
{
    static void Main()
    {
        // 1. Load datasets
        var trainData = MnistLoader.LoadData("Dataset/mnist_train.csv");
        var testData = MnistLoader.LoadData("Dataset/mnist_test.csv");

        Console.WriteLine($"Training samples: {trainData.Count}, Test samples: {testData.Count}");

        // 2. Create NN
        var nn = new NeuralNetwork(784, 128, 10, lr: 0.01f);

        int epochs = 1;

        // 3. Training loop
        for (int epoch = 1; epoch <= epochs; epoch++)
        {
            float totalLoss = 0;
            int correct = 0;

            int totalSamples = trainData.Count;
            int processed = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var sample in trainData)
            {
                float loss = nn.TrainOnSample(sample.Pixels, sample.Label);
                totalLoss += loss;

                int prediction = nn.Predict(sample.Pixels);
                if (prediction == sample.Label) correct++;

                processed++;
                if (processed % 1000 == 0 || processed == totalSamples)
                {
                    MathHelpers.DrawProgressBar(processed, totalSamples, epoch);
                }
            }

            watch.Stop();
            float accuracy = (float)correct / totalSamples;
            Console.WriteLine($"\nEpoch {epoch} completed in {watch.Elapsed.TotalSeconds:F2}s " +
                              $"- Loss: {totalLoss / totalSamples:F4}, Accuracy: {accuracy:P2}");

            // Save epoch weights
            nn.SaveWeights($"weights_epoch_{epoch}.txt");
        }

        // Save final model
        nn.SaveWeights("final_model.txt");


        // 4. Evaluate on test set
        int testCorrect = 0;
        foreach (var sample in testData)
        {
            int prediction = nn.Predict(sample.Pixels);
            if (prediction == sample.Label)
                testCorrect++;
        }
        float testAccuracy = (float)testCorrect / testData.Count;
        Console.WriteLine($"Test Accuracy: {testAccuracy:P2}");
    }
}