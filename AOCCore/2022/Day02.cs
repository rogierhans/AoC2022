using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore._2022;

class Day02 : Day
{
    public Day02()
    {
        GetInput("2022", "02");

    }

    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        int sum = 0;
        for (int i = 0; i < Lines.Count; i++)
        {

            var elf = Lines[i][0];
            var you = Lines[i][2];
            if (elf == 'A')
            {
                if (you == 'X') {
                    sum += 1 + 3;
                }
                else if (you == 'Y')
                {
                    sum += 2 + 6;
                }
                else if(you == 'Z')
                {
                    sum += 3 + 0;
                }

            }
            else if (elf == 'B')
            {
                if (you == 'X')
                {
                    sum += 1 + 0;
                }
                else if (you == 'Y')
                {
                    sum += 2 + 3;
                }
                else if (you == 'Z')
                {
                    sum += 3 + 6;
                }
            }
            else if (elf == 'C')
            {
                if (you == 'X')
                {
                    sum += 1 + 6;
                }
                else if (you == 'Y')
                {
                    sum += 2 + 0;
                }
                else if(you == 'Z')
                {
                    sum += 3 + 3;
                }
            }

        }
        Console.WriteLine(sum);

    }
    public override void Part2(List<string> Lines)
    {
        TryParse(Lines);
        int sum = 0;
        for (int i = 0; i < Lines.Count; i++)
        {

            var elf = Lines[i][0];
            var you = Lines[i][2];
            if (elf == 'A')
            {
                if (you == 'X')
                {
                    sum += 0 + 3;
                }
                else if (you == 'Y')
                {
                    sum += 3 + 1;
                }
                else if (you == 'Z')
                {
                    sum += 6 + 2;
                }

            }
            else if (elf == 'B')
            {
                if (you == 'X')
                {
                    sum += 0 + 1;
                }
                else if (you == 'Y')
                {
                    sum += 3 + 2;
                }
                else if (you == 'Z')
                {
                    sum += 6 + 3;
                }
            }
            else if (elf == 'C')
            {
                if (you == 'X')
                {
                    sum += 0 + 2;
                }
                else if (you == 'Y')
                {
                    sum += 3 + 3;
                }
                else if (you == 'Z')
                {
                    sum += 6 + 1;
                }
            }

        }
        Console.WriteLine(sum);

    }
}
