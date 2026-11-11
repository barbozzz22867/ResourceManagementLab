namespace ResourceManagementLab.Services;

// Правильный вариант: both объекта освобождаются автоматически.
public static class FileReaderWithUsing
{
    public static string Read(string filePath)
    {
        using var stream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read);

        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }

    // Задание 2: искусственное исключение после чтения.
    // using всё равно вызовет Dispose() при выходе из метода,
    // даже если внутри блока выброшено исключение.
    public static string ReadFileWithError(string filePath)
    {
        using var stream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read);

        using var reader = new StreamReader(stream);

        string content = reader.ReadToEnd();

        throw new InvalidOperationException(
            "Искусственная ошибка после чтения файла.");
    }
}