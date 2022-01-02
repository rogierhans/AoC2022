using System;
using System.Collections.Generic;
using System.Linq;
using CforFSharp;
namespace AOC2021;


class Day18 : Day
{


    public Day18()
    {

        GetInput(RootFolder + @"2021_18\");
    }
    public const string BLOCK = "\U00002588";

    public override string Part1(List<string> Lines)
    {
        var schoolOfFish = Lines.Select(FishNumberParser.strToFish).ToList();
        var megaFish = schoolOfFish[0];
        for (int i = 1; i < schoolOfFish.Count; i++)
        {
            megaFish = new Fish(CopyFish(megaFish), CopyFish(schoolOfFish[i]));
            PutFishInTheOven(megaFish);

        }
        return PrintSolution(Magnitude(megaFish), "4391", "part 1");
    }
    private Object someListLock = new Object(); // only once

    public override string Part2(List<string> Lines)
    {
        var schoolOfFish = Lines.Select(FishNumberParser.strToFish).ToList();
        long max = 0;
        for (int i = 0; i < schoolOfFish.Count; i++)
        {
            Parallel.ForEach(SL.GetNumbersInt(0, schoolOfFish.Count) ,j => {

                if (i != j)
                {
                    var megaFish = new Fish(CopyFish(schoolOfFish[i]), CopyFish(schoolOfFish[j]));
                    PutFishInTheOven(megaFish);
                    var mag = Magnitude(megaFish);
                    lock (someListLock)
                        max = Math.Max(max, mag);
                }
            });
           
        }
        //Console.ReadLine();
        return PrintSolution(max, "4626", "part 2");
    }
    public void PutFishInTheOven(Fish fish)
    {
        bool p = true;
        SetDepth(fish, 0);
        List<Fish> fishOrder = new List<Fish>();
        GetLiterals(fish, fishOrder);
        while (p)
        {
            p = false;

            bool q = true;

            while (q) {
                q = Explode(fish, fishOrder);
            };

            p |= LeftMostSplit(fish, fishOrder);
        }

    }
    public long Magnitude(Fish fish)
    {
        if (fish.isLeaf) return fish.Number;
        else
        {
            return Magnitude(fish.left) * 3 + Magnitude(fish.right) * 2;
        }
    }
    public bool LeftMostSplit(Fish fish,List<Fish> fishOrder)
    {
        if (fish.isLeaf)
        {
            if (fish.Number > 9)
            {
                double halved = (double)fish.Number / 2.0;
                int leftNumber = (int)Math.Floor(halved);
                int rightNumber = (int)Math.Ceiling(halved);
                fish.isLeaf = false;

    
                fish.left = new Fish(leftNumber);
                fish.left.depth = fish.depth + 1;
                fish.right = new Fish(rightNumber);
                fish.right.depth = fish.depth + 1;
                fishOrder.Insert(fishOrder.IndexOf(fish), fish.left);
                fishOrder.Insert(fishOrder.IndexOf(fish), fish.right);
                fishOrder.Remove(fish);
                return true;
            }
            else return false;
        }
        else
        {
            bool p = LeftMostSplit(fish.left, fishOrder);
            if (p) return p;
            else return LeftMostSplit(fish.right, fishOrder);
        }
    }

    public bool Explode(Fish fish, List<Fish> fishOrder)
    {

        return ExplodeLeftMost(fish, fishOrder);
    }


    public void GetLiterals(Fish fish, List<Fish> fishes)
    {
        if (fish.isLeaf) fishes.Add(fish);
        else
        {
            GetLiterals(fish.left, fishes);
            GetLiterals(fish.right, fishes);
        }
    }
    public bool ExplodeLeftMost(Fish fish, List<Fish> fishOrder)
    {
        if (fish.isLeaf) return false;
        else if (fish.depth == 4)
        {
            int indexLeft = fishOrder.IndexOf(fish.left);
            if (indexLeft != 0)
            {
                fishOrder[indexLeft - 1].Number += fish.left.Number;
            }
            int indexRight = fishOrder.IndexOf(fish.right);
            if (indexRight != fishOrder.Count() - 1)
            {
                fishOrder[indexRight + 1].Number += fish.right.Number;
            }
            fishOrder.Insert(indexLeft, fish);
            fishOrder.Remove(fish.left);
            fishOrder.Remove(fish.right);
            BecomeLeaf(fish);
            return true;
        }
        else
        {
            bool p = ExplodeLeftMost(fish.left, fishOrder);
            if (p) return p;
            else return ExplodeLeftMost(fish.right, fishOrder);
        }
    }

    private void BecomeLeaf(Fish fish)
    {
        fish.left = null; fish.right = null; fish.isLeaf = true; fish.Number = 0;
    }

    public Fish CopyFish(Fish fish)
    {
        if (fish.left is not null && fish.right is not null)
        {
            return new Fish(CopyFish(fish.left), CopyFish(fish.right));
        }
        else return new Fish(fish.Number);

    }

    public void SetDepth(Fish fish, int d)
    {
        fish.depth = d;
        if (fish.left is not null && fish.right is not null)
        {
            SetDepth(fish.left, d + 1);
            SetDepth(fish.right, d + 1);
        }
    }
}
