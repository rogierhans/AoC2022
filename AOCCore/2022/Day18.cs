//using Priority_Queue;
using System.Xml;

namespace AOCCore._2022;

class Day18 : Day
{
    public Day18()
    {
        GetInput("2022", "18");

    }


    public override void Part1(List<string> Lines)
    {
        //TryParse(Lines);
        int sum = 0;
        int[,,] cubes = new int[30, 30, 30];
        (int, int)[,,] cubesSides = new (int, int)[30, 30, 30];
        foreach (var line in Lines)
        {
            var input = line.Split(',');
            int x = int.Parse(input[0]);

            int y = int.Parse(input[1]);

            int z = int.Parse(input[2]);
            cubes[x, y, z] = 1;
            // cubesSides[x, y, z] = ();
            sum += 6;
        }
      //  Escape(2, 2, 5, cubes).P();
      //  Console.ReadLine();
        foreach (var line in Lines)
        {
            var input = line.Split(',');
            int x = int.Parse(input[0]);
            int y = int.Parse(input[1]);
            int z = int.Parse(input[2]);
            cubes[x, y, z] = 1;
            foreach (var (dx, dy, dz) in new List<(int, int, int)>() {
            (1,0,0),            (0,1,0),            (0,0,1),
            (-1,0,0),            (0,-1,0),            (0,0,-1),
            })
            {
                if (x + dx >= 0 && y + dy >= 0 && z + dz >= 0)
                {
                    if (cubes[x + dx, y + dy, z + dz] == 1)
                    {
                        sum--;
                    }
                    else if (!Escape(x + dx, y + dy, z + dz, cubes))
                    {
                       // (x + dx, y + dy, z + dz).P();
                        sum--;
                    }
                }
            }
        }
        sum.P();
        Console.ReadLine();
      


    }
    bool Escape(int X, int Y, int Z, int[,,] cubes)
    {
        var visted = new bool[30, 30, 30];
        var Next = new Stack<(int, int, int)>();
        Next.Push((X, Y, Z));

        while (Next.Count() > 0)
        {
            var (x, y, z) = Next.Pop();
           // (x, y, z).P();
            visted[x, y, z] = true;
            foreach (var (dx, dy, dz) in new List<(int, int, int)>() { (1, 0, 0), (0, 1, 0), (0, 0, 1), (-1, 0, 0), (0, -1, 0), (0, 0, -1), })
            {
                if (x + dx >= 0 && y + dy >= 0 && z + dz >= 0 &&
                    x + dx <= 29 && y + dy <= 29 && z + dz <= 29)
                {
                    if (cubes[x + dx, y + dy, z + dz] == 0 && !visted[x + dx, y + dy, z + dz])
                    {
                        visted[x + dx, y + dy, z + dz] = true;
                        Next.Push((x + dx, y + dy, z + dz));
                    }
                }
                else
                {

                    return true;
                }
            }

        }
        return false;
    }
}

