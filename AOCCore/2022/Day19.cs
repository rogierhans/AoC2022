//using Priority_Queue;
using Google.OrTools.LinearSolver;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace AOCCore._2022;

class Day19 : Day
{
    public Day19()
    {
        GetInput("2022", "19");

    }

    public override void Part1(List<string> Lines)
    {
        var BluePrints = new List<List<(int, int, int, int, int)>>();
        foreach (var line in Lines)
        {
            var trimmedLine = line.Split(".").Select(x => x.Replace("Each", "")
            .Replace("robot", "")
            .Replace("costs", "")
            .Replace("Each", "")).ToList();
            for (int i = 0; i < 100; i++)
            {
                trimmedLine = trimmedLine.Select(x => x.Replace("Blueprint " + i + ": ", " ").Replace("  ", " ")).ToList();
            }
            var ingredients = new List<(int, int, int, int, int)>();
            for (int i = 0; i < 4; i++)
            {
                var thing = trimmedLine[i];
                var input = thing.Split(' ');
                var name = input[1];
                int oreNumber = 0;
                int clayNumber = 0;
                int obsNumber = 0;
                int geoNumber = 0;
                if (input[3] == "ore") oreNumber += int.Parse(input[2]);
                if (input[3] == "clay") clayNumber += int.Parse(input[2]);
                if (input[3] == "obsidian") obsNumber += int.Parse(input[2]);
                if (input[3] == "geode") geoNumber += int.Parse(input[2]);
                if (thing.Contains("and"))
                {
                    if (input[6] == "ore") oreNumber += int.Parse(input[5]);
                    if (input[6] == "clay") clayNumber += int.Parse(input[5]);
                    if (input[6] == "obsidian") obsNumber += int.Parse(input[5]);
                    if (input[6] == "geode") geoNumber += int.Parse(input[5]);
                }
                ingredients.Add((i, oreNumber, clayNumber, obsNumber, geoNumber));

            }
            BluePrints.Add(ingredients);
        }
        int sum = 1;
        foreach (var BluePrint in BluePrints.Take(3))
        {
            // var list = BluePrint.Select(x => x).ToList();
            // list.Reverse();
            var sets = new HashSet<(int, int, int, int, int, int, int, int)>();
            sets.Add((0, 0, 0, 0, 1, 0, 0, 0));
            var maxMaxine = (BluePrint.Max(x => x.Item2), BluePrint.Max(x => x.Item3), BluePrint.Max(x => x.Item4), 200);
            var number = Calc(1, sets, BluePrint, (6, 22, 22, 200), maxMaxine);
            number.P();
            sum *= number;

        }

        sum.P();
    }

    public int Calc(int min, HashSet<(int, int, int, int, int, int, int, int)> set, List<(int, int, int, int, int)> BluePrint, (int, int, int, int) TrimValues, (int, int, int, int) TrimMachine)
    {
        if (min == 33)
        {
            set.Count.P();
            set.MaxItem(x => x.Item4).P();
            return set.Max(x => x.Item4);
        }

        var newSet = new HashSet<(int, int, int, int, int, int, int, int)>();
        foreach (var state in set)
        {

            var (s1, s2, s3, s4, m1, m2, m3, m4) = state;


            for (int index = 3; index >= 0; index--)
            {
                var (_, c1, c2, c3, c4) = BluePrint[index];
                if (s1 - c1 >= 0 && s2 - c2 >= 0 && s3 - c3 >= 0 && s4 - c4 >= 0)
                {
                    var (t1, t2, t3, t4) = TrimValues;
                    if (index == 0 && m1 < TrimMachine.Item1) { newSet.Add(Trim(PayAndUpdate(state, BluePrint[index]), TrimValues, TrimMachine)); }
                    if (index == 1 && m2 < TrimMachine.Item2) { newSet.Add(Trim(PayAndUpdate(state, BluePrint[index]), TrimValues, TrimMachine)); }
                    if (index == 2 && m3 < TrimMachine.Item3) { newSet.Add(Trim(PayAndUpdate(state, BluePrint[index]), TrimValues, TrimMachine)); }
                    if (index == 3) { newSet.Add(Trim(PayAndUpdate(state, BluePrint[index]), TrimValues, TrimMachine)); }

                }
            }
            {
                newSet.Add(Trim(Update(state), TrimValues, TrimMachine));
            }
        }

        return Calc(min + 1, newSet, BluePrint, TrimValues, TrimMachine);
    }
    public (int, int, int, int, int, int, int, int) Trim((int, int, int, int, int, int, int, int) state, (int, int, int, int) TrimValues, (int, int, int, int) TrimMachine)
    {
        var (s1, s2, s3, s4, m1, m2, m3, m4) = state;
        var (t1, t2, t3, t4) = TrimValues;
        var (t1m, t2m, t3m, t4m) = TrimMachine;
        return (s1.M(t1), s2.M(t2), s3.M(t3), s4.M(t4), m1.M(t1m), m2.M(t2m), m3.M(t3m), m4.M(t4m));
    }
    public (int, int, int, int, int, int, int, int) Update((int, int, int, int, int, int, int, int) state)
    {
        var (s1, s2, s3, s4, m1, m2, m3, m4) = state;
        return (s1 + m1, s2 + m2, s3 + m3, s4 + m4, m1, m2, m3, m4);
    }
    public (int, int, int, int, int, int, int, int) PayAndUpdate((int, int, int, int, int, int, int, int) state, (int, int, int, int, int) price)
    {
        var (s1, s2, s3, s4, m1, m2, m3, m4) = state;
        var (index, c1, c2, c3, c4) = price;
        var ns1 = s1 - c1 + m1;
        var ns2 = s2 - c2 + m2;
        var ns3 = s3 - c3 + m3;
        var ns4 = s4 - c4 + m4;
        var nm1 = m1;
        var nm2 = m2;
        var nm3 = m3;
        var nm4 = m4;
        if (index == 0)
            nm1++;
        if (index == 1)
            nm2++;
        if (index == 2)
            nm3++;
        if (index == 3)
            nm4++;
        return (ns1, ns2, ns3, ns4, nm1, nm2, nm3, nm4);
    }
    public (List<int>, List<int>) ToList((int, int, int, int, int, int, int, int) state)
    {
        var (s1, s2, s3, s4, m1, m2, m3, m4) = state;
        return (new List<int>() { s1, s2, s3, s4 }, new List<int>() { m1, m2, m3, m4 });
    }
}

public static class NumberExtenstion
{
    public static int M(this int number, int max)
    {
        return Math.Min(number, max);
    }
}

