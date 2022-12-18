using Google.Protobuf.Collections;
using System.Collections.Generic;

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
        List<string> subset = new List<string>();
        var monkeys = new List<Monkey>();
        foreach (var line in Lines)
        {
            if (line == "")
            {
                monkeys.Add(new Monkey(subset));
                subset = new List<string>();
            }
            else
                subset.Add(line);
        }
        monkeys.Add(new Monkey(subset));
        //  monkeys = monkeys.OrderByDescending(x => x.divTest).ToList();
        for (int i = 0; i < monkeys.Count; i++)
        {
            monkeys[i].Index = i;
        }
        long magicNumber = monkeys.Select(x => x.divTest).Aggregate(1, (a, b) => a * b);
        var divs = monkeys.Select(x => x.divTest).OrderBy(x => x).ToList();
        monkeys.ForEach(x => x.SetCoolNumbers(divs, magicNumber));
        for (int i = 0; i < 10000; i++)
        {
            foreach (var Monkey in monkeys)
            {
                foreach (var number in Monkey.coolNumber)
                {
                    number.Print();
                    var f = Monkey.Function;
                    number.AddFunction(f);
                    Monkey.Inspected++;
                    number.Print();
                    // Console.WriteLine("{0} {1} {2}", Monkey.Index, Monkey.MTrue, Monkey.MFalse);
                    var index = number.GetNumber(Monkey.divTest) == 0 ? Monkey.MTrue : Monkey.MFalse;
                    monkeys[index].coolNumber.Add(number);
                }
                Monkey.coolNumber = new();
            }
        }
        var orderedMonkeys = monkeys.OrderByDescending(x => x.Inspected).ToArray();
        Console.WriteLine((long)orderedMonkeys[0].Inspected * (long)orderedMonkeys[1].Inspected);
    }

    public class CoolNumber
    {
        public readonly List<int> PrimeNumbers;
        public readonly List<int> TinyNumbers;

        public long DUMBNUMBER;
        public long MagicNumber;
        public CoolNumber(List<int> primeNumbers, int number, long magicNumber)
        {
            MagicNumber = magicNumber;
            DUMBNUMBER = number;
            this.PrimeNumbers = primeNumbers.ToList();
            TinyNumbers = primeNumbers.Select(x => number).ToList();
            AddFunction(x => x);
        }

        public void AddFunction(Func<long, long> f)
        {
            for (int i = 0; i < PrimeNumbers.Count; i++)
            {
                TinyNumbers[i] = (int)f(TinyNumbers[i]) % PrimeNumbers[i];
            }
            DUMBNUMBER = f(DUMBNUMBER) % MagicNumber;
        }
        public int GetNumber(int divTest)
        {
            return TinyNumbers[PrimeNumbers.IndexOf(divTest)];
        }

        public void Print()
        {
            "".P();
            Console.WriteLine("Large Number {0}", DUMBNUMBER);
            string line = "Tiny equvalent:";
            for (int i = 0; i < PrimeNumbers.Count; i++)
            {
                line += string.Format(" {0} (klok {1})  ", TinyNumbers[i], PrimeNumbers[i]);
            }
            line.P();

            for (int i = 0; i < PrimeNumbers.Count; i++)
            {
                Console.WriteLine("{0} % {1} = {2}", DUMBNUMBER, PrimeNumbers[i], DUMBNUMBER % PrimeNumbers[i]);
            }
            Console.ReadLine();
        }
    }

    public class Monkey
    {
        private List<int> Sadnumbers = new List<int>();
        string operatorM = "";
        public List<int> numbers = new();
        public List<CoolNumber> coolNumber = new();

        public int divTest = 0;
        public int MTrue = 0;
        public int MFalse = 0;
        public int Inspected = 0;
        public int Index = -1;
        public void SetCoolNumbers(List<int> divs, long magic)
        {
            foreach (var number in Sadnumbers)
            {
                coolNumber.Add(new CoolNumber(divs, number, magic));
            }
        }
        public Monkey(List<string> lines)
        {
            Sadnumbers = GetNumbers(lines[1]).Select(x => (int)x).ToList();
            operatorM = lines[2].Split(' ')[6];
            numbers = GetNumbers(lines[2]);
            divTest = GetNumbers(lines[3])[0];
            MTrue = GetNumbers(lines[4])[0];
            MFalse = GetNumbers(lines[5])[0];
            if (operatorM == "+" && numbers.Count == 1) Function = x => x + numbers[0];
            if (operatorM == "*" && numbers.Count == 1) Function = x => x * numbers[0];
            if (operatorM == "-" && numbers.Count == 1) Function = x => x - numbers[0];
            if (operatorM == "+" && numbers.Count == 0) Function = x => x + x;
            if (operatorM == "*" && numbers.Count == 0) Function = x => x * x;
            if (operatorM == "-" && numbers.Count == 0) Function = x => x - x;
        }
        public Func<long, long> Function;


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
