//using Priority_Queue;

using Google.OrTools.Sat;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;
using static Microsoft.FSharp.Core.ByRefKinds;

namespace AOCCore._2022;

class Day23 : Day
{
    public Day23()
    {
        GetInput("2022", "23");

    }

    //If there is no Elf in the N, NE, or NW adjacent positions, the Elf proposes moving north one step.
    //If there is no Elf in the S, SE, or SW adjacent positions, the Elf proposes moving south one step.
    //If there is no Elf in the W, NW, or SW adjacent positions, the Elf proposes moving west one step.
    //If there is no Elf in the E, NE, or SE adjacent positions, the Elf proposes moving east one step.

    class Elf
    {
        public (int, int) Next;
        public bool hasNext = false;
        public List<List<(int, int)>> PList = new List<List<(int, int)>> {
            new List<(int, int)>() { (-1, 0),(-1, 1) , (-1, -1), },
            new List<(int, int)>() { (1, 0) ,(1,1)   , (1,-1) } ,
            new List<(int, int)>() { (0, -1), (1, -1), (-1, -1) },
            new List<(int, int)>() { (0, 1) ,(1, 1)  , (-1, 1) },
        };

    }
    public override void Part1(List<string> Lines)
    {
        var grid = Lines.Select(x => x.List()).ToList();
        Dictionary<(int, int), Elf> elfs = new Dictionary<(int, int), Elf>();

        for (int r = 0; r < grid.Count; r++)
        {
            for (int c = 0; c < grid[0].Count; c++)
            {
                if (grid[r][c] != "#") continue;
                var elf = new Elf();
                elfs[(r, c)] = (elf);
            }
        }

        for (int k = 0; k < 10000; k++)
        {


            var Claims = new Dictionary<(int, int), int>();

            foreach (var ((r, c), elf) in elfs)
            {
                bool Somebody = false;
                foreach (var (nx, ny) in new List<(int, int)> { (1, 1), (-1, 1), (1, -1),
                                        (-1, -1), (1, 0), (-1, 0),
                                        (0, 1), (0, -1) })
                {
                    if (elfs.ContainsKey((nx +r, ny+c)))
                        Somebody = true;
                }
                if (!Somebody) { //"OK".P();
                    continue; }


                for (int d = 0; d < 4; d++)
                {
                    bool free = true;

                    foreach (var (dx, dy) in elf.PList[d])
                    {

                        // Console.WriteLine(offsetI +" "+ offsetJ);
                        int nr = r + dx;
                        int nc = c + dy;

                        if (elfs.ContainsKey((nr, nc)))
                        {
                            free = false;
                        }

                    }
                    if (free)
                    {
                        //d.P();
                        var (dx, dy) = elf.PList[d][0];
                        elf.Next = (dx + r, dy + c);
                        elf.hasNext = true;
                        if (!Claims.ContainsKey((dx + r, dy + c)))
                        {
                            Claims[(dx + r, dy + c)] = 0;
                        }
                        Claims[(dx + r, dy + c)] += 1;
                        break;
                    }
                }



            }
            foreach (var (_, elf) in elfs) {
                var prop = elf.PList.First();
                elf.PList.RemoveAt(0);
                elf.PList.Add(prop);
            }

            var newElfs = new Dictionary<(int, int), Elf>();

            bool moves = false;
            foreach (var ((r, c), elf) in elfs)
            {
                if (elf.hasNext)
                {
                    if (Claims[elf.Next] == 1)
                    {
                        moves= true;    
                        //(r, c, elf.Next).P();
                        newElfs[elf.Next] = elf;
                    }
                    else {
                        newElfs[(r, c)] = elf;
                    }
                }
                else {
                    newElfs[(r, c)] = elf;
                }
            }
            if (!moves) {
                k.P();
                break;
            }

            //Print(elfs);
            elfs.ToList().ForEach(x => x.Value.hasNext = false);
            elfs = newElfs;
           // Print(elfs);
            //Claims.ToList().Print(" "); 
           // Console.ReadLine();
        }
        Print(elfs);
        // print.GridSelect(x => x ? 1 : 0).Print("");
        Console.ReadLine();
    }

    private static void Print(Dictionary<(int, int), Elf> elfs)
    {
        int maxX = elfs.Keys.Max(x => x.Item1);

        int minX = elfs.Keys.Min(x => x.Item1);

        int maxY = elfs.Keys.Max(x => x.Item2);

        int minY = elfs.Keys.Min(x => x.Item2);

        var printGrind = new string[maxX - minX + 1, maxY - minY + 1];
        for (int x = 0; x < printGrind.GetLength(0); x++)
        {
            for (int y = 0; y < printGrind.GetLength(1); y++)
            {
                printGrind[x, y] = ".";
            }
        }
        elfs.Count().P();
        foreach (var ((r, c), elf) in elfs)
        {
            printGrind[r - minX, c - minY] = "#";
        }
        ((maxX - minX + 1) * (maxY - minY + 1) - elfs.Count()).P();
        printGrind.ToLists().Print();
    }


}
