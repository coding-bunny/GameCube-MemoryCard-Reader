using GC_MemoryCard_Reader;
using GC_MemoryCard_Reader.Parsers;

internal class Program
{
    private static void Main(string[] args)
    {
        string path = "C:\\Users\\arned\\OneDrive\\GameCube\\1019b_2021_07Jul_25_15-52-05.raw";

        Console.WriteLine($"Reader File from {path}");

        var memoryCard = VirtualCard.FromPath(path);

        Console.WriteLine(memoryCard.ToString());

    }
}