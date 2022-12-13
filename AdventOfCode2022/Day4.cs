namespace AdventOfCode2022;

public class Day4
{
    public void DoWork()
    {
        var clearingPairs =
            File
                .ReadAllLines("./d4.txt")
                .Select(x => new ClearingPair(x)).ToList();

        var scoreSolution01 = 0;
        var scoreSolution02 = 0;
        foreach (var pairs in clearingPairs)
        {
            var isInBounds1 = IsInBounds(pairs.Pair1Min, pairs.Pair1Max, pairs.Pair2Min, pairs.Pair2Max, out var pair1Overlaps);
            var isInBounds2 = IsInBounds(pairs.Pair2Min, pairs.Pair2Max, pairs.Pair1Min, pairs.Pair1Max, out var pair2Overlaps);

            scoreSolution01 += isInBounds1 || isInBounds2
                    ? 1
                    : 0;

            scoreSolution02 += pair1Overlaps || pair2Overlaps ? 1 : 0;
        }

        Console.WriteLine("\r\n----- Day 04 -----");
        Console.WriteLine($"Solution 01: {scoreSolution01}");
        Console.WriteLine($"Solution 02: {scoreSolution02}");
    }

    private bool IsInBounds(int min, int max, int min1, int max1, out bool overlaps)
    {
        var boundsCheck = min <= min1 && max >= max1;

        overlaps = !(max < min1 || min > max1);
        
        return boundsCheck;
    }

    private struct ClearingPair
    {
        public ClearingPair(string line)
        {
            var res = line
                .Split(',')
                .SelectMany(x => x
                    .Split('-')
                    .Select(y => Convert.ToInt32(y)))
                .ToArray();

            Pair1Min = res[0];
            Pair1Max = res[1];
            Pair2Min = res[2];
            Pair2Max = res[3];
        }

        public int Pair1Min { get; set; }
        public int Pair1Max { get; set; }
        public int Pair2Min { get; set; }
        public int Pair2Max { get; set; }
    }
}