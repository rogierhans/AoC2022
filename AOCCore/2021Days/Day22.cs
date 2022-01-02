using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Day22 : Day
{
    public Day22()
    {
        GetInput(RootFolder + @"2021_22\");
    }


    public override string Part1(List<string> Lines)
    {
        var tuples = Lines.FindPatterns("{0} x={1},y={2},z={3}", x => x, x => x, x => x, x => x);
        var Areas = tuples.Select(x => new CubeThing(x)).ToList();
        List<CubeThing> currentAreas = new List<CubeThing>() { };
        foreach (var area in Areas.Where(a => Math.Abs(a.xmin) <= 50 && Math.Abs(a.xmax) <= 50 && Math.Abs(a.ymin) <= 50 && Math.Abs(a.zmin) <= 50 && Math.Abs(a.ymax) <= 50 && Math.Abs(a.zmax) <= 50))
        {
            var newAres = new List<CubeThing>();
            CubeThing current = area;
            foreach (var old in currentAreas)
            {
                old.Intersect(current, newAres);

            }
            if (current.Counter == 0)
            {
                newAres.Add(current);
            }
            newAres.AddRange(currentAreas);
            currentAreas = newAres;
        }
        return PrintSolution(currentAreas.Where(x => x.Counter % 2 == 0).Sum(x => x.area) - currentAreas.Where(x => x.Counter % 2 == 1).Sum(x => x.area), "527915", "part 1");
    }
    private static Object someListLock = new Object(); // only once


    public override string Part2(List<string> Lines)
    {
        var tuples = Lines.FindPatterns("{0} x={1},y={2},z={3}", x => x, x => x, x => x, x => x);
        var Areas = tuples.Select(x => new CubeThing(x)).ToList();
        List<CubeThing> currentAreas = new List<CubeThing>() { };

        foreach (var area in Areas)//.Where(a => Math.Abs(a.xmin) <= 50 && Math.Abs(a.xmax) <= 50 && Math.Abs(a.ymin) <= 50 && Math.Abs(a.zmin) <= 50 && Math.Abs(a.ymax) <= 50 && Math.Abs(a.zmax) <= 50))
        {
            var newAres = new List<CubeThing>();
            CubeThing current = area;

            Parallel.ForEach(currentAreas, old =>
                        {
                            old.Intersect(current, newAres);
                        });
            if (current.Counter == 0)
            {
                newAres.Add(current);
            }
            newAres.AddRange(currentAreas);
            currentAreas = newAres;
        }
        return PrintSolution(currentAreas.Where(x => x.Counter % 2 == 0).Sum(x => x.area) - currentAreas.Where(x => x.Counter % 2 == 1).Sum(x => x.area), "1218645427221987", "part 2");
    }
    public class CubeThing
    {
        public int Counter = 0;
        public long xmin, ymin, xmax, ymax, zmin, zmax;
        public long area;

        public CubeThing((string on, string x, string y, string z) tuple)
        {
            var On = tuple.on == "on";
            (xmin, xmax) = tuple.x.Pattern("{0}..{1}", long.Parse, long.Parse);
            (ymin, ymax) = tuple.y.Pattern("{0}..{1}", long.Parse, long.Parse);
            (zmin, zmax) = tuple.z.Pattern("{0}..{1}", long.Parse, long.Parse);
            area = (xmax - xmin + 1) * (ymax - ymin + 1) * (zmax - zmin + 1);
            if (On) Counter = 0;
            else Counter = 1;
        }
        public CubeThing(long xmin, long xmax, long ymin, long ymax, long zmin, long zmax, int counter)
        {
            this.xmin = xmin; this.ymin = ymin; this.xmax = xmax; this.ymax = ymax; this.zmin = zmin; this.zmax = zmax;
            Counter = counter;
            area = (xmax - xmin + 1) * (ymax - ymin + 1) * (zmax - zmin + 1);
        }


        public bool NonEmpty()
        {
            bool valid = xmax - xmin >= 0 && ymax - ymin >= 0 && zmax - zmin >= 0;
            return valid;
        }
        public void Intersect(CubeThing otherCube, List<CubeThing> newArreas)
        {
            long newxmin = Math.Max(xmin, otherCube.xmin);
            long newymin = Math.Max(ymin, otherCube.ymin);
            long newzmin = Math.Max(zmin, otherCube.zmin);

            long newxmax = Math.Min(xmax, otherCube.xmax);
            long newymax = Math.Min(ymax, otherCube.ymax);
            long newzmax = Math.Min(zmax, otherCube.zmax);


            if (newxmax - newxmin >= 0 && newymax - newymin >= 0 && newzmax - newzmin >= 0)
            {
                lock (someListLock)
                    newArreas.Add(new CubeThing(newxmin, newxmax, newymin, newymax, newzmin, newzmax, Counter + 1));
            }
        }
    }

}


