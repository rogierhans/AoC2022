using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Priority_Queue;
class Day25 : Day
{
    public Day25()
    {
        GetInput(RootFolder + @"2021_25\");
    }

    public override string Part1(List<string> Lines)
    {
        var grid = new List<char[]>();
        for (int i = 0; i < Lines.Count; i++)
        {
            grid.Add(Lines[i].ToCharArray());
        }

        int k = 0;
        bool p = true;
        while (p)
        {
            p = false;
            k++;

            var newGrid = grid.DeepCopy();


            Parallel.ForEach(SL.GetNumbersInt(0, grid.Count), i =>
            {

                for (int j = 0; j < grid[0].Length; j++)
                {

                    if (grid[i][j] == '>')
                    {
                        var neighbor = grid[(i) % grid.Count][(j + 1) % grid[0].Length];

                        if (neighbor == '.')
                        {
                            p = true;
                            newGrid[i][j] = '.';
                            newGrid[(i) % grid.Count][(j + 1) % grid[0].Length] = grid[i][j];
                        }
                    }
                }
            });


            grid = newGrid;
            newGrid = grid.DeepCopy();

            Parallel.ForEach(SL.GetNumbersInt(0, grid[0].Length), j => {
                for (int i = 0; i < grid.Count; i++)
                {

                    if (grid[i][j] == 'v')
                    {
                        var neighbor = grid[(i + 1) % grid.Count][(j) % grid[0].Length];
                        if (neighbor == '.')
                        {
                            p = true;
                            newGrid[i][j] = '.';
                            newGrid[(i + 1) % grid.Count][(j) % grid[0].Length] = grid[i][j];
                        }
                    }
                }
            });

            grid = newGrid;
        }
        return PrintSolution(k, "441", "part 1");
    }
    public override string Part2(List<string> inputLines)
    {
        return PrintSolution("50 stars", "50 stars", "part 2");
    }



}


