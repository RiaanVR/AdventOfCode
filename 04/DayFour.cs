class DayFour
{
    public static void Run()
    {
        PartOne();
        PartTwo();
    }
    private static void PartTwo()
    {
        var totalOverlappingSegments = ReadLines()
            .Select(l => ParseSegments(l))
            .GroupBy(p => HaveAnyOverlap(p.firstElfSegment, p.secondElfSegment) || HaveAnyOverlap(p.secondElfSegment, p.firstElfSegment))
            .Where(g => g.Key)
            .Sum(g => g.Count());

        System.Console.WriteLine(totalOverlappingSegments);
    }

    private static void PartOne()
    {
        var totalOverlappingSegments = ReadLines()
            .Select(l => ParseSegments(l))
            .GroupBy(p => OverlapEachOther(p.firstElfSegment, p.secondElfSegment) || OverlapEachOther(p.secondElfSegment, p.firstElfSegment))
            .Where(g => g.Key)
            .Sum(g => g.Count());

        System.Console.WriteLine(totalOverlappingSegments);
    }

    private static bool HaveAnyOverlap(int[] source, int[] candiates)
    {
        return candiates.Any(c => source.Contains(c));

    }

    private static bool OverlapEachOther(int[] source, int[] candiates)
    {
        return candiates.All(c => source.Contains(c));
    }

    private static (int[] firstElfSegment, int[] secondElfSegment) ParseSegments(string line)
    {

        string[] segments = line.Split(',');
        return (ParseSegment(segments[0]), ParseSegment(segments[1]));
    }

    private static int[] ParseSegment(string segment)
    {
        int[] range = segment.Split('-').Select(i => int.Parse(i)).ToArray();
        return Enumerable.Range(range[0], range[1] - range[0] + 1).ToArray();
    }


    private static IEnumerable<string> ReadLines()
    {
        return File.ReadAllLines("04/input.csv");
    }

}