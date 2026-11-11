namespace ResourceManagementLab.Services;

// Намеренно неправильный вариант: Dispose() не вызывается.
//
// Задание 1:
// - FileStream владеет файловым ресурсом (дескриптор файла в ОС).
// - StreamReader тоже реализует IDisposable.
// - Владелец FileStream здесь — сам метод Read: он создал — он и должен освободить.
// - GC не гарантирует своевременного закрытия: финализация может
//   произойти позже, а может вообще не произойти до завершения процесса.
public static class FileReaderWithoutUsing
{
    public static string Read(string filePath)
    {
        var stream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read);

        var reader = new StreamReader(stream);

        // Ни stream, ни reader не освобождаются.
        return reader.ReadToEnd();
    }
}