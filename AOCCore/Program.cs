using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Google.OrTools.Sat;
using AOC2021;
class Program
{
    [STAThreadAttribute]
    static void Main()
    {
        var sw = new Stopwatch();
        sw.Start(); double now = 0; double previous = now;
        ExcuteDay(sw, ref now, ref previous, new Day1());
        ExcuteDay(sw, ref now, ref previous, new Day2());
        ExcuteDay(sw, ref now, ref previous, new Day3());
        ExcuteDay(sw, ref now, ref previous, new Day4());
    }

    private static void ExcuteDay(Stopwatch sw, ref double now, ref double previous, Day day1)
    {
        string part1 = day1.Part1(false);
        PrintTime(sw, part1, ref now, ref previous);
        string part2 =  day1.Part2(false);
        PrintTime(sw, part2, ref now, ref previous);
    }

    private static void PrintTime(Stopwatch sw,string solution, ref double now, ref double previous)
    {
        now = sw.Elapsed.TotalMilliseconds;
        Console.WriteLine("{0} ms \t  total {1} ms \t{2}", Math.Round(now - previous), Math.Round(now), solution);
        previous = now;
    }
}

