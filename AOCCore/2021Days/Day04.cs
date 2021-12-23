using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2
{
    class Day4 : Day
    {


        public Day4()
        {

            string folder = @"C:\Users\Rogier\Desktop\AOC\";
            string name = "input.txt";
            string filename = folder + name;
            string filenameTest = folder + "test.txt";
            var testLines = File.ReadAllLines(filenameTest).ToList();
            var inputLines = File.ReadAllLines(filename).ToList();
            Console.WriteLine("test:");
            ParseLines(testLines);
            Console.WriteLine("input:");
            ParseLines(inputLines);
        }

        private static void ParseLines(List<string> lines)
        {
            var clusterLine = lines.ClusterLines();
            var parsed = clusterLine;
            var numbers = parsed.First().First().Split(',').Select(x => long.Parse(x)).ToList();
            var bingocard = parsed.Skip(1).Select(line => new Element(line)).ToList();


            var done = new List<long>();
            for (int i = 0; i < numbers.Count; i++)
            {
                done.Add(numbers[i]);
                for (int b = 0; b < bingocard.Count; b++)
                {
                    if (bingocard[b].Bingo(done)) {
                        Console.WriteLine("{0} {1}", numbers[i], bingocard[b].Score(done));
                        Console.WriteLine(numbers[i] * bingocard[b].Score(done));
                        bingocard.Remove(bingocard[b]);
                    }
                    
                }
            }
            Console.ReadLine();


        }

        class Element
        {
            public List<List<long>> numbers = new();
            public List<List<long>> numbers2 = new();
            public Element(List<string> lines)
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
               return  numbers.Sum(row => row.Where(x => !done.Contains(x)).Sum());
            }

            private void ParseMulti(List<string> lines)
            {
                SL.Line();
                for (int i = 0; i < lines.Count; i++)
                {
                    var line = lines[i];
                    SL.Log(line);

                    var sperator = ' ';

                    var newline = line.ToCharArray()[0] == ' ' ? line.Trim(1, 0) : line;
                    var input = newline.Replace("  ", " ").Split(sperator).Select(x =>
                    {
                        //Console.WriteLine(x);
                        return long.Parse(x);
                    }).ToList();
                    numbers.Add(input);

                }
                numbers2 = numbers.Transpose();
            }
        }

    }
}
