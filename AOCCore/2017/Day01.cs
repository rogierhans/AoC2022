using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore._2017;

class Day01 : Day
{
    public Day01()
    {
        GetInput("2017", "01");

    }

    public override void Part1(List<string> Lines)
    {
        var digits = Lines.First().List().Select(int.Parse).ToList(); ;
        long sum = 0;
        for (int i = 0; i < digits.Count(); i++)
        {
            int indexNext = (i + (digits.Count())/2 + digits.Count()) % digits.Count();
            if (digits[i] == digits[indexNext]) sum += digits[i];
        }
        Console.WriteLine(sum);
        return;
    }
}
