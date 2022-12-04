using Gurobi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore._2022;


class GurobiDay : Day
{
    public GurobiDay()
    {
        GetInput("2022", "03");

    }

    GRBModel Model = new GRBModel(new GRBEnv());

    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        int sum = 0;
        for (int k = 0; k < Lines.Count; k++)
        {

        }
        Console.WriteLine(sum);

    }
}

