using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AOCCore;
using Google.OrTools.LinearSolver;

namespace AOCCore._2021;

class Day09 : Day
{
    public Day09()
    {
        GetInput("2021", "09");

    }

    public override void Part1(List<string> Lines)
    {
        var grid = Lines.Select(x => x.List().Select(int.Parse).ToList()).ToList();
        bool[,] visited = new bool[grid.Count, grid[0].Count];
        bool foundNew = true;
        List<int> Lowest = new List<int>();
        List<int> Sizes = new List<int>();
        while (foundNew)
        {
            foundNew = false;
            for (int x = 0; x < grid.Count; x++)
            {
                for (int y = 0; y < grid[0].Count; y++)
                {
                    if (!visited[x, y] && grid[x][y] != 9)
                    {
                        foundNew = true;
                        var basin = grid.DFS((x, y), GridHelper.Neighbor4(), x => x != 9);
                        //basin.Select(x => string.Format("({0},{1},{2})",x.Item1, x.Item2, x.Item3)).ToList().Print("\n");
                        //Console.WriteLine(basin.Count);
                        foreach (var (_, xp, yp) in basin)
                        {
                            grid[xp][yp] = 9;
    
                            visited[xp, yp] = true;
                        }
                        Lowest.Add(basin.Min(x => x.Item1 + 1));
                        Sizes.Add(basin.Count);
                    }
                }
            }
        }
        

       // Console.ReadLine();
       // Lowest.Sum().P() ;
        var top3 = Sizes.OrderByDescending(x => x).Take(3).ToList();
        top3[0].P(); top3[1].P(); top3[2].P();
        (top3[0] * top3[1] * top3[2]).P();
    }
    public override void Part2(List<string> Lines)
    {


    }
}
