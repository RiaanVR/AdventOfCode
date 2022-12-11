using System.Linq.Expressions;
using System.Numerics;
using SuccincT.Functional;


static class Extensions
{
    public static int GetLowestCommonMultiple(this int left, int right)
    {
        var gcd =BigInteger.GreatestCommonDivisor(left, right);
        return (int)(left * (right/gcd));
       
    }
}
class DayEleven : IChallenge
{

    class Monkey
    {

        public static int? LowestCommonMultiple;
        public Monkey(IEnumerable<long> items, Delegate inspect, int[] operation)
        {
            Items = new Queue<long>(items);
            Inspect = inspect;

            var (divisor, (trueMonkey, (falseMonkey, rest))) = operation;
            Divisor = divisor;
            _trueMonkey = trueMonkey;
            _falseMonkey = falseMonkey;


        }

        public Queue<long> Items { get; private set; }
        public Delegate Inspect { get; private set; }

        private int _trueMonkey;
        private int _falseMonkey;




        public long ItemsSeen { get; private set; }
        public int Divisor { get; private set; }

        public Dictionary<int, List<long>> TakeTurn()
        {
            Dictionary<int, List<long>> outcome = new Dictionary<int, List<long>>();

            while (Items.TryDequeue(out long item))
            {
                ItemsSeen++;
                item = (long)Inspect.DynamicInvoke(item);
                item = LowestCommonMultiple.HasValue ? item % LowestCommonMultiple.Value : (int)(item / 3);
                var throwToMonkey = item % Divisor == 0 ? _trueMonkey: _falseMonkey;
                if (outcome.ContainsKey(throwToMonkey))
                {
                    outcome[throwToMonkey].Add(item);
                }
                else
                {
                    outcome.Add(throwToMonkey, new List<long> { item });
                }

            }
            return outcome;
        }


    }

    public object PartOne()
    {
        var monkeys = GetMonkeys();

        return MonkeyBusiness(monkeys, 20);
    }

    public object PartTwo()
    {
        var monkeys = GetMonkeys();

        var lowestCommonMultiple =monkeys.Select(m => m.Divisor).Aggregate(monkeys[0].Divisor, (accu, next) => accu.GetLowestCommonMultiple(next));
        Monkey.LowestCommonMultiple = lowestCommonMultiple;

        return MonkeyBusiness(monkeys, 10000);

    }

    private long MonkeyBusiness(Monkey[] monkeys, int rounds)
    {
        var monkeyBusinesses = Enumerable
            .Range(1, rounds)
            .Select(i =>
            {
                return monkeys.Select((m, j) =>
                {
                    var turnResults = m.TakeTurn();
                    foreach (var turnResult in turnResults)
                    {
                        foreach (var itemWorryLevel in turnResult.Value)
                            monkeys[turnResult.Key].Items.Enqueue(itemWorryLevel);
                    }
                    return m.ItemsSeen;
                }).ToArray();
            }).ToArray();

        return monkeyBusinesses
        .Last()
        .OrderByDescending(f => f)
        .Take(2)
         .Aggregate((long)1,
            (current, item) => current * item); ;
    }
    private Monkey[] GetMonkeys()
    {
        return ReadInput().Chunk(7).Select(GetMonkey).ToArray();
    }

    private Monkey GetMonkey(string[] lines)
    {
        var items = lines[1].Split(':')[1].Split(',').Select(i => Convert.ToInt64(i));
        var inspection = ParseInspection(lines[2].Split('=')[1]);
        var operation = ParseOperation(lines[3..]);

        return new Monkey(items, inspection, operation);
    }

    private int[] ParseOperation(string[] operationLines)
    {
        var foos = operationLines.
        Where(f => !string.IsNullOrEmpty(f)).
        Select(f => f.
            Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[^1])
            .Select(d => Convert.ToInt32(d))
            .ToArray();

        return foos;
    }

    private Delegate ParseInspection(string exp)
    {
        var old = Expression.Parameter(typeof(long), "old");
        var lambda = System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda(new[] { old }, null, exp);
        return lambda.Compile();
    }

    public IEnumerable<string> ReadInput()
    {
        return File.ReadLines("11/input.txt");
    }
}