using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public class Day6
{

    public void DoWork()
    {
        var chars = File.ReadAllLines("./d6.txt").First().ToCharArray().ToList();

        Console.WriteLine("\r\n----- Day 06 -----");
        Console.WriteLine($"Solution 01: {LookAhead(0, 1, chars, new List<char>(), 3)}");
        Console.WriteLine($"Solution 02: {LookAhead(0, 1, chars, new List<char>(), 13)}");
    }

    private int LookAhead(int toCheckIndex, int checkDepth, List<char> charlist, List<char> checkedChars, int maxCheckDepth)
    {
        var currentChar = charlist[toCheckIndex];
        var charToCompare = charlist[toCheckIndex + checkDepth];
        if (currentChar != charToCompare && checkDepth != maxCheckDepth && !checkedChars.Contains(charToCompare))
        {
            checkedChars.Add(charToCompare);
            return LookAhead(toCheckIndex, ++checkDepth, charlist, checkedChars, maxCheckDepth);
        }

        if (currentChar != charToCompare && checkDepth == maxCheckDepth && !checkedChars.Contains(charToCompare))
        {
            return (toCheckIndex + 1) + checkDepth;
        }

        return LookAhead(++toCheckIndex, 1, charlist, new List<char>(), maxCheckDepth);
    }
}