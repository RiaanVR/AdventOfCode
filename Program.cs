// DayOne.Run();
// DayTwo.Run();
// DayThree.Run();
// DayFour.Run();
//DayFive.Run();
// DaySix.Run();
// DaySeven.Run();
// DayEight.Run();


IChallenge challenge = new DayNine();

var dayName = challenge.GetType().Name;

System.Console.WriteLine($"Day {dayName}.1: {challenge.PartOne()}");
System.Console.WriteLine($"Day {dayName}.2: {challenge.PartTwo()}");