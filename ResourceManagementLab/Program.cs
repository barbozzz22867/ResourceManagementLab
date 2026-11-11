using ResourceManagementLab.Services;

namespace ResourceManagementLab;

internal class Program
{
    private static async Task Main()
    {
        string dataDirectory = Path.Combine(
            AppContext.BaseDirectory,
            "Data");

        Directory.CreateDirectory(dataDirectory);

        string filePath = Path.Combine(dataDirectory, "sample.txt");

        File.WriteAllText(filePath, "Hello from C#!\nResource management example.");

        Console.WriteLine("=== RESOURCE MANAGEMENT LAB — П2 ===");
        Console.WriteLine();

        // Задания 5, 6, 7: FileStorage + повторный Dispose + использование после Dispose.
        Console.WriteLine("1. FileStorage: Write/Read + Dispose");
        using (var storage = new FileStorage(filePath))
        {
            storage.Write("Данные записаны через FileStorage.");
            Console.WriteLine(storage.Read());
        }

        Console.WriteLine();
        Console.WriteLine("2. Повторный Dispose");
        var second = new FileStorage(filePath);
        second.Dispose();
        second.Dispose();
        second.Dispose();

        Console.WriteLine();
        Console.WriteLine("3. Использование после Dispose");
        try
        {
            second.Read();
        }
        catch (ObjectDisposedException ex)
        {
            Console.WriteLine($"ObjectDisposedException: {ex.Message}");
        }

        // Задание 8: владелец — вызывающий код, StreamReader не закрывает поток.
        Console.WriteLine();
        Console.WriteLine("4. Владение ресурсом + leaveOpen");
        using (var ownerStream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.ReadWrite))
        {
            using (var reader = new StreamReader(ownerStream, leaveOpen: true))
            {
                string line = reader.ReadLine() ?? string.Empty;
                Console.WriteLine($"Первая строка: {line}");
            }

            // Поток всё ещё открыт — им владеет вызывающий код.
            Console.WriteLine($"Поток открыт: {ownerStream.CanRead}");
        }

        // Задание 10: IAsyncDisposable.
        Console.WriteLine();
        Console.WriteLine("5. AsyncFileStorage");
        await using (var asyncStorage = new AsyncFileStorage(filePath))
        {
            await asyncStorage.WriteAsync("Данные через AsyncFileStorage.");
            Console.WriteLine(await asyncStorage.ReadAsync());
        }

        Console.WriteLine();
        Console.WriteLine("Работа программы завершена.");
    }
}