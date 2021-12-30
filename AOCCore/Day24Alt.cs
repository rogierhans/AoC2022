using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.OrTools.Sat;
class Day24Alt : Day
{
    public Day24Alt()
    {
        GetInput(RootFolder + @"2021_24\");
    }

    public override string Part1(List<string> Lines)
    {
        VariableNameToVar = new Dictionary<string, IntVar>();
        CpModel model = new CpModel();
        AddLinesToModel(model, Lines);
        // a = b oper c

        //set last z to 0
        model.Add(VariableNameToVar["z43"] == 0);
        AddObjective(model, false);

        var solver = new CpSolver();
        var status = solver.Solve(model);
        string line = "";
        if (status == CpSolverStatus.Optimal)
            foreach (var (key, value) in VariableNameToVar)
            {
                if (key[..1] == "w")
                {
                    line += solver.Value(value);
                    //Console.WriteLine("{0} {1}", key, solver.Value(value));
                }
            }
        return PrintSolution(line, "97919997299495", "part 1");
    }

    public override string Part2(List<string> Lines)
    {
        VariableNameToVar = new Dictionary<string, IntVar>();
        CpModel model = new CpModel();
        AddLinesToModel(model, Lines);
        // a = b oper c

        //set last z to 0
        model.Add(VariableNameToVar["z43"] == 0);
        AddObjective(model, true);

        var solver = new CpSolver();
        var status = solver.Solve(model);
        string line = "";
        if (status == CpSolverStatus.Optimal)
            foreach (var (key, value) in VariableNameToVar)
            {
                if (key[..1] == "w")
                {
                    line += solver.Value(value);
                    //Console.WriteLine("{0} {1}", key, solver.Value(value));
                }
            }

        return PrintSolution(line, "51619131181131", "part 2");
    }
    private void AddConstraint(CpModel model, IntVar a, IntVar b, LinearExpr c, string operatorString)
    {
        if (operatorString == "add")
        {
            model.Add(a == b + c);
        }
        else if (operatorString == "mul")
        {
            model.AddMultiplicationEquality(a, new List<LinearExpr>() { b, c });
        }
        else if (operatorString == "div")
        {
            model.AddDivisionEquality(a, b, c);
        }
        else if (operatorString == "mod")
        {
            model.AddModuloEquality(a, b, c);
            model.Add(a >= 0);
        }
        else if (operatorString == "eql")
        {
            var ifStatementIsTrue = model.NewBoolVar("helperVariable");
            model.Add(b == c).OnlyEnforceIf(ifStatementIsTrue);
            model.Add(a == 1).OnlyEnforceIf(ifStatementIsTrue);
            model.Add(b != c).OnlyEnforceIf(ifStatementIsTrue.Not());
            model.Add(a == 0).OnlyEnforceIf(ifStatementIsTrue.Not());
        }
        else
        {
            throw new Exception();
        }
    }
    private void AddLinesToModel(CpModel model, List<string> Lines)
    {
        var tuples = new List<(string, string, string, string)>();
        var dictLetter = new Dictionary<string, int> { ["w"] = 0, ["x"] = 1, ["y"] = 1, ["z"] = 1 };
        var letters = new List<string>() { "w", "x", "y", "z" };
        foreach (var line in Lines)
        {
            if (line[..3] == "inp")
            {
                dictLetter["w"]++;
            }
            else
            {
                var (multi, var1, var2) = line.Pattern("{0} {1} {2}", x => x, x => x, x => x);
                var b = string2ModelVar(model, var1 + dictLetter[var1]);
                var a = string2ModelVar(model, var1 + ++dictLetter[var1]);
                LinearExpr c = new();
                if (letters.Contains(var2))
                {
                    c = string2ModelVar(model, var2 + dictLetter[var2]);
                }
                else
                {
                    c = new ConstantExpr(int.Parse(var2));
                }
                AddConstraint(model, a, b, c, multi);
            }
        }

    }

    private void AddObjective(CpModel model, bool minimizing)
    {
        if (minimizing)
        {
            model.Minimize(VariableNameToVar["w14"]);
        }
        else
        {
            model.Maximize(VariableNameToVar["w14"]);
        }
        for (int i = 1; i < 14; i++)
        {
            model.AddTermToObjective(VariableNameToVar["w" + ((14 - i))], 1 << i);
        }
    }

    Dictionary<string, IntVar> VariableNameToVar = new Dictionary<string, IntVar>();
    private IntVar string2ModelVar(CpModel model, string varName)
    {
        if (!VariableNameToVar.ContainsKey(varName))
        {
            if (varName[..1] == "w")
            {
                VariableNameToVar[varName] = model.NewIntVar(1, 9, varName);
            }
            else
            {
                VariableNameToVar[varName] = model.NewIntVar(-1000000, 10000000, varName);
            }
        }
        return VariableNameToVar[varName];
    }
}


