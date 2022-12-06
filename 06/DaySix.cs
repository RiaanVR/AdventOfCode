class DaySix
{

    public static void Run()
    {
        Example();
        PartOne();
        PartTwo();
    }

    public static void Example()
    {
        IEnumerable<(string input, int expected)> lines = File.ReadAllLines("06/example.csv").Select(l => l.Split(',')).Select(row => (row[0], int.Parse(row[1])));

       var matched = lines.Select(row => (found: ExtractMarker(row.input), row.expected));

        foreach (var item in matched)
        {
            System.Console.WriteLine($"Expected: {item.expected}, Found: {item.found}");
        }

    }

    private static void PartOne()
    {
        var buffer  = File.ReadAllText("06/input.txt");
        System.Console.WriteLine(ExtractMarker(buffer));
    }

    private static void PartTwo()
    {
        var buffer  = File.ReadAllText("06/input.txt");
        System.Console.WriteLine(ExtractMarker(buffer, 14));
    }

    private static int ExtractMarker(string input, int markerWidth = 4)
    {
        var inputSpan = input.AsSpan();
        var firstUniqueTrigram = Enumerable.Range(markerWidth, input.Length)
        .Select(i => input[(i-markerWidth)..Math.Min(i, input.Length)])
        .Where(i => i.Length == markerWidth)
        .Where(i => i.Distinct().Count() == markerWidth)
        .First();
        return input.IndexOf(firstUniqueTrigram) + markerWidth;
    }


}