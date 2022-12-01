using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AOCCore._2017;

class Day02 : Day
{
    public Day02()
    {
        GetInput("2017", "02");

    }

    public override void Part1(List<string> Lines)
    {

        var linesNumbers = Lines.Select(x => x.Split('\t').Select(int.Parse).ToList());
        int sum = 0;
        foreach (var line in linesNumbers)
        {
            for (int i = 0; i < line.Count; i++)
            {
                for (int j = i+1; j < line.Count; j++)
                {
                    
                    sum += Test(line[i], line[j]) + Test(line[j], line[i]);

                }
            }
        }
        Console.WriteLine(sum);
        return  ;
    }


    public int Test(int first, int second) {
        if (first % second == 0) {
            return first / second;  
        }
        return 0;
    }   
    

    //return Gretest common devisor 

    public int GCD(int first, int second)
    {
        while (first != second)
        {
            if (first > second)
                first -= second;
            else
                second -= first;
        }
        return first;
    }
}
