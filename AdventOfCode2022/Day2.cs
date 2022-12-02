namespace AdventOfCode2022;

public class Day2
{
    public void DoWork()
    {
        var lines = File.ReadAllLines("./d2.txt");

        var rock = new Rock();
        var paper = new Paper();
        var scissors = new Scissors();

        var pointsDay01 = 0;
        var pointsDay02 = 0;

        foreach (var line in lines)
        {
            var splt = line.Split(" ");
            IRps rival;
            switch (splt[0])
            {
                case "A":
                    rival = rock;
                    break;
                case "B":
                    rival = paper;
                    break;
                case "C":
                    rival = scissors;
                    break;
                default:
                    throw new Exception("NO CHEATING");
            }

            var meOnDay01 = GetMyInputForDay01(splt[1], rock, paper, scissors);
            var meOnDay02 = GetMyInputForDay02(splt[1],splt[0], rock, paper, scissors);
            
            pointsDay01 += meOnDay01.DecideMatchPoints(rival);
            pointsDay02 += meOnDay02.DecideMatchPoints(rival);
        }
        
        Console.WriteLine("\r\n----- Day 02 -----");
        Console.WriteLine($"Solution 01: {pointsDay01}");
        Console.WriteLine($"Solution 02: {pointsDay02}");
    }

    private IRps GetMyInputForDay01(string decider, IRps rock, IRps paper, IRps scissors)
    {
        switch (decider)
        {
            case "X":
                return rock;
            case "Y":
                return paper;
            case "Z":
                return scissors;
            default:
                throw new Exception("NO CHEATING");
        }
    }
    private IRps GetMyInputForDay02(string me, string rival, IRps rock, IRps paper, IRps scissors)
    {
        switch (rival)
        {
            case "A":
                switch (me)
                {
                    case "X": return scissors;
                    case "Y": return rock;
                    case "Z": return paper;
                }
                break;
            case "B":
                switch (me)
                {
                    case "X": return rock;
                    case "Y": return paper;
                    case "Z": return scissors;
                }
                break;
            case "C":
                switch (me)
                {
                    case "X": return paper;
                    case "Y": return scissors;
                    case "Z": return rock;
                }
                break;
        }

        throw new Exception("NO CHEATING");
    }
    
    private interface IRps
    {
        int Points { get; }

        public int DecideMatchPoints(IRps rival);

        bool WinsOver(IRps rival);
        bool LosesTo(IRps rival);
    }

    private abstract class RpsBase : IRps
    {
        public abstract int Points { get; }

        public virtual int DecideMatchPoints(IRps rival)
        {
            if (WinsOver(rival)) return Points + 6;
            if (LosesTo(rival) && !WinsOver(rival)) return Points + 0;
            if (!WinsOver(rival) && !LosesTo(rival)) return Points + 3;
            throw new Exception("NO CHEATING");
        }

        public abstract bool WinsOver(IRps rival);

        public abstract bool LosesTo(IRps rival);
    }

    private class Scissors: RpsBase
    {
        public override int Points => 3;
        
        public override bool WinsOver(IRps rival)
        {
            switch (rival)
            {
                case Rock:
                    return false;
                case Paper:
                    return true;
                case Scissors:
                    return false;
                default:
                    throw new Exception("NO CHEATING!");
            }
        }

        public override bool LosesTo(IRps rival)
        {
            switch (rival)
            {
                case Rock:
                    return true;
                case Paper:
                    return false;
                case Scissors:
                    return false;
                default:
                    throw new Exception("NO CHEATING!");
            }
        }
    }

    private class Paper : RpsBase
    {
        public override int Points => 2;

        public override bool WinsOver(IRps rival)
        {
            switch (rival)
            {
                case Rock:
                    return true;
                case Paper:
                    return false;
                case Scissors:
                    return false;
                default:
                    throw new Exception("NO CHEATING!");
            }
        }

        public override bool LosesTo(IRps rival)
        {
            switch (rival)
            {
                case Rock:
                    return false;
                case Paper:
                    return false;
                case Scissors:
                    return true;
                default:
                    throw new Exception("NO CHEATING!");
            }
        }
    }

    private class Rock: RpsBase
    {
        public override int Points => 1;
        
        public override bool WinsOver(IRps rival)
        {
            switch (rival)
            {
                case Rock:
                    return false;
                case Paper:
                    return false;
                case Scissors:
                    return true;
                default:
                    throw new Exception("NO CHEATING!");
            }
        }

        public override bool LosesTo(IRps rival)
        {
            switch (rival)
            {
                case Rock:
                    return false;
                case Paper:
                    return true;
                case Scissors:
                    return false;
                default:
                    throw new Exception("NO CHEATING!");
            }
        }
    }
}