using ResourceManagementLab.Services;

namespace ResourceManagementLab;

internal class Program
{
    private static void Main()
    {
        string dataDirectory = Path.Combine(
            AppContext.BaseDirectory,
            "Data");

        Directory.CreateDirectory(dataDirectory);

        string filePath = Path.Combine(dataDirectory, "sample.txt");

        File.WriteAllText(filePath,
            "Hello from C#!\nResource management example.");

        Console.WriteLine("=== RESOURCE MANAGEMENT LAB — П1 ===");
        Console.WriteLine();

        // Задание 1 / 3: без using.
        Console.WriteLine("1. Чтение без using");
        Console.WriteLine(FileReaderWithoutUsing.Read(filePath));

        // Задание 1 / 3: с using.
        Console.WriteLine();
        Console.WriteLine("2. Чтение с using");
        Console.WriteLine(FileReaderWithUsing.Read(filePath));

        // Задание 2: исключение внутри using.
        Console.WriteLine();
        Console.WriteLine("3. Исключение внутри using");
        try
        {
            FileReaderWithUsing.ReadFileWithError(filePath);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Перехвачена ошибка: {ex.Message}");
        }
        // Dispose() уже вызван, хотя исключение и было выброшено.

        // Задание 4: поколения GC.
        Console.WriteLine();
        MemoryDemo.Run();

        Console.WriteLine();
        Console.WriteLine("Работа программы завершена.");
    }
}