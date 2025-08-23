public class NeuralNetwork
{
    private int inputSize, hiddenSize, outputSize;

    // Weights and biases
    private float[,] W1, W2;
    private float[] b1, b2;

    private float learningRate;

    public NeuralNetwork(int input, int hidden, int output, float lr = 0.01f)
    {
        inputSize = input;
        hiddenSize = hidden;
        outputSize = output;
        learningRate = lr;

        // Initialize
        W1 = MathHelpers.InitMatrix(inputSize, hiddenSize);
        W2 = MathHelpers.InitMatrix(hiddenSize, outputSize);
        b1 = MathHelpers.InitVector(hiddenSize);
        b2 = MathHelpers.InitVector(outputSize);
    }

    // Forward pass
    public (float[] hidden, float[] output) Forward(float[] input)
    {
        var z1 = MathHelpers.Add(MathHelpers.Dot(input, W1), b1);
        var hidden = MathHelpers.ReLU(z1);
        var z2 = MathHelpers.Add(MathHelpers.Dot(hidden, W2), b2);
        var output = MathHelpers.Softmax(z2);
        return (hidden, output);
    }

    // One-hot encoding
    private float[] OneHot(int label)
    {
        var result = new float[outputSize];
        result[label] = 1;
        return result;
    }

    // Backpropagation
    public void Backprop(float[] input, int label, float[] hidden, float[] output)
    {
        var target = OneHot(label);

        // Error at output (softmax + cross-entropy derivative)
        float[] delta2 = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
            delta2[i] = output[i] - target[i];

        // Gradients for W2 and b2
        for (int i = 0; i < hiddenSize; i++)
            for (int j = 0; j < outputSize; j++)
                W2[i, j] -= learningRate * hidden[i] * delta2[j];

        for (int j = 0; j < outputSize; j++)
            b2[j] -= learningRate * delta2[j];

        // Error at hidden layer
        float[] delta1 = new float[hiddenSize];
        for (int i = 0; i < hiddenSize; i++)
        {
            float error = 0;
            for (int j = 0; j < outputSize; j++)
                error += delta2[j] * W2[i, j];
            delta1[i] = error * (hidden[i] > 0 ? 1f : 0f); // ReLU derivative
        }

        // Gradients for W1 and b1
        for (int i = 0; i < inputSize; i++)
            for (int j = 0; j < hiddenSize; j++)
                W1[i, j] -= learningRate * input[i] * delta1[j];

        for (int j = 0; j < hiddenSize; j++)
            b1[j] -= learningRate * delta1[j];
    }

    // Training step
    public float TrainOnSample(float[] input, int label)
    {
        var (hidden, output) = Forward(input);
        float loss = MathHelpers.CrossEntropy(output, OneHot(label));
        Backprop(input, label, hidden, output);
        return loss;
    }

    // Prediction
    public int Predict(float[] input)
    {
        var (_, output) = Forward(input);
        return Array.IndexOf(output, output.Max());
    }

    public void SaveWeights(string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            // Save W1
            writer.WriteLine("#W1");
            for (int i = 0; i < W1.GetLength(0); i++)
            {
                for (int j = 0; j < W1.GetLength(1); j++)
                {
                    writer.Write(W1[i, j] + " ");
                }
                writer.WriteLine();
            }

            // Save b1
            writer.WriteLine("#b1");
            writer.WriteLine(string.Join(" ", b1));

            // Save W2
            writer.WriteLine("#W2");
            for (int i = 0; i < W2.GetLength(0); i++)
            {
                for (int j = 0; j < W2.GetLength(1); j++)
                {
                    writer.Write(W2[i, j] + " ");
                }
                writer.WriteLine();
            }

            // Save b2
            writer.WriteLine("#b2");
            writer.WriteLine(string.Join(" ", b2));
        }
    }
}
