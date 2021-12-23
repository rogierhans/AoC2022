using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Gurobi;

class Day15Gurobi : Day
{

    public Day15Gurobi()
    {
        GetInput(RootFolder + @"2021_15\");
    }
    public const string BLOCK = "\U00002588";

    public override void Main(List<string> Lines)
    {
        var grid = Lines.Select(g => g.List().Select(int.Parse).ToList()).ToList();
        List<List<int>> bigGrid = GreateBigGrid(grid);
        grid = bigGrid;

        var env = new GRBEnv();
        var model = new GRBModel(env);

        // List<List<GRBVar>> vars = bigGrid.GridSelect(x => model.AddVar(0, 1, 0, GRB.CONTINUOUS, ""));


        GRBLinExpr[,] Flowout = new GRBLinExpr[bigGrid.Count, bigGrid[0].Count];
        GRBLinExpr[,] FLowIn = new GRBLinExpr[bigGrid.Count, bigGrid[0].Count];
        GRBVar[,] vars = new GRBVar[bigGrid.Count, bigGrid[0].Count];
        GRBVar[,] RigthPipe = new GRBVar[bigGrid.Count, bigGrid[0].Count - 1];
        GRBVar[,] DownPipe = new GRBVar[bigGrid.Count - 1, bigGrid[0].Count];
        for (int i = 0; i < bigGrid.Count; i++)
        {
            for (int j = 0; j < bigGrid[0].Count; j++)
            {
                Flowout[i, j] = new GRBLinExpr();
                FLowIn[i, j] = new GRBLinExpr();
                vars[i, j] = model.AddVar(0, 1, 0.0, GRB.CONTINUOUS, "");
                // Console.WriteLine("{0},{1} {2} {3}", i, j, FlowSum[i, j], vars[i, j]);
                if (j < bigGrid[0].Count - 1)
                    RigthPipe[i, j] = model.AddVar(0, 1, 0.0, GRB.CONTINUOUS, "");
                if (i < bigGrid.Count - 1)
                    DownPipe[i, j] = model.AddVar(0, 1, 0.0, GRB.CONTINUOUS, "");
            }
        }
        GRBLinExpr objective = new GRBLinExpr();
        for (int i = 0; i < bigGrid.Count; i++)
        {
            for (int j = 0; j < bigGrid[0].Count; j++)
            {
                if (!(i == 0 && j == 0))
                       objective += vars[i, j] * bigGrid[i][j];
            }
        }
        model.AddConstr(vars[0, 1] == 1, "");
       // model.AddConstr(vars[bigGrid.Count - 1, bigGrid[0].Count - 1] == 1, "");


        for (int i = 0; i < bigGrid.Count; i++)
        {
            for (int j = 0; j < bigGrid[0].Count; j++)
            {
                if (i > 0)
                {
                    FLowIn[i, j] += DownPipe[i - 1, j];
                }
                if (j > 0)
                {
                    FLowIn[i, j] += RigthPipe[i, j - 1];
                }
                //Console.WriteLine(j + " " + (bigGrid[0].Count - 2));
                if (j < bigGrid[0].Count - 1)
                    Flowout[i, j] += RigthPipe[i, j];
                if (i < bigGrid.Count - 1)
                    Flowout[i, j] += DownPipe[i, j];
            }
        }
        for (int i = 0; i < bigGrid.Count; i++)
        {
            for (int j = 0; j < bigGrid[0].Count; j++)
            {
                //Console.WriteLine("{0},{1} {2} {3}", i, j, FlowSum[i, j], vars[i,j]);

                if (i == 0 && j == 0)
                {
                    model.AddConstr(Flowout[i, j] == 1, "");
                }
                else if (i == bigGrid.Count - 1 && j == bigGrid[0].Count - 1)
                {
                    model.AddConstr(FLowIn[i, j] == 1, "");
                    model.AddConstr(vars[i, j] == FLowIn[i, j], "");
                }
                else
                {
                    model.AddConstr(FLowIn[i, j] == Flowout[i, j], "");
                    model.AddConstr(vars[i, j] == Flowout[i, j], "");
                    model.AddConstr(vars[i, j] == FLowIn[i, j], "");
                }

            }
        }
        model.SetObjective(objective);

        model.Optimize();

        vars.ToLists().GridSelect(x => x.X > 0 ? BLOCK : ".").PrintPlusPlus();
        for (int i = 0; i < bigGrid.Count; i++)
        {
            string line = "";
            for (int j = 0; j < bigGrid[0].Count; j++)
            {
                line += vars[i, j].X > 0 ? BLOCK : ".";
            }
          //  Console.WriteLine(line);
        }
        Console.ReadLine();
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
