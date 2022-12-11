using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2021;



class Day5 : Day
{
    public Day5()
    {
        GetInput(RootFolder + @"2021_05\");
    }


    public override void Part1(List<string> Lines)
    {

        int gridsize = 1000;
        int[,] array = new int[gridsize, gridsize];
        for (int t = 0; t < Lines.Count; t++)
        {

            var line = Lines[t];
            var (x1, y1, x2, y2) = line.Pattern("{0},{1}->{2},{3}", int.Parse, int.Parse, int.Parse, int.Parse);

            int minx = Math.Min(x1, x2);
            int miny = Math.Min(y1, y2);

            int maxX = Math.Max(x1, x2);
            int maxY = Math.Max(y1, y2);
            if (x1 == x2 || y1 == y2)
            {
                for (int x = minx; x <= maxX; x++)
                {
                    for (int y = miny; y <= maxY; y++)
                    {
                        array[x, y]++;
                    }
                }
            }
            for (int x = 0; x < 10; x++)
            {
                string strLine = "";
                for (int y = 0; y < 10; y++)
                {
                    strLine += array[x, y];
                }
            }
        }

        int count = 0;
        for (int x = 0; x < gridsize; x++)
        {
            for (int y = 0; y < gridsize; y++)
            {
                if (array[x, y] >= 2) count++;
            }

        }

      PrintSolution(count, "7644", "part 1");
    }
    public override void Part2(List<string> Lines)
    {

        int gridsize = 1000;
        int[,] array = new int[gridsize, gridsize];
        for (int t = 0; t < Lines.Count; t++)
        {

            var line = Lines[t];
            var (x1, y1, x2, y2) = line.Pattern("{0},{1}->{2},{3}", int.Parse, int.Parse, int.Parse, int.Parse);

            int minx = Math.Min(x1, x2);
            int miny = Math.Min(y1, y2);

            int maxX = Math.Max(x1, x2);
            int maxY = Math.Max(y1, y2);
            if (x1 == x2 || y1 == y2)
            {
                for (int x = minx; x <= maxX; x++)
                {
                    for (int y = miny; y <= maxY; y++)
                    {
                        array[x, y]++;
                    }
                }
            }
            else
            {
                if (x1 < x2 && y1 < y2)
                {
                    for (int i = 0; i <= x2 - x1; i++)
                    {
                        array[x1 + i, y1 + i]++;
                    }
                }
                else if (x1 < x2 && y1 > y2)
                {
                    for (int i = 0; i <= x2 - x1; i++)
                    {
                        array[x1 + i, y1 - i]++;
                    }
                }
                else if (x1 > x2 && y1 < y2)
                {
                    for (int i = 0; i <= x1 - x2; i++)
                    {
                        array[x1 - i, y1 + i]++;
                    }
                }
                else if (x1 > x2 && y1 > y2)
                {
                    for (int i = 0; i <= x1 - x2; i++)
                    {
                        array[x1 - i, y1 - i]++;
                    }
                }
                else
                {
                    throw new Exception();
                }

            }
        }

        for (int x = 0; x < 10; x++)
        {
            string strLine = "";
            for (int y = 0; y < 10; y++)
            {
                strLine += array[x, y];
            }
        }
        int count = 0;
        for (int x = 0; x < gridsize; x++)
        {
            for (int y = 0; y < gridsize; y++)
            {
                if (array[x, y] >= 2) count++;
            }

        }

         PrintSolution(count, "18627", "part 2");
    }
}

