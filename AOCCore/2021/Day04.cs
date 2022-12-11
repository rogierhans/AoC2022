using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AOC2021;

class Day4 : Day
{
    public Day4()
    {
        GetInput(RootFolder + @"2021_04\");
    }


    public override void Part1(List<string> Lines)
    {
        var clusterLine = Lines.ClusterLines();
        var parsed = clusterLine;
        var numbers = parsed.First().First().Split(',').Select(x => long.Parse(x)).ToList();
        var bingocard = parsed.Skip(1).Select(line => new BingoCard(line)).ToList();

        var done = new List<long>();
        for (int i = 0; i < numbers.Count; i++)
        {
            done.Add(numbers[i]);
            for (int b = 0; b < bingocard.Count; b++)
            {
                if (bingocard[b].Bingo(done))
                {
                  PrintSolution(numbers[i] * bingocard[b].Score(done), "58838", "part 1");
                }

            }
        }
        throw new Exception();
    }
    public override void Part2(List<string> Lines)
    {
        var clusterLine = Lines.ClusterLines();
        var parsed = clusterLine;
        var numbers = parsed.First().First().Split(',').Select(x => long.Parse(x)).ToList();
        var bingocard = parsed.Skip(1).Select(line => new BingoCard(line)).ToList();
        var done = new List<long>();
        for (int i = 0; i < numbers.Count; i++)
        {
            done.Add(numbers[i]);
            for (int b = 0; b < bingocard.Count; b++)
            {
                if (bingocard[b].Bingo(done))
                {
                    if (bingocard.Count == 1)
                      PrintSolution(numbers[i] * bingocard[b].Score(done), "6256", "part 2");
                    bingocard.Remove(bingocard[b]);
                }

            }
        }
        throw new Exception();
    }

    private class BingoCard
    {
        public List<List<long>> numbers = new();
        public List<List<long>> numbers2 = new();
        public BingoCard(List<string> lines)
        {
            ParseMulti(lines);
        }


        public bool Bingo(List<long> done)
        {
            bool bingo1 = false;
            bool bingo2 = false;
            for (int i = 0; i < numbers.Count; i++)
            {
                bool bingo = true;
                for (int j = 0; j < numbers[0].Count; j++)
                {
                    bingo &= done.Contains(numbers[i][j]);
                }
                bingo1 |= bingo;
            }

            for (int i = 0; i < numbers2.Count; i++)
            {
                bool bingo = true;
                for (int j = 0; j < numbers2[0].Count; j++)
                {
                    bingo &= done.Contains(numbers2[i][j]);
                }
                bingo2 |= bingo;
            }
            return bingo1 || bingo2;
        }

        public long Score(List<long> done)
        {
            return numbers.Sum(row => row.Where(x => !done.Contains(x)).Sum());
        }

        private void ParseMulti(List<string> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var sperator = ' ';
                var newline = line.ToCharArray()[0] == ' ' ? line.Trim(1, 0) : line;
                var input = newline.Replace("  ", " ").Split(sperator).Select(x =>
                {
                    return long.Parse(x);
                }).ToList();
                numbers.Add(input);

            }
            numbers2 = numbers.Transpose();
        }
    }

}