
public class DayTen : IChallenge
{

    public class Cpu
    {

        public int Register { get; private set; }
        public int Cycle { get; private set; }
        public long SingalStrength => Register * Cycle;

        private Stack<Func<int, int, int>> Instructions = new();

        static Func<int, int, int> Add = (int a, int b) => { return a + b; };

        public event EventHandler? Ticked;

        public Cpu()
        {
            Register = 1;
            Cycle = 1;
        }

        public void Tick(object input)
        {

            switch (input)
            {
                case "noop":
                    break;
                case "addx":
                    Instructions.Push(Add);
                    break;
                case int a when input is int:
                    {
                        var func = Instructions.Pop();
                        Register = func(Register, a);
                    }
                    break;
            }
            Cycle++;

            Ticked?.Invoke(this, EventArgs.Empty);

        }

    }

    public class Crt
    {
        private Cpu _cpu;
        char[] _display;

        int _pixel;

        public Crt(Cpu cpu)
        {
            _cpu = cpu;
            _display = new char[240];

            _cpu.Ticked += FillPixel;
            //_cpu.Ticked += Render;
        }

        public void FillPixel(object? sender, EventArgs args)
        {

            int[] sprite = new[]
            {
                _cpu.Register - 1,
                _cpu.Register,
                _cpu.Register + 1
            };

            
            var a = _pixel + 1 - (((int)(_pixel / 40)) * 40);

            _display[_pixel] = sprite.Contains(a) ? '#' : ' ';

            _pixel++;

        }

        public void Render(object? sender, EventArgs args)
        {
            for (int i = 1; i <= 6; i++)
            {
                int a =(i * 40) - 40;
                int b = (i * 40);
                System.Console.WriteLine(string.Join(string.Empty, _display[a..b]));
            }
        }
    }

    public object PartOne()
    {
        int[] intervals = new int[]
        {
            20, 60, 100, 140, 180, 220
        };
        var cpu = new Cpu();
        List<long> singalStrenghts = new List<long>();
        foreach (var command in ParseInput())
        {
            if (intervals.Contains(cpu.Cycle))
                singalStrenghts.Add(cpu.SingalStrength);
            cpu.Tick(command);

        }

        return singalStrenghts.Sum();
    }

    public object PartTwo()
    {
        var cpu = new Cpu();
        var display = new Crt(cpu);

        foreach (var command in ParseInput())
        {
            cpu.Tick(command);
        }
        display.Render(null, EventArgs.Empty);
        return 0;
    }

    private IEnumerable<object> ParseInput()
    {
        return ReadInput().SelectMany(row => row.Split(' ').Select((it, i) => Parse(it.Trim(), i))).ToArray();
    }

    private static object Parse(string o, int idx)
    {
        switch (idx)
        {
            case 1: return Convert.ToInt32(o);
            default: return o;
        }
    }

    public IEnumerable<string> ReadInput()
    {
        return File.ReadLines("10/input.txt");
    }
}
