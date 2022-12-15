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

class Day15 : Day
{
    public Day15()
    {
        GetInput("2022", "15");

    }
    public override void Part2(List<string> Lines)
    {
        TryParse(Lines);
        int maxSearch = 4000000;
        for (int target = 0; target < maxSearch; target++)
        {
            List<(int, int)> Ranges = new List<(int, int)>();
            foreach (var line in NumberedRows)
            {
                int x = line[0];
                int y = line[1];
                int Bx = line[2];
                int By = line[3];

                int dx = Math.Abs(Bx - x);
                int dy = Math.Abs(By - y);

                int length = dx + dy;
                if (y < target)
                {
                    if (y + length >= target)
                    {
                        int delta = y + length - target;
                        Ranges.Add((x - delta, x + delta));
                    }
                }
                else
                {
                    if (y - length <= target)
                    {
                        int delta = target - (y - length);
                        Ranges.Add((x - delta, x + delta));
                    }
                }
            }
            Ranges = Ranges.Select(x => (Math.Max(0, x.Item1), Math.Min(maxSearch, x.Item2))).ToList();
            int sum = 0;
            var currentRange = Ranges.First();
            while (Ranges.Count > 1)
            {
                // Ranges.Count.P();
                bool changed = false;
                for (int i = 0; i < Ranges.Count; i++)
                {
                    for (int j = i + 1; j < Ranges.Count; j++)
                    {
                        var (from1, to1) = Ranges[i];
                        var (from2, to2) = Ranges[j];
                        if (from2 <= from1 && from1 <= to2 ||
                            from2 <= to1 && to1 <= to2 ||
                            from1 <= from2 && from2 <= to1 ||
                            from1 <= to2 && to2 <= to1)
                        {
                            Ranges.RemoveAt(j);
                            Ranges.RemoveAt(i);
                            Ranges.Add((Math.Min(from1, from2), Math.Max(to1, to2)));
                            changed = true;
                            continue;
                        }
                    }
                }
                if (!changed)
                {
                    long x = Ranges.Where(x => x.Item1 != 0).First().Item1 + 1;
                    Console.WriteLine(x * maxSearch + target);
                    break;
                }
            }

        }
        Console.ReadLine();
    }


}
