using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AOC2021;


class Day2 : Day
{
    public Day2()
    {
        GetInput(RootFolder + @"2021_02\");
    }


    public override void Part1(List<string> Lines)
    {
        long depth = 0;
        long horizontal = 0;
        for (int i = 0; i < Lines.Count; i++)
        {
            var line = Lines[i];
            var input = line.Split(' ');
            var value = long.Parse(input[1]);
            if (input[0] == "forward")
            {
                horizontal += value;
            }
            if (input[0] == "down")
            {
                depth += value;
            }
            if (input[0] == "up")
            {
                depth -= value;
            }
        }
      PrintSolution((depth * horizontal).ToString(), "1427868", "part 1");
    }
    public override void Part2(List<string> Lines)
    {
        long aim = 0;
        long depth = 0;
        long horizontal = 0;
        for (int i = 0; i < Lines.Count; i++)
        {
            var line = Lines[i];
            var input = line.Split(' ');
            var value = long.Parse(input[1]);
            if (input[0] == "forward")
            {
                horizontal += value;
                depth += aim * value;
            }
            if (input[0] == "down")
            {
                aim += value;
            }
            if (input[0] == "up")
            {
                aim -= value;
            }
        }
      PrintSolution((depth * horizontal).ToString(), "1568138742", "part 2");
    }
}

