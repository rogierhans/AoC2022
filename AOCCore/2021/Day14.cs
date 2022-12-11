﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2021;

class Day14 : Day
{


    public Day14()
    {

        GetInput(RootFolder + @"2021_14\");
    }
    public const string BLOCK = "\U00002588";
    public override void Part1(List<string> Lines)
    {

      PrintSolution(Simulate(Lines, 10), "3406", "part 1");
    }
    public override void Part2(List<string> Lines)
    {

      PrintSolution(Simulate(Lines, 40), "3941782230241", "part 2");
    }
    private long Simulate(List<string> Lines, int iterations)
    {
        var begin = Lines.First().List();
        var rules = Lines.FindPatterns("{0} -> {1}", x => x, x => x);

        (int, int)[] otherIndex = new (int, int)[rules.Count];
        Dictionary<string, int> stringToIndex = new Dictionary<string, int>();
        for (int i = 0; i < rules.Count; i++)
        {
            stringToIndex[rules[i].Item1] = i;
        }
        for (int i = 0; i < rules.Count; i++)
        {
            var (a, b) = rules[i];
            otherIndex[i] = (stringToIndex[a.First() + b], stringToIndex[b + a.Last()]);
        }

        long[] bucket = new long[rules.Count];
        for (int i = 0; i < begin.Count - 1; i++)
        {
            string key = begin[i] + begin[i + 1];
            bucket[stringToIndex[key]]++;
        }

        for (int k = 0; k < iterations; k++)
        {
            long[] newBucket = new long[rules.Count];
            for (int i = 0; i < newBucket.Length; i++)
            {
                newBucket[otherIndex[i].Item1] += bucket[i];
                newBucket[otherIndex[i].Item2] += bucket[i];
            }
            bucket = newBucket;
        }


        Dictionary<string, long> count = new Dictionary<string, long>();
        for (int i = 0; i < bucket.Length; i++)
        {
            var letter1 = rules[i].Item1.List()[0];
            var letter2 = rules[i].Item1.List()[1];
            if (!count.ContainsKey(letter1)) count[letter1] = 0;
            count[letter1] += bucket[i];
            if (!count.ContainsKey(letter2)) count[letter2] = 0;
            count[letter2] += bucket[i];
        }
        var numbers = count.Select(kvp => kvp.Value).Select(x => x % 2 == 0 ? x / 2 : (x + 1) / 2);

        return numbers.Max() - numbers.Min();
    }

}

