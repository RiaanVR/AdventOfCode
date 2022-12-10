public interface IChallenge
{
    public IEnumerable<string> ReadInput();
    public object PartOne();

    public object PartTwo();
}

class DayNine : IChallenge
{
    record Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public Point Move(Direction direction)
        {
            return this with { X = X + direction.X, Y = Y + direction.Y };
        }

        public int MaximumDistanceFrom(Point other)
        {

            var a = Math.Sqrt(
                Math.Pow(X - other.X, 2) +
                Math.Pow(Y - other.Y, 2));
            var xDistance = Math.Abs(X - other.X);
            var yDistance = Math.Abs(Y - other.Y);
            return (int)a;


        }
    }


    HashSet<Point> TouchedByTail = new();
    record Direction(int X, int Y)
    {
        public static Direction operator *(Direction dir, int no)
        {
            return dir with { X = dir.X * no, Y = dir.Y * no};
        }
    }

    static Dictionary<char, Direction> Directions = new()
    {
        { 'U', new(0, 1) },
        { 'D', new(0, -1) },
        { 'L', new(-1, 0) },
        { 'R', new(1, 0) },
    };

    public object PartOne()
    {

        var head = new Point(0, 0);
        var prevHead = new Point(0, 0);
        var tail = new Point(0, 0);
        TouchedByTail.Add(tail);


        foreach (var move in ParseInput())
        {
            for (int i = 0; i < move.NumOfMoves; i++)
            {

                prevHead = head;
                var h = head.Move(move.Direction);
                /*
                If the head is ever two steps directly up, down, left, or right from the tail, 
                the tail must also move one step in that direction so it remains close enough: 
                */
                if (tail.MaximumDistanceFrom(h) > 1)
                {
                    TouchedByTail.Add(prevHead);
                    tail = prevHead;
                }
                head = h;
            }
        }

        return TouchedByTail.Count();
    }

    public object PartTwo()
    {
        TouchedByTail = new HashSet<Point>();

        LinkedList<Point> snake = new();
        for (int i = 0; i < 9; i++)
        {
            snake.AddFirst(new LinkedListNode<Point>(new Point(0, 0)));
        }

        foreach (var move in ParseInput())
        {
            for (int i = 0; i < move.NumOfMoves; i++)
            {
               
            }
        }

        return TouchedByTail.Count();
    }

    private IEnumerable<(Direction Direction, int NumOfMoves)> ParseInput()
    {
        return ReadInput().Select(d => (Directions[d[0]], Convert.ToInt32(d[1..].ToString())));
    }

    public IEnumerable<string> ReadInput()
    {
        return File.ReadLines("09/example2.txt");
    }


}