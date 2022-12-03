using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore._2022;

class Day03 : Day
{
    public Day03()
    {
        GetInput("2022", "03");

    }

    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        int sum = 0;
        for (int k = 0; k < Lines.Count; k++)
        {
            var line = Lines[k];
           // line.P();
            HashSet<string> set = new HashSet<string>();
            HashSet<string> set2 = new HashSet<string>();
            for (int i = 0; i < line.Length/2; i++)
            {
                set.Add(line[i].ToString());
            }
            for (int i = line.Length / 2; i < line.Length; i++)
            {
                if (set.Contains(line[i].ToString()) && !set2.Contains(line[i].ToString())) {
                    if (line[i] >= 'a' && line[i] <= 'z')
                    {
                        sum += (int)line[i] - 'a' +1;
                    }
                    if (line[i] >= 'A' && line[i] <= 'Z')
                    {
                        sum += (int)line[i] - 'A'+27;
                    }
                    set2.Add(line[i].ToString());
                }
            }
            //set.ToList().Print(" ");
        }
        Console.WriteLine(sum);

    }
    public override void Part2(List<string> Lines)
    {
        TryParse(Lines);
        int sum = 0;
        for (int k = 0; k < Lines.Count - 2; k += 3)
        {
            var line = Lines[k];
            // line.P();
            HashSet<string> set = new HashSet<string>();
            HashSet<string> set2 = new HashSet<string>();
            HashSet<string> set3 = new HashSet<string>();
            for (int i = 0; i < line.Length; i++)
            {
                set.Add(Lines[k][i].ToString());
            }
            for (int i = 0; i < Lines[k + 1].Length; i++)
            {
                set2.Add(Lines[k + 1][i].ToString());
            }
            for (int i = 0; i < Lines[k + 2].Length; i++)
            {
                set3.Add(Lines[k + 2][i].ToString());
            }
            char element = set.Intersect(set2).Intersect(set3).First()[0];

            if (element >= 'a' && element <= 'z')
            {
                sum += (int)element - 'a' + 1;
            }
            if (element >= 'A' && element <= 'Z')
            {
                sum += (int)element - 'A' + 27;
            }

            //set.ToList().Print(" ");
        }
        Console.WriteLine(sum);

    }
}
