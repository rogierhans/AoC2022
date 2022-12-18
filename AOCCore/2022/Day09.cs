using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AOCCore._2022;

class Day09 : Day
{
    public Day09()
    {
        GetInput("2022", "09");

    }

    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        Dictionary<(int, int), int> dict = new Dictionary<(int, int), int>();
        int length = 10;
        int[] RopeX = new int[length];
        int[] RopeY = new int[length];
        for (int i = 0; i < Lines.Count; i++)
        {
            var input = Lines[i].Split(' ');
            var direction = input[0];
            for (int k = 0; k < int.Parse(input[1]); k++)
            {
                (RopeX[0], RopeY[0]) = Move(direction, RopeX[0], RopeY[0]);
                for (int r = 0; r < length - 1; r++)
                {

                    (RopeX[r + 1], RopeY[r + 1]) = Update(direction, RopeX[r], RopeY[r], RopeX[r + 1], RopeY[r + 1]);
                }


                dict[(RopeX.Last(), RopeY.Last())] = 1;
            }
        }
        dict.Values.Sum().P();
        Console.ReadLine();

    }
    public (int, int) Move(string direction, int HX, int HY)
    {
        if (direction == "R")
        {
            HX += 1;

        }
        if (direction == "L")
        {
            HX -= 1;

        }
        if (direction == "D")
        {
            HY += 1;

        }
        if (direction == "U")
        {
            HY -= 1;
        }
        return (HX, HY);
    }

    public ( int, int) Update(string direction, int HX, int HY, int TailX, int TailY)
    {
        int dx = HX - TailX;
        int dy = HY - TailY;
        if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
        {
            TailX += Math.Min(1, Math.Max(dx, -1));
            TailY += Math.Min(1, Math.Max(dy,-1));
        }


        return (TailX, TailY);
    }

}
