namespace ResourceManagementLab.Services;

public sealed class FileStorage : IDisposable
{
    private readonly FileStream _stream;
    private bool _disposed;

    public FileStorage(string path)
    {
        // TODO 1: FileStream с OpenOrCreate + ReadWrite.
        _stream = new FileStream(
            path,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.None);
    }

    public void Write(string text)
    {
        // TODO 2: проверка состояния.
        ThrowIfDisposed();

        // TODO 3: преобразовать в байты.
        byte[] data = System.Text.Encoding.UTF8.GetBytes(text);

        // TODO 4: записать.
        _stream.Write(data);
        _stream.Flush();
    }

    public string Read()
    {
        // TODO 5: проверка состояния.
        ThrowIfDisposed();

        // TODO 6: позиция в начало.
        _stream.Position = 0;

        // TODO 7: прочитать. leaveOpen: true — поток остаётся у FileStorage.
        using var reader = new StreamReader(
            _stream,
            System.Text.Encoding.UTF8,
            detectEncodingFromByteOrderMarks: true,
            bufferSize: 1024,
            leaveOpen: true);

        return reader.ReadToEnd();
    }

    // Задание 7: повторный Dispose не падает.
    public void Dispose()
    {
        // TODO 8: идемпотентность.
        if (_disposed) return;

        // TODO 9: освободить поток.
        _stream.Dispose();

        // TODO 10: установить флаг.
        _disposed = true;

        Console.WriteLine("FileStorage: ресурс освобождён.");
    }

    // Задание 6: защита от использования после Dispose.
    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}