// See https://aka.ms/new-console-template for more information

class DayOne
{

    public static void Run()
    {

        int?[] allCalories = File.ReadAllLines("input.csv").Select(s => int.TryParse(s, out int calorie) ? calorie : (int?)null).ToArray();

        List<int> eachElfCalories = new List<int>();

        eachElfCalories.Add(0);

        foreach (int? calorie in allCalories)
        {
            if (!calorie.HasValue)
            {
                eachElfCalories.Add(0);
                continue;
            }

            eachElfCalories[^1] += calorie.Value;

        }

        eachElfCalories.Sort();

        int maxCalorieElf = eachElfCalories[^1];
        int sumOfTop3 = eachElfCalories[^1] + eachElfCalories[^2] + eachElfCalories[^3];


        System.Console.WriteLine(maxCalorieElf);
        System.Console.WriteLine(sumOfTop3);
    }
}