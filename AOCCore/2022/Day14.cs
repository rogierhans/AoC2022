//using Priority_Queue;
using Microsoft.FSharp.Core;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AOCCore._2022.Day13;
using static System.Reflection.Metadata.BlobBuilder;

namespace AOCCore._2022;

class Day14 : Day
{
    public Day14()
    {
        GetInput("2022", "14");

    }

    public override void Part2(List<string> Lines)
    {

        TryParse(Lines);
        var segments = Lines.Select(x => x.Split("->").Select(y => (int.Parse(y.Split(",")[0]), int.Parse(y.Split(",")[1]))).ToList()).ToList();
        var sandDictionary = new Dictionary<(int, int), int>();
        foreach (var segment in segments)
        {
            var (fromX, fromY) = segment.First();
            foreach (var (X, Y) in segment.Skip(1))
            {
                if (X == fromX)
                {
                    for (int i = 0; i <= Math.Max(fromY - Y, Y - fromY); i++)
                    {
                        sandDictionary[(X, Math.Min(fromY, Y) + i)] = 0;
                    }
                }
                else
                {
                    for (int i = 0; i <= Math.Max(fromX - X, X - fromX); i++)
                    {
                        sandDictionary[((Math.Min(fromX, X) + i),Y)] = 0;
                    }
                }
                (fromX, fromY) = (X, Y);
            }
        }
        int counter = 0;

        Stack<(int, int)> SandPath = new Stack<(int, int)>();
        SandPath.Push((500, 0));
        while (SandPath.Count > 0)
        {
            var sandGrain = SandPath.Peek(); ;
            counter++;
            var (X, Y) = sandGrain;
            sandDictionary[sandGrain] = 1;
            var DownSand = (X, Y + 1);
            var LeftSand = (X - 1, Y + 1);
            var RightSand = (X + 1, Y + 1);
            if (!sandDictionary.ContainsKey(DownSand))
            {
                sandGrain = DownSand;
                SandPath.Push(sandGrain);
            }
            else if (!sandDictionary.ContainsKey(LeftSand))
            {
                sandGrain = LeftSand;
                SandPath.Push(sandGrain);
            }
            else if (!sandDictionary.ContainsKey(RightSand))
            {
                sandGrain = RightSand;
                SandPath.Push(sandGrain);
            }
            else
            {
                SandPath.Pop();
            }
        }
        sandDictionary.Values.Sum(x => x).P();

        //Console.ReadLine();
    }



}
