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

class Day05 : Day
{
    public Day05()
    {
        GetInput("2017", "06");

    }

    public override void Part1(List<string> Lines)
    {
        int index = 0;
        int steps = 0;
        var numbers = Lines.Select(int.Parse).ToList();
        while (true)
        {
            steps++;
            //  numbers.Print();
            if (index < 0 || index >= Lines.Count)
            {

                Console.WriteLine(steps);
                numbers.Print(" ");
                Console.ReadLine();
                return;
            }



            int oldIndex = index;
            index += numbers[index];
            if ((numbers[oldIndex]) >= 3)
            {
                numbers[oldIndex]--;
            }
            else
                numbers[oldIndex]++;
        }



    }
}

