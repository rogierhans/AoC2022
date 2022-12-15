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
        var segments = Lines.Select(x => x.Split("->").Select(y => (int.Parse(y.Split(",")[0]), int.Parse(y.Split(",")[1]))).ToList()).ToList();
        var sandDictionary = new Dictionary<(int, int), int>();
        foreach (var segment in segments)
        {
            var (fromX, fromY) = segment.First();
            foreach (var (X, Y) in segment.Skip(1))
            {
                if (X == fromX)
                {
                    for (int i = 0; i <= Math.Abs(fromY - Y); i++)
                    {
                        sandDictionary[(X, Math.Min(fromY, Y) + i)] = 0;
                    }
                }
                else
                {
                    for (int i = 0; i <= Math.Abs(fromX - X); i++)
                    {
                        sandDictionary[((Math.Min(fromX, X) + i), Y)] = 0;
                    }
                }
                (fromX, fromY) = (X, Y);
            }
        }
        Queue<(int, int)> SandQueue = new Queue<(int, int)>();
        SandQueue.Enqueue((500, 0));
        sandDictionary[(500, 0)] = 1;
        while (SandQueue.Count > 0)
        {
            var (X    , Y) = SandQueue.Dequeue();
            Add((X    , Y + 1));
            Add((X - 1, Y + 1));
            Add((X + 1, Y + 1));

            void Add((int,int) PotentialSandGrain){
                if (!sandDictionary.ContainsKey(PotentialSandGrain))
                {
                    sandDictionary[PotentialSandGrain] = 1;
                    SandQueue.Enqueue(PotentialSandGrain);
                }
            }
        }
        sandDictionary.Values.Sum(x => x).P();
    }



}
