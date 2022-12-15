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
        List<(int, int, int, int)> Segments = new List<(int, int, int, int)>();
        HashSet<int> ys = new HashSet<int>();
        foreach (var line in NumberedRows)
        {
            int x = line[0];
            int y = line[1];
            int Bx = line[2];
            int By = line[3];

            int dx = Math.Abs(Bx - x);
            int dy = Math.Abs(By - y);

            int length = dx + dy;
            var points = new List<(int, int)>() { (x, y + length), (x - length, y), (x, y - length), (x + length, y) };
            for (int i = 0; i < points.Count; i++)
            {
                var (x1, y1) = points[i];
                var (x2, y2) = points[(i + 1) % points.Count];
                Segments.Add((x1, y1, x2, y2));
                ys.Add(y1 - 1);
                ys.Add(y1);
                ys.Add(y1 + 1);
                ys.Add(y2 - 1);
                ys.Add(y2);
                ys.Add(y2 + 1);
            }
        }
        for (int i = 0; i < Segments.Count; i++)
        {
            for (int j = i + 1; j < Segments.Count; j++)
            {
                int y = Intersect(Segments[i], Segments[j]);
                ys.Add(y - 1);
                ys.Add(y);
                ys.Add(y + 1);
            }
        }
        ys.Where(x => x >= 0 && x <= 4000000).OrderBy(x => x).ToList().Print(" ");
        int Intersect((int, int, int, int) linesegment1, (int, int, int, int) linesegment2)
        {
            var (startX1, startY1, endX1, endY1) = linesegment1;
            var (startX2, startY2, endX2, endY2) = linesegment2;

            // Calculate the coefficients of the two lines
            double a1 = startY1 - endY1;
            double b1 = endX1 - startX1;
            double c1 = startX1 * endY1 - endX1 * startY1;

            double a2 = startY2 - endY2;
            double b2 = endX2 - startX2;
            double c2 = startX2 * endY2 - endX2 * startY2;

            // Calculate the intersection point
            double determinant = a1 * b2 - a2 * b1;

            if (determinant == 0)
            {
                // The lines are parallel
                return 0;
            }
            else
            {
                double x = (b2 * c1 - b1 * c2) / determinant;
                double y = (a1 * c2 - a2 * c1) / determinant;
                Console.WriteLine(x + y);
                // Return the coordinates of the intersection point as an integer tuple
                return  (int)y ;
            }
        }
        //Segments.Print("\n");
        Console.ReadLine();
        //foreach (int target in ys.Where(x => x >= 0 && x <= 4000000))
            for (int target = 0; target < 4000000; target++)
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
                    Console.WriteLine(target+" "+(x * maxSearch + target));
                    break;
                }
            }

        }
        Console.ReadLine();
    }


}
