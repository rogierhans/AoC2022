using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore._2022;

class Day04 : Day
{
    public Day04()
    {
        GetInput("2022", "04");

    }

    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        int overlaps = 0;
        int subsets = 0;
        foreach (var numbers in NumberedRows)
        {
            var numbers1 = new List<int>();
            var numbers2 = new List<int>();

            for (int number = numbers[0]; number <= numbers[1]; number++)
            {
                numbers1.Add(number);
            }
            for (int number = numbers[2]; number <= numbers[3]; number++)
            {
                numbers2.Add(number);
            }
            if (numbers1.Intersect(numbers2).Count() > 0)
            {
                overlaps++;
            }

            if (numbers1.Intersect(numbers2).Count() == numbers1.Count() ||
                numbers1.Intersect(numbers2).Count() == numbers2.Count())
            {
                subsets++;
            }
        }



        Console.WriteLine(subsets);
        Console.WriteLine(overlaps);

    }
}
