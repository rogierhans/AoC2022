using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AOC2021;

class Day9 : Day
{
    public Day9()
    {
        GetInput(RootFolder + @"2021_09\");
    }


    public override string Part1(List<string> Lines)
    {
        int count = 0;
        var grid = Lines.Select(x => x.List().Select(y => int.Parse(y)).ToList()).ToList();
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[0].Count; j++)
            {
                bool lower = true;
                foreach (var offset in new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    int x = i + offset.Item1;
                    int y = j + offset.Item2;
                    bool outOfRow = x < 0 || x >= grid.Count;
                    bool outOfColumn = y < 0 || y >= grid[0].Count;
                    if (!outOfColumn && !outOfRow)
                    {
                        lower &= grid[i][j] < grid[x][y];
                    }
                }
                if (lower)
                {
                    count += grid[i][j]+1;
                }
            }
        }

        return PrintSolution(count, "539", "part 1");
    }
    public override string Part2(List<string> Lines)
    {
        List<int> sizeBasins = new();
        var grid = Lines.Select(x => x.List().Select(y => int.Parse(y)).ToList()).ToList();
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[0].Count; j++)
            {
                bool lower = true;
                foreach (var offset in new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    int x = i + offset.Item1;
                    int y = j + offset.Item2;
                    bool outOfRow = x < 0 || x >= grid.Count;
                    bool outOfColumn = y < 0 || y >= grid[0].Count;
                    if (!outOfColumn && !outOfRow)
                    {
                        lower &= grid[i][j] < grid[x][y];
                    }
                }
                if (lower)
                {
                    sizeBasins.Add(BasinSize(i, j, grid));
                }
            }
        }
        var order = sizeBasins.OrderByDescending(x => x).ToList();
        return PrintSolution(order[0] * order[1] * order[2], "736920", "part 2");
    }

    private static int BasinSize(int startx, int stary, List<List<int>> grid)
    {
        int count = 0;

        Queue<(int, int)> q = new();
        var visited = grid.GridSelect(x => false);
        q.Enqueue((startx, stary));
        while (q.Count > 0)
        {
            var position = q.Dequeue();
            foreach (var offset in new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                int x = position.Item1 + offset.Item1;
                int y = position.Item2 + offset.Item2;
                bool outOfRow = x < 0 || x >= grid.Count;
                bool outOfColumn = y < 0 || y >= grid[0].Count;
                if (!outOfColumn && !outOfRow && !visited[x][y] && grid[x][y] != 9)
                {
                    q.Enqueue((x, y));
                    visited[x][y] = true;
                    count++;
                }
            }
        }
        return count;
    }
}

