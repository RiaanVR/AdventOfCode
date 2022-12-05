class DayTwo
{
    static Dictionary<char, int> moveScore = new()
        {
            {'X', 1},
            {'Y', 2},
            {'Z', 3}
        };

    static Dictionary<int, int> outcomeScore = new()
        {
            {1, 6},
            {0, 3},
            {-1, 0}
        };

    static Dictionary<char, Dictionary<char, int>> possibilities = new()
        {
            {'A', new() { {'X', 0}, {'Y',1}, {'Z', -1}} },
            {'B', new() { {'X', -1}, {'Y',0}, {'Z', 1}} },
            {'C', new() { {'X', 1}, {'Y',-1}, {'Z', 0}} }
        };

        
    static (char p1, char p2)[] Strategy;

    public static void Run()
    {

        string[] lines = File.ReadAllLines("input.csv");

        Strategy = lines.Select(l => l.Split(" ")).Select(i => (i[0][0], i[1][0])).ToArray();


        System.Console.WriteLine(PartOne());
        System.Console.WriteLine(PartTwo());
    }

    // A, X - Rock (1)
    // B, Y - Paper (2)
    // C, Z - Scissors (3)
    // [A, B, C]
    // [X, Y, Z]
    // [1, 2, 3]



    static int PartOne()
    {
        int playerTwoScore = 0;
        foreach ((char p1, char p2) in Strategy)
        {
            int outcome = possibilities[p1][p2];
            int outcomeAddition = outcomeScore[outcome];
            int moveAddition = moveScore[p2];
            playerTwoScore += outcomeAddition + moveAddition;

        }

        return playerTwoScore;

    }

    static int PartTwo()
    {
        Dictionary<char, Dictionary<char, char>> requiredOutcome = new()
    {
        {'A', new() { {'X', 'Z'}, {'Y','X'}, {'Z', 'Y'}} },
        {'B', new() { {'X', 'X'}, {'Y','Y'}, {'Z', 'Z'}} },
        {'C', new() { {'X', 'Y'}, {'Y','Z'}, {'Z', 'X'}} }
    };

        int playerTwoScore = 0;
        foreach ((char p1, char p2) in Strategy)
        {
            char moveToPlay = requiredOutcome[p1][p2];
            int outcome = possibilities[p1][moveToPlay];
            int outcomeAddition = outcomeScore[outcome];
            int moveAddition = moveScore[moveToPlay];
            playerTwoScore += outcomeAddition + moveAddition;

        }

        return playerTwoScore;
    }

}