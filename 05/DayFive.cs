using System.Text;

class DayFive
{

    class Move
    {
        public int NoOfCrates { get; set; }
        public int SourceStack { get; set; }
        public int DestinationStack { get; set; }
    }


    /*
            [G] [R]                 [P]    
            [H] [W]     [T] [P]     [H]    
            [F] [T] [P] [B] [D]     [N]    
        [L] [T] [M] [Q] [L] [C]     [Z]    
        [C] [C] [N] [V] [S] [H]     [V] [G]
        [G] [L] [F] [D] [M] [V] [T] [J] [H]
        [M] [D] [J] [F] [F] [N] [C] [S] [F]
        [Q] [R] [V] [J] [N] [R] [H] [G] [Z]
        1   2   3   4   5   6   7   8   9 
     */

    public static void Run()
    {

        var lines = ReadLines();
        (var stacks, var lineNumber) = ParseCrateArrangement(lines);


        var moves = ParseMoves(lines, lineNumber).ToArray();

        // PartOne(moves, stacks);
        PartTwo(moves, stacks);


        var sb = new StringBuilder();

        for (int i = 0; i < stacks.Length; i++)
        {
            sb.Append(stacks[i].Peek());
        }

        var topOfAllStacks = sb.ToString();

        System.Console.WriteLine(topOfAllStacks);
    }

    private static void PartTwo(IEnumerable<Move> moves, Stack<char>[] stacks)
    {

        foreach (var move in moves)
        {

            IEnumerable<char> toMove = Enumerable.Range(1, move.NoOfCrates).Select(_ => stacks[move.SourceStack - 1].Pop()).Reverse();
            foreach (char character in toMove)
            {
                stacks[move.DestinationStack - 1].Push(character);
            }
        }   

    }

    private static Stack<char>[] PartOne(IEnumerable<Move> moves, Stack<char>[] stacks)
    {
        foreach (var move in moves)
        {

            for (int i = 0; i < move.NoOfCrates; i++)
            {
                char toMove = stacks[move.SourceStack - 1].Pop();
                stacks[move.DestinationStack - 1].Push(toMove);

            }
        }

        return stacks;
    }

    private static (Stack<char>[], int endOfArrangement) ParseCrateArrangement(IEnumerable<string> lines)
    {
        Stack<char>[]? arrangement = null;
        int lineNumber = 0;
        foreach (var line in lines)
        {

            var cratesOfRow = line.Split(' ');

            if (arrangement == null)
            {
                arrangement = Enumerable.Range(0, line.Length / 4 + 1).Select((_, _) => new Stack<char>()).ToArray();
            }

            for (int i = 0; i < line.Length; i += 4)
            {
                if (char.IsWhiteSpace(line[i]))
                    continue;

                if (line[i] == '[')
                {
                    int stackToUse = i / 4;
                    arrangement[stackToUse].Push(line[i + 1]);
                }
            }

            if (cratesOfRow.All(string.IsNullOrEmpty))
                break;

            lineNumber++;
        }

        var a = arrangement.Select(f => new Stack<char>(f)).ToArray();
        return (a, lineNumber);
    }


    private static IEnumerable<Move> ParseMoves(IEnumerable<string> lines, int lineNumber)
    {
        return lines
        .Skip(lineNumber + 1)
        .Select(l => l.Split(' '))
        .Select(l => new[] { l[1], l[3], l[5] })
        .Select(l => l.Select(int.Parse).ToArray())
        .Select(l => new Move
        {
            NoOfCrates = l[0],
            SourceStack = l[1],
            DestinationStack = l[2]

        });
    }

    private static IEnumerable<string> ReadLines()
    {
        return File.ReadAllLines("05/input.txt");
    }
}