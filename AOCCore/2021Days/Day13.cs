using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2021;


//class Day11 : Day
//{
//    public Day11()
//    {
//        GetInput(RootFolder + @"2021_11\");
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
class Day13 : Day
{
    public Day13()
    {
        GetInput(RootFolder + @"2021_13\");
    }


    Dictionary<string, string> dict = new Dictionary<string, string>()
    {
        ["011001001010010111101001010010"] = "A",
        ["111001001011100100101001011100"] = "B",
        ["011001001010000100001001001100"] = "C",
        ["111101000011100100001000011110"] = "E",
        ["111101000011100100001000010000"] = "F",
        ["011001001010000101101001001110"] = "G",
        ["100101001011110100101001010010"] = "H",
        ["001100001000010000101001001100"] = "J",
        ["100101010011000101001010010010"] = "K",
        ["100001000010000100001000011110"] = "L",
        ["111001001010010111001000010000"] = "P",
        ["111001001010010111001010010010"] = "R",
        ["100101001010010100101001001100"] = "U",
        ["100011000101010001000010000100"] = "Y",
        ["111100001000100010001000011110"] = "Z",
        ["000000000000000000000000000000"] = " ",
    };

    public override string Part1(List<string> Lines)
    {
        var coords = Lines.FindPatterns("{0},{1}", int.Parse, int.Parse);
        var folds = Lines.FindPatterns("fold along {0}={1}", x => x == "x", int.Parse);
        var (hFlip, index) = folds.First();
        HashSet<string> seen = new HashSet<string>();
        foreach (var (x, y) in coords)
        {
            int newX = hFlip && x > index ? index - (x - index) : x;
            int newY = !hFlip && y > index ? index - (y - index) : y;
            seen.Add((newX + "_" + newY));
        }
        return PrintSolution(seen.Count, "610", "part 1");
    }
    public override string Part2(List<string> Lines)
    {
        var coords = Lines.FindPatterns("{0},{1}", int.Parse, int.Parse);
        var folds = Lines.FindPatterns("fold along {0}={1}", x => x == "x", int.Parse);
        foreach (var (hFlip, index) in folds)
        {
            var newCoords = new List<(int, int)>();
            foreach (var (x, y) in coords)
            {
                int newX = hFlip && x > index ? index - (x - index) : x;
                int newY = !hFlip && y > index ? index - (y - index) : y;
                newCoords.Add((newX, newY));
            }
            coords = newCoords;
        }
        string word = "";
        for (int letter = 0; letter < 8; letter++)
        {
            int offSet = letter * 5;
            string line = "";
            for (int row = 0; row < 6; row++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int column = j + offSet;
                    bool exits = false;
                    foreach (var coord in coords)
                    {
                        exits |= coord.Item1 == column && coord.Item2 == row;   

                    }
                    line += exits ? "1" : "0";
                }
            }
            word += dict[line];
        }

        return PrintSolution(word, "PZFJHRFZ", "part 2");
    }
}
