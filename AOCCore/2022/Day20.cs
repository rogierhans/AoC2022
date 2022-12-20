//using Priority_Queue;
using Google.OrTools.LinearSolver;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace AOCCore._2022;

class Day20 : Day
{
    public Day20()
    {
        GetInput("2022", "20");

    }
    public override void Part1(List<string> Lines)
    {

        List<NumberInLoop> numbers = new List<NumberInLoop>();
        {
            var numberss = Lines.Select(long.Parse).ToArray();
            for (int i = 0; i < numberss.Count(); i++)
            {
                numbers.Add(new NumberInLoop(numberss[i]));
            }
        }
        for (int i = 0; i < numbers.Count; i++)
        {
            int indexNext = (i + 1) % numbers.Count;
            numbers[i].Next = numbers[indexNext];
            numbers[indexNext].Prev = numbers[i];
        }

        int max = numbers.Count;
        for (int ROUND = 0; ROUND < 10; ROUND++)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                var Place = numbers[i].Value;
                var currentNumber = numbers[i];
                if (Place > 0)
                {
                    int iterations = (int) ((Math.Abs(Place) * (long) 811589153) % (max-1));
                    for (int h = 0; h < iterations; h++)
                    {

                        var next = currentNumber.Next;
                        var prev = currentNumber.Prev;
                        var nextnext = next.Next;
                        prev.Next = next;
                        next.Prev = prev;

                        nextnext.Prev = currentNumber;
                        currentNumber.Next = nextnext;

                        next.Next = currentNumber;
                        currentNumber.Prev = next;
                    }
                }
                if (Place < 0)
                {
                    int iterations = (int)((Math.Abs(Place) * (long)811589153) % (max-1));
                    for (int h = 0; h < iterations; h++)
                    {
                        var next = currentNumber.Next;
                        var prev = currentNumber.Prev;
                        var prevprev = prev.Prev;

                        prev.Next = next;
                        next.Prev = prev;

                        prevprev.Next = currentNumber;
                        currentNumber.Prev = prevprev;

                        prev.Prev = currentNumber;
                        currentNumber.Next = prev;
                    }
                }

            }

        }
        long sum = 0;
        {
            var nul = numbers.First(x => x.Value == 0);
            for (int i = 0; i < 1000; i++)
            {
                nul = nul.Next;
            }
            sum += nul.Value;
            nul.Value.P();
        }
        {
            var nul = numbers.First(x => x.Value == 0);
            for (int i = 0; i < 2000; i++)
            {
                nul = nul.Next;
            }
            sum += nul.Value;
            nul.Value.P();
        }
        {
            var nul = numbers.First(x => x.Value == 0);
            for (int i = 0; i < 3000; i++)
            {
                nul = nul.Next;
            }
            sum += nul.Value;
            nul.Value.P();
        }
        (sum * 811589153).P();
        sum.P();
        //Console.WriteLine(Number[])
        Console.ReadLine();
    }
}


class NumberInLoop
{
    public readonly long Value;
    public NumberInLoop Next;
    public NumberInLoop Prev;
    public NumberInLoop(long value)
    {
        Value = value;
    }

}
