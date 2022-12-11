using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021;

class Day17 : Day
{


    public Day17()
    {
        GetInput(RootFolder + @"2021_17\");
    }
    public const string BLOCK = "\U00002588";
    public override void Part1(List<string> Lines)
    {
        var numbers = Lines.First().Pattern("target area: x={0}..{1}, y={2}..{3}", int.Parse, int.Parse, int.Parse, int.Parse);
        List<int> heights = new List<int>();
        for (int veloX = 0; veloX < 100; veloX++)
        {
            for (int veloY = 0; veloY < 1000; veloY++)
            {
                heights.Add(Simulate(veloX, veloY, numbers));
            }
        }
      PrintSolution(heights.Where(x => x != int.MinValue).Max(), "17766", "part 1"); 
    }
    public override void Part2(List<string> Lines)
    {
        var numbers = Lines.First().Pattern("target area: x={0}..{1}, y={2}..{3}", int.Parse, int.Parse, int.Parse, int.Parse);
        List<int> heights = new List<int>();
        for (int veloX = 0; veloX < 150; veloX++)
        {
            for (int veloY = -200; veloY < 1000; veloY++)
            {
                heights.Add(Simulate(veloX, veloY, numbers));
            }
        }
      PrintSolution(heights.Where(x => x != int.MinValue).Count(), "1733", "part 2");
    }
    //int best = 0;
    private int Simulate(int veloX, int veloY, (int, int, int, int) numbers)
    {
        int posX = 0;
        int posY = 0;
        bool target = false;
        int maxYPos = 0;
        while (posX <= numbers.Item2 && posY >= numbers.Item3)
        {
            posX += veloX;
            posY += veloY;
            maxYPos = Math.Max(maxYPos, posY);
            veloX = Math.Max(0, veloX - 1);
            veloY--;
            if (numbers.Item1 <= posX && posX <= numbers.Item2 && numbers.Item3 <= posY && posY <= numbers.Item4)
                target = true;
        }
        return target ? maxYPos : int.MinValue;
    }
}
