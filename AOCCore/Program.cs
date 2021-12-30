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
        ExcuteDay(sw, ref now, ref previous, new Day5());
        ExcuteDay(sw, ref now, ref previous, new Day6());
        ExcuteDay(sw, ref now, ref previous, new Day7());
        ExcuteDay(sw, ref now, ref previous, new Day8());
        ExcuteDay(sw, ref now, ref previous, new Day9());
        ExcuteDay(sw, ref now, ref previous, new Day10());
        ExcuteDay(sw, ref now, ref previous, new Day11());
         ExcuteDay(sw, ref now, ref previous, new Day12());
        ExcuteDay(sw, ref now, ref previous, new Day13());
        //sw.Restart();
        //  var test = new Day10();
        //  test.Part1(true);

        //test.Main(test.inputLines);

    }

    private static void ExcuteDay(Stopwatch sw, ref double now, ref double previous, Day day1)
    {
        string part1 = day1.Part1(false);
        PrintTime(sw, part1, ref now, ref previous);
        string part2 = day1.Part2(false);
        PrintTime(sw, part2, ref now, ref previous);
    }

    private static void PrintTime(Stopwatch sw, string solution, ref double now, ref double previous)
    {
        now = sw.Elapsed.TotalMilliseconds;
        Console.WriteLine("{0} ms \t ({1} ms totaal) \t{2}", Math.Round(now - previous), Math.Round(now), solution);
        previous = now;
    }
}

