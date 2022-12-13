//using Priority_Queue;
using Microsoft.FSharp.Core;
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
            var left = NestedList<int>.Create(block[0], x => int.Parse(x), '[', ']');
            var right = NestedList<int>.Create(block[1], x => int.Parse(x), '[', ']');
            var test = Compare(left, right);
            Packages.Add(left);
            Packages.Add(right);
        }
        Packages.Add(NestedList<int>.Create("[[2]]", x => int.Parse(x), '[', ']'));
        Packages.Add(NestedList<int>.Create("[[6]]", x => int.Parse(x), '[', ']'));
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
            if (lowest.FlatString() == "[[2]]" || lowest.FlatString() == "[[6]]")
                sum *= counter;
            counter++;
            Packages.Remove(lowest);
        }
        sum.P();
        Console.ReadLine();
    }

    public int Compare(NestedList<int> left, NestedList<int> right)
    {

        if (left is NestedListLeaf<int> leftLeaf)
        {
            if (right is NestedListLeaf<int> rightLeaf)
                return leftLeaf.leaf.CompareTo(rightLeaf.leaf);
            else
            {
                return Compare(new NesteListNode<int>("[" + leftLeaf.leaf + "]", x => int.Parse(x), '[', ']'), right);
            }
        }
        else if (left is NesteListNode<int> leftNode)
        {
            if (right is NesteListNode<int> rightNode)
            {
                for (int i = 0; i < Math.Min(leftNode.Childeren.Count, rightNode.Childeren.Count); i++)
                {
                    if (Compare(leftNode.Childeren[i], rightNode.Childeren[i]) != 0)
                        return Compare(leftNode.Childeren[i], rightNode.Childeren[i]);
                }
                return leftNode.Childeren.Count.CompareTo(rightNode.Childeren.Count);
            }
            else if (right is NestedListLeaf<int> rightLeaf)
            {
                return Compare(leftNode, new NesteListNode<int>("[" + rightLeaf.leaf + "]", x => int.Parse(x), '[', ']'));
            }
        }
        throw new Exception();
    }
    public class NestedList<T>
    {

        public static NestedList<T> Create(string line, Func<string, T> leafParse, char BracketLeft, char BracketRight)
        {
            //   Console.WriteLine("->"+line);
            if (line[0] == BracketLeft)
            {
                return new NesteListNode<T>(line, leafParse, BracketLeft, BracketRight);
            }
            else
            {
                return new NestedListLeaf<T>(line, leafParse);
            }
        }
        public string FlatString()
        {
            if (this is NestedListLeaf<T> leaf) { return leaf.leaf.ToString(); }
            else if (this is NesteListNode<T> node)
            {
                string test = "[";
                test += string.Join(",", node.Childeren.Select(x => x.FlatString()).ToList());
                test += "]";
                return test;
            }
            throw new Exception();
        }
    }
    public class NestedListLeaf<T> : NestedList<T>
    {
        public T leaf;
        public NestedListLeaf(string line, Func<string, T> leafParse)
        {
            leaf = leafParse(line);
        }
    }
    public class NesteListNode<T> : NestedList<T>
    {
        public List<NestedList<T>> Childeren = new List<NestedList<T>>();
        public NesteListNode(string line, Func<string, T> leafParse, char BracketLeft, char BracketRight)
        {
            var trimmedLine = line.Trim(1, 1);
            if (trimmedLine == "") return;
            string substring = "";
            int level = 0;
            for (int i = 0; i < trimmedLine.Length; i++)
            {
                if ((trimmedLine[i] == ',' && level == 0))
                {
                    Childeren.Add(Create(substring, leafParse, BracketLeft, BracketRight));
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
            Childeren.Add(Create(substring, leafParse, BracketLeft, BracketRight));
        }




    }
}
