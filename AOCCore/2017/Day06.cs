using Google.OrTools.LinearSolver;
using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AOCCore._2017;

class Day06 : Day
{
    public Day06()
    {
        GetInput("2017", "06");

    }

    public override void Part1(List<string> Lines)
    {
        var numbers = Lines.First().Split("\t").ToList();
        int steps = 0;
        Dictionary<string, int> set = new Dictionary<string, int>();
        int[] ints = new int[numbers.Count];
        for (int i = 0; i < numbers.Count; i++)
        {
            ints[i] = int.Parse(numbers[i]);

        }
        while (true)
        {
            steps++;
            int max = ints.Max();

            for (int index = 0; index < numbers.Count; index++)
            {
                if (ints[index] == max)
                {
                    int number = ints[index];
                    ints[index] = 0;

                    for (int j = 0; j < number; j++)
                    {
                        index = (1 + index) % numbers.Count;
                        ints[index]++;
                    }
                    string key = string.Join("_", ints);
                    if (set.ContainsKey(key))
                    {

                        Console.WriteLine(set[key] - steps);
                        Console.WriteLine("");
                    }
                    set[key] = steps;
                    break;
                }
            }
        }

    }
}

