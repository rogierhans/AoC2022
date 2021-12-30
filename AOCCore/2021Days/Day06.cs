using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2021;

class Day6 : Day
{
    public Day6()
    {
        GetInput(RootFolder + @"2021_06\");
    }


    public override string Part1(List<string> Lines)
    {
        return PrintSolution(RunFishModel(Lines, 80), "390011", "part 2");
    }
    public override string Part2(List<string> Lines)
    {

        return PrintSolution(RunFishModel(Lines,256), "1746710169834", "part 2");
    }
    private static long RunFishModel(List<string> Lines, int maxIterations)
    {
        var numbers = Lines.First().Split(',').Select(x => long.Parse(x));
        long[] fishCount = new long[9];
        foreach (var number in numbers)
        {
            fishCount[number]++;
        }
        for (int k = 0; k < maxIterations;)
        {
            fishCount[(7 + k) % 9] += fishCount[k % 9];
            k++;
        }
        return fishCount.Sum();
    }
}

