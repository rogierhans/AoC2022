//using Priority_Queue;
using Google.OrTools.ConstraintSolver;
using Google.OrTools.LinearSolver;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Channels;
using System.Xml;
using System.Xml.Linq;
using static Microsoft.FSharp.Core.ByRefKinds;

namespace AOCCore._2022;

class Day21 : Day
{
    public Day21()
    {
        GetInput("2022", "21");

    }
    public override void Part1(List<string> OGLines)
    {
        //1405357608
        // TryParse(Lines);
        {
            Dictionary<string, ulong> dict = new Dictionary<string, ulong>();
            Dictionary<string, (string, string, Func<ulong, ulong, ulong>)> functions = new Dictionary<string, (string, string, Func<ulong, ulong, ulong>)>();
            // Console.ReadLine();
            string rootNumber1 = "";
            string rootNumber2 = "";
            foreach (var line in OGLines)
            {
                var input = line.Split(' ');
                if (line.Contains("humn: ")) continue;
                if (input.Length == 2)
                {
                    dict[input[0].Replace(":", "")] = ulong.Parse(input[1]);
                }
                else if (input[0].Replace(":", "") == "root")
                {

                    rootNumber1 = input[1];
                    rootNumber2 = input[3];
                }
                else
                {
                    var name1 = input[1];
                    var name2 = input[3];
                    Func<ulong, ulong, ulong> func = (x, y) => x;
                    if (input[2] == "*") func = (x, y) => x * y;
                    if (input[2] == "+") func = (x, y) => x + y;
                    if (input[2] == "-") func = (x, y) => x - y;
                    if (input[2] == "/") func = (x, y) => x / y;
                    functions.Add(input[0].Replace(":", ""), (name1, name2, func));
                }
            }
            {
                bool changed = true;
                while (changed)
                {
                    changed = false;
                    foreach (var (key, (name1, name2, f)) in functions)
                    {
                        if (!dict.ContainsKey(key) && dict.ContainsKey(name1) && dict.ContainsKey(name2))
                        {
                            dict[key] = f(dict[name1], dict[name2]);
                            changed = true;
                        }
                    }

                }
            }
            foreach (var key in dict.Keys)
            {
                functions.Remove(key);
            }
            Test(dict, functions, rootNumber1, rootNumber2);

            return;
        }
    }

    private static void Test(Dictionary<string, ulong> FAKNKSFN, Dictionary<string, (string, string, Func<ulong, ulong, ulong>)> functions, string rootNumber1, string rootNumber2)
    {
        ulong i = 0;
        ulong change = 100000000000;
        while (true)
        {


            while (true)
            {
                i += change;
                var newDict = FAKNKSFN.ToDictionary(entry => entry.Key,
                                               entry => entry.Value);
                newDict["humn"] = i;
                var newFunction = functions.ToDictionary(entry => entry.Key,
                               entry => entry.Value);
                bool changed = true;
                while (changed)
                {
                    changed = false;
                    foreach (var element in newFunction)
                    {
                        //  element.P();
                        var (key, (name1, name2, f)) = element;
                        if (!newDict.ContainsKey(key) && newDict.ContainsKey(name1) && newDict.ContainsKey(name2))
                        {
                            newDict[key] = f(newDict[name1], newDict[name2]);
                            changed = true;
                        }
                    }
                    foreach (var key in FAKNKSFN.Keys)
                    {
                        newFunction.Remove(key);
                    }
                }
                if ((newDict[rootNumber1] == newDict[rootNumber2])) {
                    i.P();
                    newDict[rootNumber1].P();
                    newDict[rootNumber2].P();

                    Console.ReadLine();
                }
                if ((newDict[rootNumber1] < newDict[rootNumber2]))
                {
                    i = (i - change);
                    change = change / 10;
                }
            }
        }
    }
}
