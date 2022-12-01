using Google.OrTools.LinearSolver;
using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AOCCore._2017;

class Day04 : Day
{
    public Day04()
    {
        GetInput("2017", "04");

    }

    public override void Part1(List<string> Lines)
    {

        int counter = 0;
        foreach (var line in Lines)
        {
            HashSet<string> set = new HashSet<string>();
            bool valid = true;
            foreach (var word in line.Split(" "))
            {
                string newWord = word.List().OrderBy(x => x).ToList().Flat();
                if (set.Contains(newWord))
                {
                    valid = false;
                }
                else
                {
                    set.Add(newWord);
                }   
            }
            if (valid) counter++;
        }
        Console.WriteLine(counter);
        return  ;
    }
}

