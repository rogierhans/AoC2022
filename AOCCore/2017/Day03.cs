using Google.OrTools.LinearSolver;
using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AOCCore._2017;

class Day03 : Day
{
    public Day03()
    {
        GetInput("2017", "03");

    }

    public override void Part1(List<string> Lines)
    {
        int distance_X = 0;
        int distance_Y = 0;
        int counter = 0;
        int maxIteration = 1;
        int mode = 0;
        for (int i = 1; i < 361527; i++)
        {
            if (mode == 0)
            {
                distance_X++;
            }
            if (mode == 1)
            {
                distance_Y++;
            }
            if (mode == 2)
            {
                distance_X--;
            }
            if (mode == 3)
            {
                distance_Y--;
            }
            if (counter++ >= maxIteration / 2)
            {
                counter = 0;
                maxIteration++;
                mode = (mode + 1) % 4;
            }
        }
        Console.WriteLine("{0} {1}", distance_X, distance_Y);
        Console.WriteLine(Math.Abs(distance_X) + Math.Abs(distance_Y));
        return ;
    }
    public override void Part2(List<string> Lines)
    {
        var array = new int[1000, 1000];
        int distance_X = 0;
        int distance_Y = 0;
        int counter = 0;
        int otherCounter = 0;
        int maxIteration = 0;

        int mode = 0;
        array[500, 500] = 1;
        for (int i = 1; i < 361527; i++)
        {
            int sum = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    sum += array[500 + distance_X + dx, 500 + distance_Y + dy];
                }
            }
            array[500 + distance_X, 500+ distance_Y] = sum;
            //Console.WriteLine(array[500 + distance_X, 500 + distance_Y]);
            //Console.WriteLine("X:{0} Y:{1}", distance_X,distance_Y);
            //Console.ReadLine();
            if (mode == 0)
            {
                distance_X++;
            }
            if (mode == 1)
            {
                distance_Y++;
            }
            if (mode == 2)
            {
                distance_X--;
            }
            if (mode == 3)
            {
                distance_Y--;
            }
            if (counter++ >= maxIteration)
            {
                counter = 0;
                if (otherCounter++ >= 1)
                {
                    otherCounter = 0;
                    maxIteration++;

                }
                mode = (mode + 1) % 4;
            }
            if (sum > 361527)
            {
                Console.WriteLine(sum);
                Console.ReadLine();
            }
           // Console.WriteLine("{0} {1} {2} {3}", mode,counter,otherCounter,maxIteration);
        }
        return ;
    }
}

