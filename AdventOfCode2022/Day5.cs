using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public class Day5
{
    private static readonly Regex LetterRegex = new(@"\[(?<letter>\w)\]");

    public void DoWork()
    {
        var lines = File.ReadAllLines("./d5.txt");
        var operations = lines.Select(x => new Operation(x)).Where(x => x.Amount != 0).ToList();
        var gridLines = lines
            .TakeWhile(x => x.Trim()
                .StartsWith("["))
            .Reverse()
            .ToList();

        var grid1 = CreateGrid(gridLines);
        var grid2 = CreateGrid(gridLines);

        Console.WriteLine("\r\n----- Day 05 -----");
        Console.WriteLine($"Solution 01: {ProvideSolution1(grid1, operations)}");
        Console.WriteLine($"Solution 02: {ProvideSolution2(grid2, operations)}");
    }

    private string ProvideSolution1(List<List<string>> grid, List<Operation> operations)
    {
        foreach (var operation in operations)
        {
            var fromList = grid[operation.From-1];
            var toList = grid[operation.To-1];
            for (var i = 0; i < operation.Amount; i++)
            {
                var element = fromList.Last();
                toList.Add(element);
                fromList.RemoveAt(fromList.Count - 1);
            }
        }

        return string.Join("", grid.SelectMany(x => x.Last()));
    }

    private string ProvideSolution2(List<List<string>> grid, List<Operation> operations)
    {
        foreach (var operation in operations)
        {
            var fromList = grid[operation.From-1];
            var toList = grid[operation.To-1];
            
            var elements = fromList.Take((fromList.Count - operation.Amount)..);
            toList.AddRange(elements);
            fromList.RemoveRange(fromList.Count - operation.Amount, operation.Amount);
        }

        return string.Join("", grid.SelectMany(x => x.Last()));
    }

    public List<List<string>> CreateGrid(List<string> gridLines)
    {
        var work = new List<string>(gridLines);

        var first = work.First();
        work.RemoveRange(0, 1);
        var grid = EstablishGridDimensions(first);
        
        foreach (var line in work)
        {
            var test = line;
            var sublistIndex = 0;
            do
            {
                var sublist = grid[sublistIndex];
                var elem = test[..3];
                var letterMatch = LetterRegex.Match(elem);
                if (letterMatch.Success)
                {
                    sublist.Add(letterMatch.Groups["letter"].Value);
                }

                if (test.Length > 3)
                {
                    test = test[4..];
                    sublistIndex++;
                }
                else
                {
                    test = "";
                    sublistIndex = 0;
                }
            } while (test.Length > 0);
        }

        return grid;
    }

    private List<List<string>> EstablishGridDimensions(string line)
    {
        var list = new List<List<string>>();
        do
        {
            var elem = line[..3];
            var letterMatch = LetterRegex.Match(elem);
            var sublist = new List<string>();
            list.Add(sublist);

            if (letterMatch.Success)
            {
                sublist.Add(letterMatch.Groups["letter"].Value);
            }
            line = line.Length > 3 ? line[4..] : "";

        } while (line.Length > 0);

        return list;
    }

    private class Operation
    {
        private static readonly Regex OperationRegex = new(@"move (?<amount>\d+) from (?<from>\d+) to (?<to>\d+)");

        public Operation(string line)
        {
            var match = OperationRegex.Match(line);
            if (match.Success)
            {
                Amount = Convert.ToInt32(match.Groups["amount"].Value);
                From = Convert.ToInt32(match.Groups["from"].Value);
                To = Convert.ToInt32(match.Groups["to"].Value);
            }
        }

        public int Amount { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}