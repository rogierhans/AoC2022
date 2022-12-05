using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore._2022;

class Day05 : Day
{
    public Day05()
    {
        GetInput("2022", "05");

    }

    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        var Rows = Blocks[0];
        int max = (Lines.First().Count()+1)/4;
        List<Stack<string>> stacks = new List<Stack<string>>();
        for (int i = 0; i < max; i++)
        {
            stacks.Add(new Stack<string>());
        }
        for (int i = Rows.Count - 2; i >= 0; i--)
        {
            for (int stack = 0; stack < Rows[i].Length; stack+=4)
            {
                if (Rows[i][stack+1] != ' ')
                {
                    stacks[stack/4].Push(Rows[i][stack+1].ToString());
                }
            }
        }

        foreach (var line in Blocks[1])
        {

            if (line == "") continue;
            var numbers = GetNumbers(line);
            int amount = numbers[0];
            int index1 = numbers[1] - 1;
            int index2 = numbers[2] - 1;
            List<string> Pops = new List<string>();   
            for (int i = 0; i < amount; i++)
            {
               Pops.Add(stacks[index1].Pop());

            }
            for (int i = 0; i < Pops.Count; i++)
            {
                stacks[index2].Push(Pops[i]);
            }
        }
        stacks.ToList().Select(x => x.ToList().First()).ToList().Print();
    }
    public override void Part2(List<string> Lines)
    {
        TryParse(Lines);
        var Rows = Blocks[0];
        int max = (Lines.First().Count() + 1) / 4;
        List<Stack<string>> stacks = new List<Stack<string>>();
        for (int i = 0; i < max; i++)
        {
            stacks.Add(new Stack<string>());
        }
        for (int i = Rows.Count - 2; i >= 0; i--)
        {
            for (int stack = 0; stack < Rows[i].Length; stack += 4)
            {
                if (Rows[i][stack + 1] != ' ')
                {
                    stacks[stack / 4].Push(Rows[i][stack + 1].ToString());
                }
            }
        }


        foreach (var line in Blocks[1])
        {

            if (line == "") continue;
            var numbers = GetNumbers(line);
            int amount = numbers[0];
            int index1 = numbers[1] - 1;
            int index2 = numbers[2] - 1;
            List<string> Pops = new List<string>();
            for (int i = 0; i < amount; i++)
            {
                Pops.Add(stacks[index1].Pop());

            }
            for (int i = Pops.Count - 1; i >= 0; i--)
            {
                stacks[index2].Push(Pops[i]);
            }
        }
        stacks.ToList().Select(x => x.ToList().First()).ToList().Print();
    }
}
