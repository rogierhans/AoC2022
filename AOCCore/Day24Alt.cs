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

    public override void Main(List<string> Lines)
    {
        CpModel model = new CpModel();

        List<(string, string, string, string)> newLines = GiveEachVariableNewNames(Lines);


        // a = b oper c
        foreach (var (a, b, oper, c) in newLines)
        {
            if (c[..1] == "x" || c[..1] == "y" || c[..1] == "z" || c[..1] == "w")
            {
                AddConstraint(model, a, b, c, oper);
            }
            else
            {
                AddConstraintConstant(model, a, b, int.Parse(c), oper);
            }
        }

        //set last z to 0
        model.Add(VariableNameToVar["z43"] == 0);
        AddObjective(model);

       // Console.WriteLine(model.Model.Objective);
        var solver = new CpSolver();
        var status = solver.Solve(model);
        if (status == CpSolverStatus.Optimal)
            foreach (var (key, value) in VariableNameToVar)
            {
                if (key[..1] == "w")
                    Console.WriteLine("{0} {1}", key, solver.Value(value));
            }
       // Console.ReadLine();
    }

    private static List<(string, string, string, string)> GiveEachVariableNewNames(List<string> Lines)
    {
        var tuples = new List<(string, string, string, string)>();
        int w = 0;
        int x = 1;
        int y = 1;
        int z = 1;
        foreach (var line in Lines)
        {
            if (line[..3] == "inp")
            {
                w++;
            }
            else
            {
                var (multi, var1, var2) = line.Pattern("{0} {1} {2}", x => x, x => x, x => x);
                string newVar2 = var2;
                string newVar1 = "error";
                string assignment = "error";
                if (var2 == "x") newVar2 = "x" + x;
                if (var2 == "y") newVar2 = "y" + y;
                if (var2 == "z") newVar2 = "z" + z;
                if (var2 == "w") newVar2 = "w" + w;
                if (var1 == "x") newVar1 = "x" + x++;
                if (var1 == "y") newVar1 = "y" + y++;
                if (var1 == "z") newVar1 = "z" + z++;
                if (var1 == "w") newVar1 = "w" + w++;
                if (var1 == "x") assignment = "x" + x;
                if (var1 == "y") assignment = "y" + y;
                if (var1 == "z") assignment = "z" + z;
                if (var1 == "w") assignment = "w" + w;
                tuples.Add((assignment, newVar1, multi, newVar2));
            }
        }
        return tuples;
    }

    private void AddObjective(CpModel model)
    {
        model.Minimize(VariableNameToVar["w14"]);
        for (int i = 1; i < 14; i++)
        {
            model.AddTermToObjective(VariableNameToVar["w" + ((14 - i))], 1 << i);
        }
    }

    Dictionary<string, IntVar> VariableNameToVar = new Dictionary<string, IntVar>();
    private IntVar AddVarToModel(CpModel model, string varName)
    {
        if (!VariableNameToVar.ContainsKey(varName))
        {
            if (varName[..1] == "w")
            {
                IntVar x = model.NewIntVar(1, 9, varName);
                VariableNameToVar[varName] = x;
            }
            else
            {
                IntVar x = model.NewIntVar(-1000000, 10000000, varName);
                VariableNameToVar[varName] = x;
            }
        }
        return VariableNameToVar[varName];
    }

    private void AddConstraint(CpModel model, string assigment, string var1, string var2, string operatorString)
    {
        var assigmentVar = AddVarToModel(model, assigment);
        var Var1 = AddVarToModel(model, var1);
        var Var2 = AddVarToModel(model, var2);

        if (operatorString == "add")
        {
            model.Add(assigmentVar == Var1 + Var2);
        }
        else if (operatorString == "mul")
        {
            model.AddMultiplicationEquality(assigmentVar, new List<IntVar>() { Var1, Var2 });
        }
        else if (operatorString == "div")
        {
            model.AddDivisionEquality(assigmentVar, Var1, Var2);
        }
        else if (operatorString == "mod")
        {
            model.AddModuloEquality(assigmentVar, Var1, Var2);
            model.Add(assigmentVar >= 0);
        }
        else if (operatorString == "eql")
        {
            var B = model.NewBoolVar("b");
            model.Add(Var1 == Var2).OnlyEnforceIf(B);
            model.Add(assigmentVar == 1).OnlyEnforceIf(B);
            model.Add(Var1 != Var2).OnlyEnforceIf(B.Not());
            model.Add(assigmentVar == 0).OnlyEnforceIf(B.Not());
        }
        else
        {
            throw new Exception();
        }
    }
    private void AddConstraintConstant(CpModel model, string assigment, string var1, int number, string operatorString)
    {
        var assigmentVar = AddVarToModel(model, assigment);
        var Var1 = AddVarToModel(model, var1);

        if (operatorString == "add")
        {
            model.Add(assigmentVar == Var1 + number);
        }
        else if (operatorString == "mul")
        {
            model.Add(assigmentVar == Var1 * number);
        }
        else if (operatorString == "div")
        {
            model.AddDivisionEquality(assigmentVar, Var1, number);
        }
        else if (operatorString == "mod")
        {
            model.AddModuloEquality(assigmentVar, Var1, number);

            model.Add(assigmentVar >= 0);
        }
        else if (operatorString == "eql")
        {
            var B = model.NewBoolVar("b");
            model.Add(Var1 == number).OnlyEnforceIf(B);
            model.Add(assigmentVar == 1).OnlyEnforceIf(B);
            model.Add(Var1 != number).OnlyEnforceIf(B.Not());
            model.Add(assigmentVar == 0).OnlyEnforceIf(B.Not());
        }
        else
        {
            throw new Exception();
        }
    }

}


