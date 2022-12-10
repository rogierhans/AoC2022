using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AOCCore._2022;

class Day08 : Day
{
    public Day08()
    {
        GetInput("2022", "08");

    }

    public override void Part2(List<string> Lines)
    {
        // TryParse(Lines);
        var Listgrid = Lines.Select(x => x.List()).ToList().GridSelect(x => int.Parse(x));

        int length = Listgrid[0].Count;
        int[,] grid = new int[length, length];
        int[,] scores = new int[length, length];
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                grid[i, j] = Listgrid[i][j];
                scores[i, j] = 1;
            }
        }
        int offset = length - 1;
        var sw = new Stopwatch();
        sw.Start();

        int[] Left = new int[10];
        int[] Down = new int[10];
        int[] Right = new int[10];
        int[] Up = new int[10];

        for (int x = 0; x < length; x++)
        {
            
            for (int y = 0; y < length; y++)
            {
                scores[x, y] *= Left[grid[x, y]];
                scores[y, x] *= Down[grid[y, x]];
                scores[x, offset - y] *= Right[grid[x, offset - y]];
                scores[offset - y, x] *= Up[grid[offset - y, x]];
                int LeftHeight = grid[x, y];
                int DownHeight = grid[y, x];
                int RightHeight = grid[x, offset - y];
                int UpHeight = grid[offset - y, x];
                for (int number = 0; number <= LeftHeight; number++) Left[number] = 0;
                for (int number = 0; number <= DownHeight; number++) Down[number] = 0;
                for (int number = 0; number <= RightHeight; number++) Right[number] = 0;
                for (int number = 0; number <= UpHeight; number++) Up[number] = 0;
                for (int number = 0; number <= 9; number++)
                {
                    Up[number]++;
                    Right[number]++;
                    Left[number]++;
                    Down[number]++;
                }
            }
            for (int number = 0; number <= 9; number++)
            {
                Up[number] = 0;
                Right[number] = 0;
                Left[number] = 0;
                Down[number] = 0;
            }
        }

        int max = 0;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                max = Math.Max(max, scores[i, j]);
            }
        }
        Console.WriteLine(sw.Elapsed.TotalMilliseconds);
        Console.WriteLine(max);

    }

    public override void Part1(List<string> Lines)
    {
        var numberGrid = Grid.GridSelect(x => int.Parse(x));
        var seen = new bool[numberGrid.Count, numberGrid[0].Count].ToLists();
        for (int x = 0; x < numberGrid[0].Count; x++)
        {
            int height = -1;
            for (int y = 0; y < numberGrid.Count; y++)
            {
                if (numberGrid[x][y] > height)
                {
                    seen[x][y] = true;
                }
                height = Math.Max(height, numberGrid[x][y]);
            }
        }
        seen.GridSelect(x => x ? 1 : 0).Print(" ");
        Console.WriteLine();
        for (int y = 0; y < numberGrid.Count; y++)
        {
            int height = -1;
            for (int x = 0; x < numberGrid[0].Count; x++)
            {

                if (numberGrid[x][y] > height)
                {
                    if (numberGrid[x][y] == 4) Console.WriteLine(height);
                    seen[x][y] = true;
                }
                height = Math.Max(height, numberGrid[x][y]);
            }
        }
        seen.GridSelect(x => x ? 1 : 0).Print(" ");
        Console.WriteLine();
        for (int y = 0; y < numberGrid.Count; y++)
        {
            int height = -1;
            for (int x = numberGrid[0].Count - 1; x >= 0; x--)
            {
                if (numberGrid[x][y] > height)
                {
                    seen[x][y] = true;
                }
                height = Math.Max(height, numberGrid[x][y]);
            }
        }
        seen.GridSelect(x => x ? 1 : 0).Print(" "); Console.WriteLine();
        for (int x = 0; x < numberGrid[0].Count; x++)
        {
            int height = -1;
            for (int y = numberGrid.Count - 1; y >= 0; y--)
            {
                if (numberGrid[x][y] > height)
                {
                    seen[x][y] = true;
                }
                height = Math.Max(height, numberGrid[x][y]);
            }
        }
        seen.GridSelect(x => x ? 1 : 0).Print(" "); Console.WriteLine();
        int sum = 0;
        for (int x = 0; x < numberGrid[0].Count; x++)
        {
            for (int y = 0; y < numberGrid.Count; y++)
            {
                if (seen[x][y]) sum++;
            }
        }
        // Console.ReadLine();
        sum.P();
    }

}
