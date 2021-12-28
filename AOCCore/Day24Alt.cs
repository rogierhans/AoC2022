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

        var dictLetter = new Dictionary<string, int>();
        dictLetter["w"] = 0;
        dictLetter["x"] = 1;
        dictLetter["y"] = 1;
        dictLetter["z"] = 1;

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
                string a = "";
                string b = var2;
                string c = "";
                foreach (var letter in letters)
                {
                    if (var2 == letter)
                    {
                        b = letter + dictLetter[letter];
                    }
                    if (var1 == letter)
                    {
                        c = letter + dictLetter[letter]++;
                        a = letter + dictLetter[letter];
                    }
                }
                tuples.Add((a, c, multi, b));
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


