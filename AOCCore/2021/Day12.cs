using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2021;



class Day12 : Day
{
    public Day12()
    {
        GetInput(RootFolder + @"2021_12\");
    }


    public override void Part1(List<string> Lines)
    {
        GetCaves(Lines);
        startIndex = SmallCaves.IndexOf("start");
        endIndex = SmallCaves.IndexOf("end");
        Memory = new Dictionary<long, int>();
        List<List<int>> NumberOfPaths = new();
        for (int i = 0; i < SmallCaves.Count; i++)
        {
            var list = new List<int>();
            for (int j = 0; j < SmallCaves.Count; j++)
            {
                list.Add(CountPaths(SmallCaves[i], SmallCaves[j]));
            }
            NumberOfPaths.Add(list);
        }

      PrintSolution(DFSWithMem(NumberOfPaths, new int[Caves.Count], startIndex, true), "3485", "part 1");
    }
    public override void Part2(List<string> Lines)
    {
        GetCaves(Lines);
        startIndex = SmallCaves.IndexOf("start");
        endIndex = SmallCaves.IndexOf("end");
        Memory = new Dictionary<long, int>();
        List<List<int>> NumberOfPaths = new();
        for (int i = 0; i < SmallCaves.Count; i++)
        {
            var list = new List<int>();
            for (int j = 0; j < SmallCaves.Count; j++)
            {
                list.Add(CountPaths(SmallCaves[i], SmallCaves[j]));
            }
            NumberOfPaths.Add(list);
        }

      PrintSolution(DFSWithMem(NumberOfPaths, new int[Caves.Count], startIndex, false), "85062", "part 2");
    }

    int startIndex;
    int endIndex;
    Dictionary<long, int> Memory = new();
    List<string> Caves = new();
    List<string> SmallCaves = new();
    List<string> BigCaves = new();
    Dictionary<string, List<string>> Edges = new();


    public static long Convert(int[] Visited, int caveFrom, bool twice)
    {
        long acc = caveFrom;
        int i = 0;
        for (; i < Visited.Length; i++)
        {
            acc += Visited[i] << (i + 4);
        }
        acc += (twice ? 1 : 0) << (i + 4);
        return acc;
    }

    private int DFS(List<List<int>> NumberOfPaths, int[] Visited, int caveFrom, bool twice)
    {
        int count = 0;
        for (int caveTo = 0; caveTo < NumberOfPaths.Count; caveTo++)
        {
            if (NumberOfPaths[caveFrom][caveTo] == 0 || caveTo == startIndex) continue;
            if (caveTo == endIndex && NumberOfPaths[caveFrom][caveTo] > 0)
            {
                count += NumberOfPaths[caveFrom][caveTo];
            }
            else if (Visited[caveTo] == 0)
            {

                Visited[caveTo] = 1;
                count += (NumberOfPaths[caveFrom][caveTo]) * DFS(NumberOfPaths, Visited, caveTo, twice);
                Visited[caveTo] = 0;
            }
            else
            if (!twice && (Visited[caveTo] == 1))
            {
                count += (NumberOfPaths[caveFrom][caveTo]) * DFS(NumberOfPaths, Visited, caveTo, true);
            }
        }
        return count;
    }
    private int DFSWithMem(List<List<int>> NumberOfPaths, int[] Visited, int caveFrom, bool twice)
    {

        long key = Convert(Visited, caveFrom, twice);
        if (Memory.ContainsKey(key)) return Memory[key];
        int count = 0;
        for (int caveTo = 0; caveTo < NumberOfPaths.Count; caveTo++)
        {
            if (NumberOfPaths[caveFrom][caveTo] == 0 || caveTo == startIndex) continue;
            if (caveTo == endIndex && NumberOfPaths[caveFrom][caveTo] > 0)
            {
                count += NumberOfPaths[caveFrom][caveTo];
            }
            else if (Visited[caveTo] == 0)
            {
                Visited[caveTo] = 1;
                count += (NumberOfPaths[caveFrom][caveTo]) * DFSWithMem(NumberOfPaths, Visited, caveTo, twice);
                Visited[caveTo] = 0;
            }
            else
            if (!twice && (Visited[caveTo] == 1))
            {
                count += (NumberOfPaths[caveFrom][caveTo]) * DFSWithMem(NumberOfPaths, Visited, caveTo, true);
            }
        }
        Memory[key] = count;
        return count;
    }

    private void GetCaves(List<string> Lines)
    {
        HashSet<string> CavesSet = new();
        HashSet<string> SmallCavesSet = new();
        HashSet<string> BigCavesSet = new();
        Edges = new Dictionary<string, List<string>>();
        foreach (var trans in Lines.Select(x => x.Split('-')))
        {
            void Add(string from, string to)
            {
                if (!Edges.ContainsKey(from)) Edges[from] = new List<string>();
                Edges[from].Add(to);
            }
            CavesSet.Add(trans[0]);
            CavesSet.Add(trans[1]);
            Add(trans[0], trans[1]);
            Add(trans[1], trans[0]);
        }
        foreach (var cave in CavesSet)
        {
            if (char.IsUpper(cave.ToCharArray()[0]))
            {
                BigCavesSet.Add(cave);
            }
            if (char.IsLower(cave.ToCharArray()[0]))
            {
                SmallCavesSet.Add(cave);
            }
        }

        Caves = CavesSet.ToList();
        SmallCaves = SmallCavesSet.ToList();
        BigCaves = BigCavesSet.ToList();
    }

    private int CountPaths(string start, string end)
    {

        Queue<string> q = new();
        HashSet<string> visted = new();
        q.Enqueue(start);
        int count = 0;
        while (q.Count > 0)
        {

            var current = q.Dequeue();
            foreach (var next in Caves.Where(next => Edges.ContainsKey(current) && Edges[current].Contains(next) && !visted.Contains(next)))
            {
                if (next == end)
                {
                    count++;
                }
                else if (BigCaves.Contains(next))
                {
                    visted.Add(next);
                    q.Enqueue(next);
                }
            }
        }
        return count;
    }


}
