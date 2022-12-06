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
        Blocks[0].Reverse();
        Stack<string>[] stacks = new Stack<string>[(Lines.First().Count() + 1) / 4];
        for (int i = 0; i < stacks.Length; i++)
        {
            stacks[i] = new Stack<string>();
        }
        for (int i = 1; i < Blocks[0].Count; i++)
        {
            for (int stack = 0; stack < Blocks[0][i].Length; stack+=4)
            {
                if (Blocks[0][i][stack+1] != ' ')
                {
                    stacks[stack/4].Push(Blocks[0][i][stack+1].ToString());
                }
            }
        }
        foreach (var line in Blocks[1].Where(x => x!= ""))
        {
            var numbers = GetNumbers(line);
            for (int i = 0; i < numbers[0]; i++)
            {
                stacks[numbers[2] - 1].Push(stacks[numbers[1] - 1].Pop());
            }
        };
        stacks.ToList().Select(x => x.ToList().Last()).ToList().Print();
    }
    public override void Part2(List<string> Lines)
    {
        TryParse(Lines);
        Blocks[0].Reverse();
        Stack<string>[] stacks = new Stack<string>[(Lines.First().Count() + 1) / 4];
        for (int i = 0; i < stacks.Length; i++)
        {
            stacks[i] = new Stack<string>();
        }
        for (int i = 1; i < Blocks[0].Count; i++)
        {
            for (int stack = 0; stack < Blocks[0][i].Length; stack += 4)
            {
                if (Blocks[0][i][stack + 1] != ' ')
                {
                    stacks[stack / 4].Push(Blocks[0][i][stack + 1].ToString());
                }
            }
        }

        foreach (var line in Blocks[1].Where(x => x != ""))
        {
            var numbers = GetNumbers(line);
            List<string> Pops = new List<string>();
            for (int i = 0; i < numbers[0]; i++)
            {
                Pops.Add(stacks[numbers[1] - 1].Pop());
            }
            Pops.Reverse();
            Pops.ForEach(x => stacks[numbers[2] - 1].Push(x));
        }
        stacks.ToList().Select(x => x.ToList().Last()).ToList().Print();
    }
}
