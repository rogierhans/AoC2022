using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Gurobi;

namespace AOC2021;



class Day7 : Day
{
    public Day7()
    {
        GetInput(RootFolder + @"2021_07\");
    }

    public override string Part1(List<string> Lines)
    {
        var numbers = Lines.First().Split(',').Select(x => long.Parse(x)).ToList();
        var bestFuel = SL.GetNumbers(numbers.Min(), numbers.Max()).Min(midpoint => numbers.Sum(x => Math.Abs(midpoint - x)));
        return PrintSolution(bestFuel, "336701", "part 1");
    }
    public override string Part2(List<string> Lines)
    {
        var numbers = Lines.First().Split(',').Select(x => long.Parse(x)).ToList();
        var mem = new Mem<long, long>();
        var bestFuel = SL.GetNumbers(numbers.Min(), numbers.Max()).Min(midpoint => numbers.Sum(x => mem.F(distance => SL.GetNumbers(0, distance).Sum() + distance, Math.Abs(midpoint - x))));
        return PrintSolution(bestFuel, "95167302", "part 2");
    }
}

