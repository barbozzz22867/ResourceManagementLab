namespace ResourceManagementLab.Services;

public sealed class AsyncFileStorage : IAsyncDisposable
{
    private readonly FileStream _stream;

    public AsyncFileStorage(string path)
    {
        _stream = new FileStream(
            path,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.None,
            bufferSize: 4096,
            useAsync: true);
    }

    public async ValueTask WriteAsync(string text)
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes(text);
        await _stream.WriteAsync(data);
        await _stream.FlushAsync();
    }

    public async ValueTask<string> ReadAsync()
    {
        _stream.Position = 0;

        using var reader = new StreamReader(
            _stream,
            System.Text.Encoding.UTF8,
            detectEncodingFromByteOrderMarks: true,
            bufferSize: 1024,
            leaveOpen: true);

        return await reader.ReadToEndAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _stream.DisposeAsync();
        Console.WriteLine("AsyncFileStorage: ресурс освобождён.");
    }
}