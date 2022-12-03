using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
            var line1 = Lines[k].List();
            var line2 = Lines[k + 1].List();
            var line3 = Lines[k + 2].List();
            
            char element = line1.Intersect(line2).Intersect(line3).First()[0];

            if (element >= 'a' && element <= 'z')
            {
                sum += element - 'a' + 1;
            }
            if (element >= 'A' && element <= 'Z')
            {
                sum += element - 'A' + 27;
            }
        }
        Console.WriteLine(sum);

    }
}
