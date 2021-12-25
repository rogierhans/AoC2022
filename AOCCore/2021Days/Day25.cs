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

    public override void Main(List<string> Lines)
    {
        //   Lines.Print();
        var grid = Lines.Select(x => x.List()).ToList();
        // grid.Print();


        int k = 0;
        bool p = true;
        while(p)  // for (int k = 0; k < 5; k++)
        {
            p = false;
            k++;

            var newGrid = grid.DeepCopy();
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Count; j++)
                {

                    if (grid[i][j] == ">")
                    {
                        var neighbor = grid[(i) % grid.Count][(j + 1) % grid[0].Count];

                        if (neighbor == ".")
                        {
                            p = true;
                            newGrid[i][j] = ".";
                            newGrid[(i) % grid.Count][(j + 1) % grid[0].Count] = grid[i][j];
                        }
                    }
                }
            }
            grid = newGrid;
            newGrid = grid.DeepCopy();
            //Console.WriteLine();
            //grid.Print();            //Console.WriteLine();
            //grid.Print();
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Count; j++)
                {

                    if (grid[i][j] == "v")
                    {
                        var neighbor = grid[(i + 1) % grid.Count][(j) % grid[0].Count];
                       // Console.WriteLine("first "+grid[i][j]);
                        if (neighbor == ".")
                        {
                            p = true;
                            newGrid[i][j] = ".";
                          //  Console.WriteLine(grid[i][j]);
                            newGrid[(i + 1) % grid.Count][(j) % grid[0].Count] = grid[i][j];
                        }
                    }
                }
            }
            grid = newGrid;
            //Console.WriteLine();
            //grid.Print();
         //   Console.ReadLine();
        }
        Console.WriteLine(k);
        Console.ReadLine();
    }




}


