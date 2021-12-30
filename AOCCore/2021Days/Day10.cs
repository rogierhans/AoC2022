using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2021;



//class Day4 : Day
//{
//    public Day4()
//    {
//        GetInput(RootFolder + @"2021_03\");
//    }


//    public override string Part1(List<string> Lines)
//    {

//        return PrintSolution("", "2035764", "part 1");
//    }
//    public override string Part2(List<string> Lines)
//    {

//        return PrintSolution("", "2817661", "part 2");
//    }
//}


class Day10 : Day
{
    public Day10()
    {
        GetInput(RootFolder + @"2021_10\");
    }


    public override string Part1(List<string> Lines)
    {
        List<long> scores = new();
        var score = new Dictionary<string, long> { [")"] = 3, ["]"] = 57, ["}"] = 1197, [">"] = 25137 };
        long count = 0;
        var validCombinations = new List<(string, string)> { ("(", ")"), ("{", "}"), ("[", "]"), ("<", ">") };

        for (int t = 0; t < Lines.Count; t++)
        {
            var input = Lines[t].List();
            Stack<string> stack = new();
            for (int i = 0; i < input.Count; i++)
            {
                var letter = input[i];
                if (new List<string>() { "(", "{", "[", "<" }.Contains(letter))
                {
                    stack.Push(letter);
                }
                if (new List<string>() { ")", "}", "]", ">" }.Contains(letter))
                {
                    var otherLetter = stack.Pop();
                    var pair = (otherLetter, letter);
                    if (!validCombinations.Contains(pair)) {
                        count += score[letter];
                        break;
                    }
                }
            }

        }
        return PrintSolution(count, "193275", "part 1");
    }
    public override string Part2(List<string> Lines)
    {
        List<long> scores = new();
        var score = new Dictionary<string, long> { ["("] = 1, ["["] = 2, ["{"] = 3, ["<"] = 4 };
        List<long> numbers = new();
        var validCombinations = new List<(string, string)> { ("(", ")"), ("{", "}"), ("[", "]"), ("<", ">") };

        for (int t = 0; t < Lines.Count; t++)
        {
            var input = Lines[t].List();
            Stack<string> stack = new();
            bool skip = false;
            for (int i = 0; i < input.Count; i++)
            {
                var letter = input[i];
                if (new List<string>() { "(", "{", "[", "<" }.Contains(letter))
                {
                    stack.Push(letter);
                }
                if (new List<string>() { ")", "}", "]", ">" }.Contains(letter))
                {
                    var otherLetter = stack.Pop();
                    var pair = (otherLetter, letter);
                    if (!validCombinations.Contains(pair)) skip = true;
                }
            }
            long count = stack.Aggregate((long)0, (a, b) => a * 5 + score[b]);
            if (!skip)
                scores.Add(count);

        }
        var middle = scores.OrderBy(x => x).ToList()[scores.Count / 2];
        return PrintSolution(middle, "2429644557", "part 2");
    }
}

