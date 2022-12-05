class DayThree
{

    const int UpperCaseAsciiOffset = (int)'A' - 27;
    const int LowerCaseAsciiOffset = (int)'a' - 1;

    public static void Run()
    {
        PartOne();
        PartTwo();
    }

    private static void PartTwo()
    {
        long fileTotal = 0;

        string[] lines = ReadLines().ToArray();

        for (int i = 2; i < lines.Length; i += 3)
        {
            int groupTotal = DuplicatedCharacters(lines[i], lines[i - 1], lines[i-2])
            .Select(ch => {
                var offset = Char.IsUpper(ch)? UpperCaseAsciiOffset: LowerCaseAsciiOffset;
                return (int)ch - offset;
            })
            .Sum();
            fileTotal += groupTotal;
        }
        System.Console.WriteLine(fileTotal);

    }

    private static void PartOne(){
        long fileTotal  = ReadLines()
        .Select(line => SplitInTwo(line))
        .Select(halves => DuplicatedCharacters(halves.firstHalf, halves.secondHalf)
            .Select(ch => {
                var offset = Char.IsUpper(ch)? UpperCaseAsciiOffset: LowerCaseAsciiOffset;
                return (int)ch - offset;
            })
            .Sum())
            .Sum();

        System.Console.WriteLine(fileTotal);
    }
    private static (string firstHalf, string secondHalf) SplitInTwo(string input)
    {
        int halfPoint = input.Length / 2;
        string firstHalf = input[0..halfPoint];
        string secondHalf = input[halfPoint..input.Length];
        return (firstHalf, secondHalf);
    }

    private static IEnumerable<char> DuplicatedCharacters(string firstHalf, params string[] searchSpace)
    {
        List<char> repeatedCharacters = new();
        for (int i = 0; i < firstHalf.Length; i++)
            if(searchSpace.All(sp => sp.IndexOf(firstHalf[i]) >= 0))
                repeatedCharacters.Add(firstHalf[i]);

        return repeatedCharacters.Distinct();
    }

    private static IEnumerable<string> ReadLines()
    {
        return File.ReadAllLines("03/input.csv");
    }


}