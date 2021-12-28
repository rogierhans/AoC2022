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
        var newLines = new List<string>();
        int w = 1;
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

                string newLine = assignment + " = " + newVar1 + " " + multi + " " + newVar2;
                newLines.Add(newLine);

                Console.WriteLine(newLine);

                Console.WriteLine(line);

                AddConstraint(model, assignment, newVar1, newVar2, multi);
            }


        }
        var solver = new CpSolver();
        var status = solver.Solve(model);

        Console.WriteLine(status);
        if (status == CpSolverStatus.Optimal)
            foreach (var (key, value) in dict) {
            Console.WriteLine("{0} {1}", key, solver.Value(value));
        }

        //newLines.Print("\n");
        Console.ReadLine();
    }

    Dictionary<string, IntVar> dict = new Dictionary<string, IntVar>();
    private IntVar AddVarToModel(CpModel model, string varName)
    {
        if (!dict.ContainsKey(varName))
        {
            IntVar x = model.NewIntVar(-100000,1000000, varName);
            dict[varName] = x;
        }
        return dict[varName];
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
    }
    private void AddConstraintConstant(CpModel model, string assigment, string var1, int number, string operatorString)
    {
        var assigmentVar = AddVarToModel(model, assigment);
        var Var1 = AddVarToModel(model, var1);

        if (operatorString == "add")
        {
            model.Add(assigmentVar == Var1 + number);
        }
        if (operatorString == "mul")
        {
            model.Add(assigmentVar == Var1 * number);
        }
        if (operatorString == "div")
        {
            model.Add(assigmentVar == Var1 / number);
        }
    }

    private void Test()
    {


        // Creates the model.
        CpModel model = new CpModel();
        // Creates the variables.
        int num_vals = 3;

        IntVar x = model.NewIntVar(0, num_vals - 1, "x");
        IntVar y = model.NewIntVar(0, num_vals - 1, "y");
        IntVar z = model.NewIntVar(0, num_vals - 1, "z");

        var lel = x + y;
        model.AddModuloEquality(z, x, y);
        // Adds a different constraint.
        model.Add(x != y);

        // Creates a solver and solves the model.
        CpSolver solver = new CpSolver();

        // Adds a time limit. Parameters are stored as strings in the solver.
        solver.StringParameters = "max_time_in_seconds:10.0";

        CpSolverStatus status = solver.Solve(model);

        if (status == CpSolverStatus.Optimal)
        {
            Console.WriteLine("x = " + solver.Value(x));
            Console.WriteLine("y = " + solver.Value(y));
            Console.WriteLine("z = " + solver.Value(z));
        }
        Console.WriteLine(status);
        Console.ReadLine();
    }


}


