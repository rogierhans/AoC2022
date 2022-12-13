//using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace AOCCore._2022;

class Day12 : Day
{
    public Day12()
    {
        GetInput("2022", "12");

    }

    //part1 
    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        var grid = Lines.Select(g => g.List().ToList()).ToList();
        GetDistances(grid).P();
    }


    private int GetDistances(List<List<string>> grid)
    {

        var q = new Queue<(int, int)>();
        var distance = grid.GridSelect(x => int.MaxValue);
        
        for (int x = 0; x < grid.Count; x++)
        {
            for (int y = 0; y < grid[0].Count; y++)
            {
                if ( grid[x][y] == "S")
                {
                    grid[x][y] = "a";
                    distance[x][y] = 0;
                    q.Enqueue((x, y));
                }
                else
                {
                    distance[x][y] = int.MaxValue;
                }
            }
        }

        // bool[,] added = new bool[grid.Count, grid[0].Count];
        int counter = 0;
        var list = new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) };
        while (q.Count > 0)
        {

            var (x, y) = q.Dequeue();
            //   Console.WriteLine("{0} {1}", x, y);
            // if (distance[x][y] > distance.Last().Last()) break;
            foreach (var (xoff, yoff) in list)
            {
                var nx = x + xoff;
                var ny = y + yoff;
                bool outOfRow = nx < 0 || nx >= grid.Count;
                bool outOfColumn = ny < 0 || ny >= grid[0].Count;

                if (outOfColumn || outOfRow) continue;
                if ( ( grid[nx][ny][0] - grid[x][y][0] <= 1  && grid[nx][ny] != "E") || (grid[nx][ny] == "E" && grid[x][y] == "z"))
                {
                  //  Console.WriteLine("{0} {1} {2}", grid[x][y][0] - grid[nx][ny][0], grid[x][y][0], grid[nx][ny][0]);
                    var riskLevel = 1;
                    var newTotal = riskLevel + distance[x][y];

                    if (distance[nx][ny] > newTotal)
                    {
                        distance[nx][ny] = newTotal;
                        {
                            q.Enqueue((nx, ny));
                        }
                    }
                    if (Grid[nx][ny] == "E" && grid[nx][ny][0] - grid[x][y][0] <= 1)
                    {
                        distance[nx][ny].P();
                    }
                }
            }

            counter++;
        }

        // distance.GridSelect().PrintPlusPlus("\t");
        Console.ReadLine();
        return 0;
    }
}
