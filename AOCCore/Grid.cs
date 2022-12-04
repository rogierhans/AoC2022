using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Gurobi;

public static class GridHelper
{


    public static List<List<T>> Make<T>(int height, int width, T baseObject)
    {
        List<List<T>> list = new List<List<T>>();
        for (int i = 0; i < height; i++)
        {
            List<T> subList = new List<T>();
            for (int j = 0; j < width; j++)
            {
                subList.Add(baseObject);
            }
            list.Add(subList);
        }
        return list;
    }

    public static List<(int, int)> Neighbor8()
    {
        return new List<(int, int)> { (1,1), (-1, 1), (1, -1), (-1, -1) , (1,0), (-1, 0), (0, 1), (0, -1) };
    }
    public static List<(int, int)> Neighbor4()
    {
        return new List<(int, int)> { (1, 0), (-1, 0), (0, 1), (0, -1) };
    }

    public static List<(int, int)> NeighborList<T>(this List<List<T>> list, int i, int j, int minI, int maxI, int minJ, int maxJ, bool includeSelf = true)
    {
        return (NeighborList(i, j, minI, maxI, minJ, maxJ, list.Count, list[0].Count, includeSelf));
    }

    public static List<(int, int)> NeighborList(int i, int j, int minI, int maxI, int minJ, int maxJ, int width, int height, bool includeSelf = true)
    {
        List<(int, int)> neighborList = new List<(int, int)>();
        for (int offsetI = minI; offsetI <= maxI; offsetI++)
        {
            for (int offsetJ = minJ; offsetJ <= maxJ; offsetJ++)
            {

                // Console.WriteLine(offsetI +" "+ offsetJ);
                int newI = i + offsetI;
                int newJ = j + offsetJ;

                bool outOfRow = newI < 0 || newI >= width;
                bool outOfColumn = newJ < 0 || newJ >= height;
                if (!outOfColumn && !outOfRow && !(!includeSelf && i == newI && j == newJ))
                {
                    neighborList.Add((newI, newJ));
                }
            }
        }


        return neighborList;
    }
    public static List<(int, int)> Neighbor4<T>(this List<List<T>> list, int i, int j)
    {
        List<(int, int)> neighborList = new List<(int, int)>();
        foreach (var offset in new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) })
        {
            int newI = i + offset.Item1;
            int newJ = j + offset.Item2;

            bool outOfRow = newI < 0 || newI >= list.Count;
            bool outOfColumn = newJ < 0 || newJ >= list[0].Count;
            if (!outOfColumn && !outOfRow && !(i == newI && j == newJ))
            {
                neighborList.Add((newI, newJ));
            }

        }
        return neighborList;
    }

    public static List<(T, int, int)> DFS<T>(this List<List<T>> grid, (int, int) index, List<(int, int)> neightbors, Func<T, bool> Condition)
    {
        var list = new List<(T, int, int)>();
        bool[,] visited = new bool[grid.Count, grid[0].Count];
        var start = grid[index.Item1][index.Item2];
        var stack = new Stack<(T, int, int)>();
        stack.Push((start, index.Item1, index.Item2));
        while (stack.Count > 0)
        {
            var current = stack.Pop();
            var (item, x, y) = current;
            visited[x, y] = true;
            list.Add(current);
            foreach (var (dx, dy) in neightbors)
            {
                int nx = x + dx;
                int ny = y + dy;
                if (0 <= nx && nx < grid.Count && 0 <= ny && ny < grid[0].Count && !visited[nx,ny])
                {
                   // Console.WriteLine("{0} {1}  from {2} {3}", nx, ny,x,y);
                    var nextItem = grid[nx][ny];
                    if (Condition(nextItem))
                    {
                      //  Console.WriteLine(nextItem);    
                        stack.Push((nextItem, nx, ny));
                        visited[nx, ny] = true;
                    }
                }

            }
        }
        return list;
    }

}
