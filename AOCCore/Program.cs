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
        var day = new Day07();
        day.Part1(true);
        //if (false) {
        //    await InputFetcher.GetFile("2022", "02");
        //}
    }

}

