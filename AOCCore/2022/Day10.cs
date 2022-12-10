using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AOCCore._2022;

class Day10 : Day
{
    public Day10()
    {
        GetInput("2022", "10");

    }

    //part1 
    public override void Part2(List<string> Lines)
    {
        int Position = 1;
        int Cycle = 0;
        string output = "";
        for (int i = 0; i < Lines.Count; i++)
        {
            output += Math.Abs(Position - Cycle++ % 40) <= 1 ? "#" : ".";
            if (Lines[i].Contains("addx"))
            {
                output += Math.Abs(Position - Cycle++ % 40) <= 1 ? "#" : ".";
                Position += int.Parse(Lines[i].Split(' ')[1]);
            }
        }
        for (int i = 0; i < 6; i++)
            Console.WriteLine(output.Substring(i * 40, 40));
    }


}
