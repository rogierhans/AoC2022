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
        var pictureGrid = new string[20, 20].ToLists();
        for (int i = 0; i < Lines.Count; i++)
        {
            var input = Lines[i].Split(' ');
            var direction = input[0];
            for (int k = 0; k < int.Parse(input[1]); k++)
            {
                (RopeX[0], RopeY[0]) = Move(direction, RopeX[0], RopeY[0]);
                for (int r = 0; r < length - 1; r++)
                {

                    (RopeX[r], RopeY[r], RopeX[r + 1], RopeY[r + 1]) = Update2(direction, RopeX[r], RopeY[r], RopeX[r + 1], RopeY[r + 1]);
                }


                dict[(RopeX.Last(), RopeY.Last())] = 1;
                //Console.WriteLine("###########"); ;
                //Console.WriteLine("({0},{1}) ({2},{3})  {4} {5}", RopeX[0], RopeY[0], RopeX[1], RopeY[1], Math.Abs(RopeX[0] - RopeX[1]) ,Math.Abs(RopeY[0] - RopeY[1]));
                ////pictureGrid = pictureGrid.GridSelect(x => ".");
                //for (int l = 0; l < length; l++)
                //{
                //    pictureGrid[RopeY[l] + 10][RopeX[l] + 10] = l.ToString();

                //}
                //pictureGrid.Print();
                //Console.ReadLine();
                //for (int l = 0; l < length; l++)
                //{
                //    pictureGrid[RopeY[l] + 10][RopeX[l] + 10] = "x";
                //    //.Print();
                //}
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
    public (int, int, int, int) Update1(string direction, int HX, int HY, int TailX, int TailY)
    {
        if (direction == "R")
        {
            if (Math.Abs(HX - TailX) > 1 || Math.Abs(HY - TailY) > 1)
            {
                TailX = HX - 1;
                TailY = HY;
            }
        }
        if (direction == "L")
        {
            if (Math.Abs(HX - TailX) > 1 || Math.Abs(HY - TailY) > 1)
            {
                TailX = HX + 1;
                TailY = HY;
            }
        }
        if (direction == "D")
        {
            if (Math.Abs(HX - TailX) > 1 || Math.Abs(HY - TailY) > 1)
            {
                TailY = HY - 1;
                TailX = HX;
            }
        }
        if (direction == "U")
        {
            if (Math.Abs(HX - TailX) > 1 || Math.Abs(HY - TailY) > 1)
            {
                TailY = HY + 1;
                TailX = HX;
            }
        }
        return (HX, HY, TailX, TailY);
    }
    public (int, int, int, int) Update2(string direction, int HX, int HY, int TailX, int TailY)
    {
        int dx = HX - TailX;
        int dy = HY - TailY;
        //if (direction == "R")

        if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
        {
            TailX += Math.Min(1, Math.Max(dx, -1));
            TailY += Math.Min(1, Math.Max(dy,-1));
        }


        return (HX, HY, TailX, TailY);
    }

}
