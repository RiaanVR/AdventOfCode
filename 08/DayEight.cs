class DayEight
{
    public static void Run()
    {
        var noOfTallest = PartOne();
        System.Console.WriteLine($"PartOne: {noOfTallest}");

        var highestScenicScore = PartTwo();
        System.Console.WriteLine($"PartTwo: {highestScenicScore}");
    }

    private static int PartTwo()
    {
        int[][] input = ReadInput();
        var highestScenicScore = input.Select((line, row) =>
        {
            

            var rowMaximumScore =
                Enumerable
                .Range(0, line.Length)
                .Select(col => CalculateScenicScore(input, row, col))
                .ToArray();

            return rowMaximumScore.Max();

        }).Max();

        return highestScenicScore;
    }

    private static int PartOne()
    {
        int[][] input = ReadInput();
        var visibleFromEdges = input.Select((row, r) =>
        {
            if (r == 0 || r == input.Length - 1) return row.Count();

            var visibleFromEdges = Enumerable
            .Range(0, row.Length)
            .Select(c => VisibleFromEdge(input, r, c))
            .Where(c => c)
            .Count();
            return visibleFromEdges;
        }).Sum();
        return visibleFromEdges;
    }

    private static int CalculateScenicScore(int[][] space, int row, int col)
    {
        if(row == 0 || row == space.Length - 1 || col == 0 || col == space[0].Length - 1)
         return 1;

        var rowScore  = RowScenicScore();
        var columnScore = ColumScenicScore();
        
        return rowScore * columnScore;

        int RowScenicScore()
        {
            int treesInPositiveDirection = 0;
            for (int i = col-1; i >= 0; i--)
            {
                if (space[row][col] > space[row][i])
                    treesInPositiveDirection++;
                else if (space[row][col] <= space[row][i])
                {
                    treesInPositiveDirection++;
                    break;
                }
                else
                    break;

            }

            int treesInNegativeDirection = 0;
            for (int i = col + 1; i < space[row].Length; i++)
            {
                if (space[row][col] > space[row][i])
                    treesInNegativeDirection++;
                else if (space[row][col] <= space[row][i])
                {
                    treesInNegativeDirection++;
                    break;
                }
                else
                    break;
            }

            return Math.Max(treesInNegativeDirection,1) * Math.Max(treesInPositiveDirection,1);
        }
        int ColumScenicScore()
        {

            int treesInPositiveDirection = 0;
            for (int i = row - 1; i >= 0; i--)
            {
                if (space[row][col] > space[i][col])
                    treesInPositiveDirection++;
                else if (space[row][col] <= space[i][col])
                {
                    treesInPositiveDirection++;
                    break;
                }
                else
                    break;

            }

            int treesInNegativeDirection = 0;
            for (int i = row + 1; i < space.Length; i++)
            {
                if (space[row][col] > space[i][col])
                    treesInNegativeDirection++;
                else if (space[row][col] <= space[i][col])
                {
                    treesInNegativeDirection++;
                    break;
                }
                else
                    break;


            }
            return Math.Max(treesInNegativeDirection, 1) * Math.Max(treesInPositiveDirection,1);


        }
    }

    private static bool VisibleFromEdge(int[][] inputs, int row, int col)
    {
        if (col == 0 || inputs[row].Length - 1 == col) return true;

        var rowVisible = VisibleInRow(inputs, row, col);

        return rowVisible || VisibleInCol(inputs, row, col);
    }

    private static bool VisibleInRow(int[][] inputs, int row, int col)
    {
        return Enumerable.Range(0, col).All(i => inputs[row][col] > inputs[row][i])
        || Enumerable.Range(col + 1, inputs[row].Length - col - 1).All(i => inputs[row][col] > inputs[row][i]);
    }


    private static bool VisibleInCol(int[][] inputs, int row, int col)
    {
        return Enumerable.Range(0, row).All(i => inputs[row][col] > inputs[i][col])
      || Enumerable.Range(row + 1, inputs.Length - row - 1).All(i => inputs[row][col] > inputs[i][col]);
    }



    private static int[][] ReadInput()
    {
        var rows = File.ReadAllLines("08/input.txt");
        return rows.Select(r => r.Select(f => int.Parse(f.ToString())).ToArray()).ToArray();
    }
}