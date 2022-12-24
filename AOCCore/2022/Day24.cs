//using Priority_Queue;

using Google.OrTools.Sat;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;
using static Microsoft.FSharp.Core.ByRefKinds;

namespace AOCCore._2022;

class Day24 : Day
{
    public Day24()
    {
        GetInput("2022", "24");

    }

    class Blizzard
    {
        public string Name;
        (int, int) Position;
        (int, int) Direction;

        public Blizzard(string name, (int, int) Position, (int, int) direction)
        {
            Name = name;
            this.Position = Position;
            Direction = direction;
        }

        public (int, int) CurrentPosition(int iteration, int maxRow, int maxCol)
        {
            int maxIteration = 10000;
            var (r, c) = Position;
            var (dr, dc) = Direction;
            var (mr, mc) = (r - 1, c - 1);
            var (nr, nc) = (mod(mr + (dr * iteration), maxRow), mod((mc) + (dc * iteration), maxCol));
            return (nr + 1, nc + 1);

        }
        int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
    public override void Part1(List<string> Lines)
    {
        var grid = Lines.Select(x => x.List()).ToList();
        grid.Print(" ");
        grid[0][1] = "#";
        List<Blizzard> zards = new List<Blizzard>();
        int maxRow = Lines.Count;
        int maxCol = Lines[0].List().Count;
        for (int r = 0; r < Lines.Count; r++)
        {
            var line = Lines[r].List();
            for (int c = 0; c < line.Count; c++)
            {
                var cell = line[c];
                if (cell == "v") zards.Add(new Blizzard(cell, (r, c), (1, 0)));
                if (cell == "^") zards.Add(new Blizzard(cell, (r, c), (-1, 0)));
                if (cell == "<") zards.Add(new Blizzard(cell, (r, c), (0, -1)));
                if (cell == ">") zards.Add(new Blizzard(cell, (r, c), (0, 1)));
            }
        }

        HashSet<(int, int, bool,bool)> Q = new HashSet<(int, int, bool, bool)>();
        Q.Add((1, 1,false,false));
        for (int m = 1; m < 1000; m++)
        {
            //Q.Count().P();
            List<List<string>> stateGrid = StateGrid(grid, zards, maxRow, maxCol, m);
            //  stateGrid.Print(" ");
            var nQ = new HashSet<(int, int,bool,bool)>();
            foreach (var (r, c, first,second) in Q)
            {
                var hasFirst = first;
                var hasSecond = second;
                if (r == maxRow - 1 && c == maxCol - 2)
                {
                    hasFirst = true;
                }
                if (r == 0 && c == 1 && hasFirst)
                {
                    hasSecond = true;
   
                }
                if (r == maxRow - 1 && c == maxCol - 2 && hasFirst && hasSecond)
                {
                    (m - 1).P();
                    Console.ReadLine();
                    return;
                }
                foreach (var (dr, dc) in new List<(int, int)>() { (0, 1), (-1, 0), (1, 0), (0, -1), (0, 0) })
                {
                    var (nr, nc) = (r + dr, c + dc);
                    if ( nr >= 0 && nc >= 0 && nr < maxRow && nc < maxCol && stateGrid[nr][nc] == "." )
                        nQ.Add((nr, nc, hasFirst,hasSecond));
                    //nQ.Add()

                }
            }
            Q = nQ;
            //Console.ReadLine();
        }

        for (int i = 0; i < 10; i++)
        {
            Console.ReadLine();
        }
    }

    private static List<List<string>> StateGrid(List<List<string>> grid, List<Blizzard> zards, int maxRow, int maxCol, int i)
    {
        var pGrid = grid.GridSelect(x => ".").ToList();

        for (int r = 0; r < maxRow; r++)
        {
            for (int c = 0; c < maxCol; c++)
            {
                if (c == 0 || r == 0 || c == maxCol - 1 || r == maxRow - 1)
                    pGrid[r][c] = "#";
            }
        }
        foreach (var zard in zards)
        {
            var (r, c) = zard.CurrentPosition(i, maxRow - 2, maxCol - 2);

            if (pGrid[r][c] != ".")
            {
                if (pGrid[r][c] == "v" || pGrid[r][c] == "^" || pGrid[r][c] == "<" || pGrid[r][c] == ">")
                {
                    pGrid[r][c] = "2";
                }
                else
                {
                    int number = int.Parse(pGrid[r][c]);
                    pGrid[r][c] = (number + 1).ToString();

                }

            }
            else
                pGrid[r][c] = zard.Name;

        }
        pGrid[0][1] = ".";
        pGrid[maxRow - 1][maxCol - 2] = ".";
        return pGrid;
    }
}
