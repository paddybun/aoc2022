namespace AdventOfCode2022;

public class Day3
{
    public void DoWork()
    {
        var lines = File.ReadAllLines("./d3.txt");
        long scoreSolution1 = 0;
        long scoreSolution2 = 0;
        var charMapping = CreateCharMapping();

        foreach (var line in lines)
        {
            var comp1 = line[..(line.Length / 2)].ToCharArray();
            var comp2 = line[(line.Length / 2)..].ToCharArray();

            var toScore = comp1.Where(x => comp2.Contains(x)).GroupBy(x => x).First().Key;
            scoreSolution1 += charMapping[toScore];
        }

        foreach (var chunk in lines.Chunk(3))
        {
            var ruck1 = chunk[0].ToCharArray();
            var ruck2 = chunk[1].ToCharArray();
            var ruck3 = chunk[2].ToCharArray();

            var toScore = ruck1
                .Where(x => ruck2.Contains(x))
                .Where(x => ruck3.Contains(x))
                .GroupBy(x => x)
                .First().Key;

            scoreSolution2 += charMapping[toScore];
        }

        Console.WriteLine("\r\n----- Day 03 -----");
        Console.WriteLine($"Solution 01: {scoreSolution1}");
        Console.WriteLine($"Solution 02: {scoreSolution2}");
    }

    private Dictionary<char, int> CreateCharMapping()
    {
        var pointMapping = new Dictionary<char, int>();

        for (int i = 1; i < 27; i++)
        {
            var letter = Convert.ToChar((byte)(i + 96));
            pointMapping.Add(letter, i);
        }

        for (int i = 27; i < 53; i++)
        {
            var letter = Convert.ToChar((byte)(i + 38));
            pointMapping.Add(letter, i);
        }

        return pointMapping;
    }
}