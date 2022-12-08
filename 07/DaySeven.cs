
// $ cd /
// $ ls
// dir a
// 14848514 b.txt
// 8504156 c.dat
// dir d
// $ cd a
// $ ls
// dir e
// 29116 f
// 2557 g
// 62596 h.lst
// $ cd e
// $ ls
// 584 i
// $ cd ..
// $ cd ..
// $ cd d
// $ ls
// 4060174 j
// 8033020 d.log
// 5626152 d.ext
// 7214296 k

// - / (dir)
//   - a (dir)
//     - e (dir)
//       - i (file, size=584)
//     - f (file, size=29116)
//     - g (file, size=2557)
//     - h.lst (file, size=62596)
//   - b.txt (file, size=14848514)
//   - c.dat (file, size=8504156)
//   - d (dir)
//     - j (file, size=4060174)
//     - d.log (file, size=8033020)
//     - d.ext (file, size=5626152)
//     - k (file, size=7214296)

// 95437

class DaySeven
{
    public static void Run()
    {

        //Example();
        //PartOne();
        PartTwo();
    }

    class Folder{
        public Folder(string name, Folder? parent, long size, List<Folder> children)
        {
            Name = name;
            Parent = parent;
            Size = size;
            Children = new List<Folder>();
        }

        public string Name { get; private set; }
        public Folder? Parent { get; private set; }
        public List<Folder> Children { get;private set; }
        public long Size { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Size: {Size:D}, Children: {Children.Select(c=>c.ToString())}";
        }
    }


    private static void PartOne()
    {
        string[] lines = ReadFile("input.txt");

        (long sumOfDirectoriesBelowThreshold, _) = CalculateSumOfDirectories(lines);

        System.Console.WriteLine($"PartOne: {sumOfDirectoriesBelowThreshold}");

    }

    private static void PartTwo()
    {
        string[] lines = ReadFile("input.txt");
        (_, Folder current) = CalculateSumOfDirectories(lines);

        Folder cu = current;
        long totalSize = 0;
        while (current.Parent != null)
        {
            totalSize += current.Size;
            current = current.Parent;

        }
        totalSize += current.Size;

        long totalNeeded = 30_000_000 - (70_000_000 - totalSize);
        long size = current.Size;

        var b = FindBestMatch(current);


        // find_min(node):
        //     result = node.value
        //     for each child of node:
        //         child_min = find_min(child)
        //         if child_min < result:
        //             result = child_min
        //     return result

        Folder FindBestMatch(Folder parent)
        {
                var c =  parent.Children.Where(f => f.Size >= totalNeeded).MinBy(f => f.Size);
            if (c is not null)
            {
                foreach (var f in parent.Children)
                {
                    return FindBestMatch(f);
                }
                return c;
            }

            return parent;

        }

    }




    private static void Example()
    {
        string[] lines = ReadFile("example.txt");

        (long sumOfDirectoriesBelowThreshold, _) = CalculateSumOfDirectories(lines);

        System.Console.WriteLine($"Expected: 95437, Got: {sumOfDirectoriesBelowThreshold}");
    }

    private static (long v, Folder current) CalculateSumOfDirectories(string[] lines, int threshold = 100_000)
    {

        long sumOfDirectoriesBelowThreshold = 0;
        Folder? folder = null;

        foreach (var line in lines)
        {

            string[] parts = line.Split(' ');
            if (parts[0] == "$")
            {
                if (parts[1] == "cd")
                {
                    if (parts[2] != "..")
                    {
                        var p = folder;
                        folder = new Folder(parts[2], folder, 0, new List<Folder>());
                        p?.Children.Add(folder);
                    }
                    else
                    {
                        if (folder.Size <= threshold)
                            sumOfDirectoriesBelowThreshold += folder.Size;

                        long size = folder.Size;
                        folder = folder.Parent;
                        folder.Size += size;
                    }
                }
            }
            else if (parts[0] == "dir")
            { continue; }
            else
            {
                // folder listing
                int fileSize = int.Parse(parts[0]);
                folder.Size += fileSize;

            }
        }

        return (sumOfDirectoriesBelowThreshold, folder);
    }


    private static string[] ReadFile(string fileName)
    {
        return File.ReadAllLines($"07/{fileName}");
    }
}

