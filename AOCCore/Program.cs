using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Google.OrTools.Sat;
using AOCCore;
using AOCCore._2022;


using Google.OrTools.ConstraintSolver;
using static Google.OrTools.ConstraintSolver.RoutingModel.ResourceGroup;

class Program
{
    [STAThreadAttribute]
    static void Main()
    {
        //var WRName = new List<string>() { "SB-", "AR+", "NAO-", "SB+", "NAO+", "AR-", "0 No Label" };
        //var WRdict = File.ReadAllLines(@"C:\Users\Rogier\Desktop\WR_k6_combo.csv").Skip(1)
        //    .Select(x => x.Split(',')).ToDictionary(x => DateTime.Parse(x[0]), x => int.Parse(x[1]));
        //string line = "";
        //for (int hours = 384; hours < 504; hours+=24)
        //{
        //    line += DateTime.Parse("1987-01-01").AddHours(hours) + "\t" + WRName[WRdict[DateTime.Parse("1987-01-01").AddHours(hours - (hours % 24))]] + "\n";
        //}
        //line.P();
        //for (int hours = 8136; hours < 8328; hours += 24)
        //{
        //    line += DateTime.Parse("1996-01-01").AddHours(hours) + "\t" + WRName[WRdict[DateTime.Parse("1996-01-01").AddHours(hours - (hours % 24))]] + "\n";
        //}
        //line.P();
        //for (int hours = 96; hours < 264; hours += 24)
        //{
        //    line += DateTime.Parse("1997-01-01").AddHours(hours) + "\t" + WRName[WRdict[DateTime.Parse("1997-01-01").AddHours(hours - (hours % 24))]] + "\n";
        //}
        //line.P();
        //File.WriteAllText(@"C:\Users\Rogier\Desktop\temp.csvejmf", line);
        //return;


        //var a = Text.OneOf("a");
        //var b = Text.OneOf("b");
        //var ab = a.Append(b);
        //var abalt = a | b;
        //ab.Parse("ab").Value.ToArray().Select(x => x.ToString()).ToList().Print(" ");
        //return;
        //{
        //    var source = "(1 + 2 * (3 - 4) + 5 / 6) - 7 + (8 * 9)";
        //    var IntParser = Parser.Many(Text.DecDigit());
        //    var test = IntParser.Map(x => x.Select(y => int.Parse(y.ToString())).ToList());
        //    var OperatorParser = Parser.Many(Text.OneOf("+*-"));
        //    // var ExpersionParser = IntParser.
        //    var result = IntParser.Parse(source);
        //    result.Value.ToList().Print();
        //}

        // Year2021();


        //
        //while (true)
        {
            var day = new Day14();
            day.Part2(true);
        }

        //if (false) {
        //    await InputFetcher.GetFile("2022", "02");
        //}
    }

}

