//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using System.Diagnostics;

//namespace AOC2021;



//class Day7 : Day
//{
//    public Day7()
//    {
//        GetInput(RootFolder + @"2021_07\");
//    }

//    public override void Part1(List<string> Lines)
//    {
//        var numbers = Lines.First().Split(',').Select(x => long.Parse(x)).OrderBy(x => x).ToList() ;
//        var midpoint = numbers[numbers.Count / 2];
//        var bestFuel = numbers.Sum(x => Math.Abs(midpoint - x));
//      PrintSolution(bestFuel, "336701", "part 1");
//    }
//    public override void Part2(List<string> Lines)
//    {
//        var numbers = Lines.First().Split(',').Select(x => long.Parse(x)).ToList();
//        var mem = new Mem<long, long>();
//        var bestFuel = SL.GetNumbers((int)numbers.Average()-1, (int)numbers.Average() +1 ).Min(midpoint => numbers.Sum(x => mem.F(distance => SL.GetNumbers(0, distance).Sum() + distance, Math.Abs(midpoint - x))));
//      PrintSolution(bestFuel, "95167302", "part 2");
//    }
//}

