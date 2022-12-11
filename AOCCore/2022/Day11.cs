using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace AOCCore._2022;

class Day11 : Day
{
    public Day11()
    {
        GetInput("2022", "11");

    }

    //part1 
    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        var monkeys = Blocks.Select(x => new Monkey(x)).ToList();
        long magicNumber = monkeys.Select(x => x.divTest).Aggregate(1, (a, b) => a * b);
        for (int i = 0; i < 10000; i++)
        {
            foreach (var Monkey in monkeys)
            {
                foreach (var item in Monkey.Items)
                {
                    long newNumber = Monkey.NewNumber(item);
                    newNumber %= magicNumber;
                    Monkey.Inspected++;
                    var index = newNumber % Monkey.divTest == 0 ? Monkey.MTrue : Monkey.MFalse;
                    monkeys[index].Items.Add(newNumber);
                }
                Monkey.Items = new List<long>();
            }
        }
        var orderedMonkeys = monkeys.OrderByDescending(x => x.Inspected).ToArray();
        Console.WriteLine(orderedMonkeys[0].Inspected * orderedMonkeys[1].Inspected);
    }

    public class Monkey
    {
        public List<long> Items = new List<long>();
        string operatorM = "";
        List<int> number = new ();

        public int divTest = 0;
        public int MTrue = 0;
        public int MFalse = 0;
        public long Inspected = 0;
        public Monkey(List<string> lines)
        {
            Items = GetNumbers(lines[1]).Select(x => (long)x).ToList();
            operatorM = lines[2].Split(' ')[6];
            number = GetNumbers(lines[2]);
            divTest = GetNumbers(lines[3])[0];
            MTrue = GetNumbers(lines[4])[0];
            MFalse = GetNumbers(lines[5])[0];
        }
        public long NewNumber(long input)
        {
            if (operatorM == "+" && number.Count == 1) return input + number[0];
            if (operatorM == "*" && number.Count == 1) return input * number[0];
            if (operatorM == "-" && number.Count == 1) return input - number[0];
            if (operatorM == "+" && number.Count == 0) return input + input;
            if (operatorM == "*" && number.Count == 0) return input * input;
            if (operatorM == "-" && number.Count == 0) return input - input;
            {
                throw new Exception();
            }
        }


        public List<int> GetNumbers(string line)
        {
            List<int> list = new List<int>();
            string number = "";
            for (int i = 0; i < line.Length; i++)
            {
                if ((number == "") && (line[i] == '-'))
                {
                    number = "-";
                }
                else if (line[i] >= '0' && line[i] <= '9')
                {
                    number += line[i];
                }
                else if (number.Length > 0 && number != "-")
                {
                    _ = int.TryParse(number, out int parsedNumber);
                    list.Add(parsedNumber);
                    number = "";
                }
                else
                {
                    number = "";
                }
            }
            if (number.Length > 0 && number != "-")
            {

                _ = int.TryParse(number, out int parsedNumber);
                list.Add(parsedNumber);

            }
            return list;
        }
    }


}
