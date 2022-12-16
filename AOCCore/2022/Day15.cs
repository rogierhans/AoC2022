//using Priority_Queue;
using Microsoft.FSharp.Core;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AOC2021.Day19Alt;
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
        var sw = new Stopwatch();
        sw.Start();
        //TryParse(Lines);
        var Diamonds = Lines.Select(line => GetNumbers(line)).Select(x => (x[0], x[1], Math.Abs(x[0] - x[2]) + Math.Abs(x[1] - x[3]))).ToList();
        int maxSearch = 4000000;
        List<(int, int, int, int)> EndPoints = new List<(int, int, int, int)>();

        foreach (var (scannerX, scannerY, length) in Diamonds)
        {
            var TopPointDiamond = (scannerX, scannerY + length, scannerY + length, scannerY - length);
            var BottomPointDiamond = (scannerX, scannerY - length, scannerY + length, scannerY - length);
            EndPoints.Add(TopPointDiamond);
            EndPoints.Add(BottomPointDiamond);
        }
        HashSet<double> ys = new HashSet<double>();
        for (int i = 0; i < EndPoints.Count; i++)
        {
            for (int j = i + 1; j < EndPoints.Count; j++)
            {
                var (x1, y1, t1, b1) = EndPoints[i];
                var (x2, y2, t2, b2) = EndPoints[j];
                double firstIntersect = Math.Floor((y1 + y2 + x1 - x2) / (double)2);
                double secondIntersect = Math.Floor((y1 + y2 + x2 - x1) / (double)2);
                if (firstIntersect - Math.Floor(firstIntersect) == 0)
                {
                    AddMaybeY(Math.Ceiling(firstIntersect));
                }
                if (secondIntersect - Math.Floor(secondIntersect) == 0)
                {
                    AddMaybeY(Math.Ceiling(secondIntersect));
                }
                void AddMaybeY(double potentialY)
                {
                    if (y1 <= potentialY && potentialY <= y2 &&
                        b1 <= potentialY && potentialY <= t1 &&
                        b2 <= potentialY && potentialY <= t2)
                        ys.Add(potentialY);
                }
            }
        }
        foreach (int target in ys)
        {
            if (!(target >= 0 && target <= 4000000)) continue;
            List<(int, int)> Ranges = new List<(int, int)>();
            foreach (var (x, y, length) in Diamonds)
            {
                if (y < target && y + length >= target)
                {

                    int delta = Math.Abs((y + length) - target);
                    Ranges.Add((x - delta, x + delta));

                }
                else if (y - length <= target && y >= target)
                {
                    int delta = Math.Abs(target - (y - length));
                    Ranges.Add((x - delta, x + delta));
                }

            }
            // Ranges = Ranges.Select(x => (Math.Max(0, x.Item1), Math.Min(maxSearch, x.Item2))).ToList();
            Ranges = Ranges.OrderBy(x => x).ToList();
            var currentPoint = Ranges[0];
            for (int j = 1; j < Ranges.Count; j++)
            {
                var (from1, to1) = currentPoint;
                {
                    var (from2, to2) = Ranges[j];
                    if (from2 <= from1 && from1 <= to2 ||
                        from2 <= to1 && to1 <= to2 ||
                        from1 <= from2 && from2 <= to1 ||
                        from1 <= to2 && to2 <= to1)
                    {
                        currentPoint = (from1, Math.Max(to1, to2));
                    }
                    else
                    {
                        long x = currentPoint.Item2 + 1;
                        var number = x * maxSearch + target;
                        if (number != 13071206703981)
                        {
                            Console.WriteLine(number - 13071206703981);
                            Console.ReadLine();
                        }
                        Console.WriteLine(target + " " + " " + number);
                        sw.Stop();
                        Console.WriteLine(sw.Elapsed.TotalMilliseconds);
                        return;
                    }
                }
            }
        }

    }


}
