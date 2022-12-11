using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2021;

class Day1 : Day
{
    public Day1()
    {
        GetInput(RootFolder + @"2021_01\");
    }


    public override void Part1(List<string> lines)
    {
        var input = lines.Select(x => double.Parse(x)).ToList();
        int count = 0;
        for (int i = 0; i < input.Count - 1; i++)
        {
            if (input[i] < input[i + 1]) count++;
        }
      PrintSolution(count.ToString(), "1557", "part 1");
    }
    public override void Part2(List<string> lines)
    {
        var input = lines.Select(x => double.Parse(x)).ToList();
        int count = 0;
        for (int i = 0; i < input.Count - 3; i++)
        {
            if (input[i] < input[i + 3]) count++;
        }
      PrintSolution(count.ToString(), "1608", "part 2");
    }
}
