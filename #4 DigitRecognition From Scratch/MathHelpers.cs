using System;
using System.Linq;

public static class MathHelpers
{
    private static Random rng = new Random();

    // Initialize weights with small random numbers
    public static float[,] InitMatrix(int rows, int cols)
    {
        var m = new float[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                m[i, j] = (float)(rng.NextDouble() - 0.5) * 0.01f;
        return m;
    }

    public static float[] InitVector(int size)
    {
        return new float[size]; // biases start as 0
    }

    // Dot product (vector * matrix)
    public static float[] Dot(float[] vector, float[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        if (vector.Length != rows) throw new Exception("Dimension mismatch!");

        float[] result = new float[cols];
        for (int j = 0; j < cols; j++)
        {
            float sum = 0;
            for (int i = 0; i < rows; i++)
                sum += vector[i] * matrix[i, j];
            result[j] = sum;
        }
        return result;
    }

    // Add bias
    public static float[] Add(float[] a, float[] b)
    {
        var result = new float[a.Length];
        for (int i = 0; i < a.Length; i++)
            result[i] = a[i] + b[i];
        return result;
    }

    // ReLU activation
    public static float[] ReLU(float[] x)
    {
        return x.Select(v => Math.Max(0, v)).ToArray();
    }

    // Derivative of ReLU
    public static float[] ReLUDerivative(float[] x)
    {
        return x.Select(v => v > 0 ? 1f : 0f).ToArray();
    }

    // Softmax
    public static float[] Softmax(float[] x)
    {
        float max = x.Max();
        var exps = x.Select(v => MathF.Exp(v - max)).ToArray();
        float sum = exps.Sum();
        return exps.Select(v => v / sum).ToArray();
    }

    // Cross entropy loss
    public static float CrossEntropy(float[] predicted, float[] target)
    {
        float loss = 0;
        for (int i = 0; i < predicted.Length; i++)
            loss += target[i] * MathF.Log(predicted[i] + 1e-9f);
        return -loss;
    }

    public static void DrawProgressBar(int processed, int total, int epoch, int barSize = 50)
    {
        double progress = (double)processed / total;
        int filled = (int)(progress * barSize);
        string bar = new string('█', filled) + new string('-', barSize - filled);

        Console.Write($"\rEpoch {epoch} [{bar}] {progress:P1}");
    }

}
