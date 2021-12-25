using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Priority_Queue;

class Day15 : Day
{


    public Day15()
    {
        GetInput(RootFolder + @"2021_15\");
    }
    public const string BLOCK = "\U00002588";

    class W<T> : FastPriorityQueueNode
    {
        public T X;

        public W(T x)
        {
            X = x;
        }
    }
    private W<(int, int)>[,] ws;

    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public override void Main(List<string> Lines)
    {
        var grid = Lines.Select(g => g.List().Select(int.Parse).ToList()).ToList();
        List<List<int>> bigGrid = GreateBigGrid(grid);
        grid = bigGrid;
        var realDistance = new List<List<int>>();
        var q = new FastPriorityQueue<W<(int, int)>>(grid.Sum(x => x.Count));
        ws = new W<(int, int)>[grid.Count, grid[0].Count];
        Init(grid, realDistance);
        realDistance[0][0] = 0;
        q.Enqueue(ws[0, 0], 0);
        bool[,] added = new bool[grid.Count, grid[0].Count];
        int counter = 0;
        while (q.Count > 0)
        {

            var W = q.Dequeue();
            var (x, y) = W.X;
            if (realDistance[x][y] > realDistance.Last().Last()) break;
            foreach (var (nx, ny) in grid.Neighbor4(x, y))
            {
                var riskLevel = grid[nx][ny];
                var newTotal = riskLevel + realDistance[x][y];
                if (realDistance[nx][ny] > newTotal)
                {
                    realDistance[nx][ny] = newTotal;

                    if (added[nx, ny])
                    {
                        q.UpdatePriority(ws[nx, ny], newTotal);
                    }
                    else
                    {
                        added[nx, ny] = true;
                        q.Enqueue(ws[nx, ny], newTotal);
                    }
                }
            }

            counter++;
        }
        realDistance.PrintPlusPlus("\t");
        Console.WriteLine("counter " + counter);
        Console.WriteLine(realDistance.Last().Last());
        Console.ReadLine();
    }

    private void Init(List<List<int>> grid, List<List<int>> dist)
    {
        for (int i = 0; i < grid.Count; i++)
        {
            List<int> list = new List<int>();
            for (int j = 0; j < grid.Count; j++)
            {
                ws[i, j] = new W<(int, int)>((i, j));
                list.Add(int.MaxValue);
            }
            dist.Add(list);
        }
    }

    private static List<List<int>> GreateBigGrid(List<List<int>> grid)
    {
        var bigGrid = new List<List<int>>();
        for (int r = 0; r < 5; r++)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                List<int> list = new List<int>();
                for (int c = 0; c < 5; c++)
                {
                    for (int j = 0; j < grid.Count; j++)
                    {
                        list.Add((grid[i][j] + (r + c)) > 9 ? (grid[i][j] + (r + c)) % 10 + 1 : grid[i][j] + (r + c));
                    }
                }
                bigGrid.Add(list);
            }

        }

        return bigGrid;
    }
}
