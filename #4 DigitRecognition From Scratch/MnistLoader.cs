using System.IO;

public static class MnistLoader
{
    public static List<MnistSample> LoadData(string filePath)
    {
        var data = new List<MnistSample>();

        foreach (var line in File.ReadLines(filePath))
        {
            var values = line.Split(',').Select(int.Parse).ToArray();
            int label = values[0];
            float[] pixels = values.Skip(1).Select(v => v / 255f).ToArray();

            data.Add(new MnistSample { Label = label, Pixels = pixels });
        }

        return data;
    }
    public static void ShowImageFromCsvRow(MnistSample row)
    {
        int label = row.Label;
        var pixels = row.Pixels;
        Console.WriteLine($"Label: {label}");
        for (int i = 0; i < 28; i++)
        {
            for (int j = 0; j < 28; j++)
            {
                float pixel = pixels[i * 28 + j];
                char c = pixel > 200/255 ? '#' : pixel > 100/255 ? '+' : pixel > 50/255 ? '.' : ' ';
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }

}
