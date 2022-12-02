namespace AdventOfCode2022;

public class Day1
{
    public void DoWork()
    {
        var lines = File.ReadAllText("./d1.txt");
        var groups = lines.Split("\r\n\r\n");

        var largestFirst = groups
            .Select(x => x
                .Split("\r\n")
                .Sum(Convert.ToInt64))
            .OrderByDescending(x => x)
            .ToArray();

        var firstThree = largestFirst.Take(3).Sum();

        Console.WriteLine("\r\n----- Day 01 -----");
        Console.WriteLine($"Solution 01: {largestFirst[0]}");
        Console.WriteLine($"Solution 02: {firstThree}");
    }
}