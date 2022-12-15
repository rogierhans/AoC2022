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
        List<(int, int)> EndPoints = new List<(int, int)>();
        HashSet<int> ys = new HashSet<int>();
        foreach (var line in NumberedRows)
        {
            int scannerX = line[0];
            int scannerY = line[1];
            int BaconX = line[2];
            int BaconY = line[3];

            int dx = Math.Abs(BaconX - scannerX);
            int dy = Math.Abs(BaconY - scannerY);

            int length = dx + dy;
            EndPoints.Add((scannerX, scannerY + length));
            EndPoints.Add((scannerX, scannerY - length));
        }
        for (int i = 0; i < EndPoints.Count; i++)
        {
            for (int j = i + 1; j < EndPoints.Count; j++)
            {
                var (x1, y1) = EndPoints[i];
                var (x2, y2) = EndPoints[j];
                ys.Add(((y1 + y2) / 2) + ((x1 - x2)/2));
                ys.Add(((y1 + y2) / 2) + ((x2 - x1) / 2));
            }
        }
        ys.Where(x => x >= 0 && x <= 4000000).Count().P();
       foreach (int target in ys.Where(x => x >= 0 && x <= 4000000))
        //for (int target = 0; target < 4000000; target++)
        {
            //target.P();
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
                        int delta = Math.Abs(y + length - target);
                        Ranges.Add((x - delta, x + delta));
                    }
                }
                else
                {
                    if (y - length <= target)
                    {
                        int delta = Math.Abs(target - (y - length));
                        Ranges.Add((x - delta, x + delta));
                    }
                }
            }
            Ranges = Ranges.Select(x => (Math.Max(0, x.Item1), Math.Min(maxSearch, x.Item2))).ToList();
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
                    Console.WriteLine(target + " " + (x * maxSearch + target));
                    break;
                }
            }

        }
        Console.ReadLine();
    }


}
