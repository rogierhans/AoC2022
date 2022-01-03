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

        while (true)
        {
            memory = new List<(double, string)>();
            var sw = new Stopwatch();
            sw.Start(); double now = 0; double previous = now;
            //ExcuteDay(sw, ref now, ref previous, new Day1());
            //ExcuteDay(sw, ref now, ref previous, new Day2());
            //ExcuteDay(sw, ref now, ref previous, new Day3());
            //ExcuteDay(sw, ref now, ref previous, new Day4());
            //ExcuteDay(sw, ref now, ref previous, new Day5());
            //ExcuteDay(sw, ref now, ref previous, new Day6());
            //ExcuteDay(sw, ref now, ref previous, new Day7());
            //ExcuteDay(sw, ref now, ref previous, new Day8());
            //ExcuteDay(sw, ref now, ref previous, new Day9());
            //ExcuteDay(sw, ref now, ref previous, new Day10());
            //ExcuteDay(sw, ref now, ref previous, new Day11());
            //ExcuteDay(sw, ref now, ref previous, new Day12());
            //ExcuteDay(sw, ref now, ref previous, new Day13());
            //ExcuteDay(sw, ref now, ref previous, new Day14());
            //ExcuteDay(sw, ref now, ref previous, new Day15());
            //ExcuteDay(sw, ref now, ref previous, new Day16());
            //ExcuteDay(sw, ref now, ref previous, new Day17());
            //ExcuteDay(sw, ref now, ref previous, new Day18());
            //ExcuteDay(sw, ref now, ref previous, new Day19Alt());
            //ExcuteDay(sw, ref now, ref previous, new Day20());
            //ExcuteDay(sw, ref now, ref previous, new Day21());
            //ExcuteDay(sw, ref now, ref previous, new Day22());
            ExcuteDay(sw, ref now, ref previous, new Day23Alt());
            //ExcuteDay(sw, ref now, ref previous, new Day24Alt());
            //ExcuteDay(sw, ref now, ref previous, new Day25());
            //
            double c = 0;
            foreach (var (a, b) in memory.OrderBy(kvp => kvp.Item1))
            {
                c += a;
                Console.WriteLine(b + " " + a + " (" + Math.Round(c, 1) + ")");
            }
            Console.ReadLine();
        }



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

    public static List<(double, string)> memory = new List<(double, string)>();
    private static void PrintTime(Stopwatch sw, string solution, ref double now, ref double previous)
    {
        now = sw.Elapsed.TotalMilliseconds;
        Console.WriteLine("{0}\t ({1} ms totaal) \t{2}", FixedLenght(Math.Round(now - previous, 1).ToString() + " ms", 8), FixedLenght(Math.Round(now).ToString(), 5), solution);

        memory.Add((Math.Round(now - previous,2), solution.Split(':').First() + ":"));

        previous = now;
    }

    private static string FixedLenght(string input, int max)
    {
        int lel = max - input.Length;
        for (int i = 0; i < lel; i++)
        {
            input += " ";
        }
        return input;
    }
}

