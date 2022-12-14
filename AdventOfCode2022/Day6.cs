using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public class Day6
{

    public void DoWork()
    {
        var chars = File.ReadAllLines("./d6.txt").First().ToCharArray().ToList();

        Console.WriteLine("\r\n----- Day 06 -----");
        Console.WriteLine($"Solution 01: {LookAhead(0, 1, 3, chars, new List<char>())}");
        Console.WriteLine($"Solution 02: {LookAhead(0, 1, 13, chars, new List<char>())}");
    }

    private int LookAhead(int toCheckIndex, int checkDepth, int maxCheckDepth, List<char> charlist, List<char> checkedChars)
    {
        var currentChar = charlist[toCheckIndex];
        var charToCompare = charlist[toCheckIndex + checkDepth];
        if (currentChar != charToCompare && checkDepth != maxCheckDepth && !checkedChars.Contains(charToCompare))
        {
            checkedChars.Add(charToCompare);
            return LookAhead(toCheckIndex, ++checkDepth, maxCheckDepth, charlist, checkedChars);
        }

        if (currentChar != charToCompare && checkDepth == maxCheckDepth && !checkedChars.Contains(charToCompare))
        {
            return (toCheckIndex + 1) + checkDepth;
        }

        return LookAhead(++toCheckIndex, 1, maxCheckDepth, charlist, new List<char>());
    }
}