namespace ResourceManagementLab.Services;

public static class MemoryDemo
{
    public static void Run()
    {
        Console.WriteLine("=== MEMORY DEMO ===");

        var first = new byte[1024];
        var second = new byte[1024 * 1024];
        var third = new byte[1024 * 1024 * 5];

        // TODO: поколения.
        Console.WriteLine($"first  поколение: {GC.GetGeneration(first)}");
        Console.WriteLine($"second поколение: {GC.GetGeneration(second)}");
        Console.WriteLine($"third  поколение: {GC.GetGeneration(third)}");

        // TODO: количество сборок.
        Console.WriteLine($"Gen 0: {GC.CollectionCount(0)}");
        Console.WriteLine($"Gen 1: {GC.CollectionCount(1)}");
        Console.WriteLine($"Gen 2: {GC.CollectionCount(2)}");

        // TODO: почему размер не определяет поколение?
        // Все три объекта только что созданы, поэтому физически находятся
        // в Gen 0. Поколение — это «возраст» объекта, а не его размер.
        // В Gen 1/2 объект попадает только после того, как переживёт сборку.
        // Большой массив (>= 85 000 байт) может попасть в LOH (Large Object
        // Heap), но это отдельная область, а не поколение 2.

        first = null!;
        second = null!;
        third = null!;

        Console.WriteLine("Ссылки потеряны.");
        Console.WriteLine(
            $"Память в куче: {GC.GetTotalMemory(false) / 1024} KB");
    }
}