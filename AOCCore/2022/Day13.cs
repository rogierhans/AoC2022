//using Priority_Queue;
using Microsoft.FSharp.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AOCCore._2022.Day13;
using static System.Reflection.Metadata.BlobBuilder;

namespace AOCCore._2022;

class Day13 : Day
{
    public Day13()
    {
        GetInput("2022", "13");

    }

    //part1 
    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        int sum = 1;
        List<NestedList<int>> Packages = new List<NestedList<int>>();
        for (int i = 0; i < Blocks.Count; i++)
        {
            var block = Blocks[i];
            var left = new NestedList<int>(block[0], x => int.Parse(x), '[', ']');
            var right = new NestedList<int>(block[1], x => int.Parse(x), '[', ']');
            var test = Compare(left, right);
            Packages.Add(left);
            Packages.Add(right);
        }
        Packages.Add(new NestedList<int>("[[2]]", x => int.Parse(x), '[', ']'));
        Packages.Add(new NestedList<int>("[[6]]", x => int.Parse(x), '[', ']'));
        int counter = 1;
        while (Packages.Count() > 1)
        {
            var lowest = Packages.First();
            for (int i = 1; i < Packages.Count; i++)
            {
                if (Compare(lowest, Packages[i]) == 1)
                {
                    lowest = Packages[i];
                }
            }
            if (lowest.Stacking() == "[[2]]" || lowest.Stacking() == "[[6]]")
                sum *= counter;
            counter++;
            Packages.Remove(lowest);
        }
        sum.P();
        Console.ReadLine();
    }

    public int Compare(NestedList<int> left, NestedList<int> right)
    {
        if (!left.isList && !right.isList)
        {
            return left.number.CompareTo(right.number);
        }
        else if (left.isList && right.isList)
        {
            for (int i = 0; i < Math.Min(left.Childeren.Count, right.Childeren.Count); i++)
            {
                if (Compare(left.Childeren[i], right.Childeren[i]) != 0)
                    return Compare(left.Childeren[i], right.Childeren[i]);
            }
            return left.Childeren.Count.CompareTo(right.Childeren.Count);
        }
        else if (left.isList && !right.isList)
        {
            return Compare(left, new NestedList<int>("[" + right.number + "]", x => int.Parse(x), '[', ']'));
        }
        else
        {
            return Compare(new NestedList<int>("[" + left.number + "]", x => int.Parse(x), '[', ']'), right);
        }
    }
    public class NestedList<T>
    {
        public List<NestedList<T>> Childeren = new List<NestedList<T>>();
        public bool isList = false;
        public T number;
#pragma warning disable CS8618 
        public NestedList(string line, Func<string, T> leafParse, char BracketLeft, char BracketRight)
#pragma warning restore CS8618 
        {
            if (line == "" + BracketLeft + BracketRight)
            { isList = true; }
            else if (line[0] == BracketLeft)
            {
                isList = true;
                var trimmedLine = line.Trim(1, 1);
                string substring = "";
                int level = 0;
                for (int i = 0; i < trimmedLine.Length; i++)
                {
                    if ((trimmedLine[i] == ',' && level == 0))
                    {
                        Childeren.Add(new NestedList<T>(substring, leafParse, BracketLeft, BracketRight));
                        substring = "";
                    }
                    else if (trimmedLine[i] == BracketLeft)
                    {
                        level++;
                        substring += trimmedLine[i];
                    }
                    else if (trimmedLine[i] == BracketRight)
                    {
                        level--;
                        substring += trimmedLine[i];
                    }
                    else
                    {
                        substring += trimmedLine[i];
                    }
                }
                Childeren.Add(new NestedList<T>(substring, leafParse, BracketLeft, BracketRight));
            }
            else
            {
                number = leafParse(line);
            }

        }

        public string Stacking()
        {
#pragma warning disable CS8603 
#pragma warning disable CS8602 
            if (!isList) { return number.ToString(); }
#pragma warning restore CS8602
#pragma warning restore CS8603
            string test = "[";
            test += string.Join(",", Childeren.Select(x => x.Stacking()).ToList());
            test += "]";
            return test;

        }


    }
}
