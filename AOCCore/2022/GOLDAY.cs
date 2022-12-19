
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore._2022;


class GOLDAY : Day
{
    public GOLDAY()
    {
        GetInput("2022", "03");

    }


    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        var grid = Lines.Select(x => x.List()).ToList();   



    }
    public List<List<T>> GOL<T>(List<List<T>> list, List<(int, int)> neighbors, Func<List<T>, T> f, int iterations, bool loop = false)
    {
        var newList = list.DeepCopy();
        for (int i = 0; i < iterations; i++)
        {
            newList = GOLIteration(list, neighbors, f, loop);
        }
        return newList;
    }

    public List<List<T>> GOLIteration<T>(List<List<T>> list, List<(int, int)> neighbors, Func<List<T>, T> f, bool loop = false)
    {
        var newGrid = list.DeepCopy();
        for (int r = 0; r < list.Count; r++)
        {
            for (int c = 0; c < list[0].Count; c++)
            {
                newGrid[r][c] = f(GetNeighbors(list, r, c, neighbors, loop));
            }
        }
        return newGrid;
    }

    public List<T> GetNeighbors<T>(List<List<T>> list, int oldX, int oldY, List<(int, int)> neighbors, bool loop = false)
    {
        List<T> neighborList = new List<T>();
        int maxHeight = list.Count;
        int Width = list[0].Count;
        foreach (var (dx, dy) in neighbors)
        {

            // Console.WriteLine(offsetI +" "+ offsetJ);
            int r = oldX + dx;
            int c = oldY + dy;

            bool inRow = 0 <= r && r <= maxHeight - 1;
            bool inColouwm = 0 <= c && c <= Width - 1;


            if (loop)
            {
                neighborList.Add(list[(r + maxHeight) % maxHeight][(c + Width) % Width]);
            }
            else
            {
                if (inRow && inColouwm)
                {
                    neighborList.Add(list[r][c]);
                }
            }
        }



        return neighborList;
    }
}

