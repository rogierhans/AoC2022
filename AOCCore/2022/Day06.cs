using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore._2022;

class Day06 : Day
{
    public Day06()
    {
        GetInput("2022", "06");

    }

    public override void Part1(List<string> Lines)
    {
        int range = 4;
        foreach (var line in Lines)

            for (int i = 0; i < line.Length - range + 1; i++)
            {
                if (line.Substring(i, range).List().ToHashSet().Count() >= range)
                {
                    Console.WriteLine(i + range);
                    break;
                }
            }
    }
    public override void Part2(List<string> Lines)
    {
        int range = 14;
        foreach (var line in Lines)

            for (int i = 0; i < line.Length - range + 1; i++)
            {
                if (line.Substring(i, range).List().ToHashSet().Count() >= range)
                {
                    Console.WriteLine(i + range);
                    break;
                }
            }
    }
}
