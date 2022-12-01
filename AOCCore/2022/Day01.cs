using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore._2022;

class Day01 : Day
{
    public Day01()
    {
        GetInput("2022", "01");

    }

    public override void Part1(List<string> Lines)
    {
        List<int> sums = new List<int>();    
        int sum = 0;    
        for (int i = 0; i < Lines.Count; i++)
        {
            string line = Lines[i];
            if (line == "")
            {
                sums.Add(sum);
                sum = 0;
            }
            else {

                sum += int.Parse(line);
            }
        }
        Console.WriteLine(sums.Max());
        Console.WriteLine(sums.OrderByDescending(x=> x).Take(3).Sum());
    }
}
