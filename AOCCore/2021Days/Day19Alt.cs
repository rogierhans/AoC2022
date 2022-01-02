using System;
using System.Collections.Generic;
using System.Linq;
//using ParsecSharp;
//using FishLibrary;
namespace AOC2021;
class Day19Alt : Day
{


    public Day19Alt()
    {
        GetInput(RootFolder + @"2021_19\");
    }
    const string BLOCK = "\U00002588";


    public static HashSet<int> counts = new HashSet<int>();
    public override string Part2(List<string> Lines)
    {
        var scaners = Lines.ClusterLines().Select(clines => new Scanner(clines)).ToList();
        List<(long, long, long, Position, Position)> AllDistances = new();
        //scaners[0].Machtes(scaners[1]);
        scaners[0].HasPosition = true;
        scaners[0].PositionScanner = new Position(0, 0, 0);
        Queue<Scanner> Q = new Queue<Scanner>();
        Q.Enqueue(scaners[0]);

        while (Q.Any())
        {
            var scanner1 = Q.Dequeue();
            for (int i = 0; i < scaners.Count; i++)
            {
                if (!scaners[i].HasPosition)
                    scanner1.Machtes(scaners[i], Q);
            }
        }

        long max = 0;
        for (int i = 0; i < scaners.Count; i++)
        {
            for (int j = 0; j < scaners.Count; j++)
            {
                if (i != j)
                {
                    var distance = (Math.Abs(scaners[i].PositionScanner.X - scaners[j].PositionScanner.X)) +
                       Math.Abs((scaners[i].PositionScanner.Y - scaners[j].PositionScanner.Y)) +
                        Math.Abs((scaners[i].PositionScanner.Z - scaners[j].PositionScanner.Z));
                    max = Math.Max(max, distance);
                }

            }
        }
        return PrintSolution(max, "13382", "part 2");
    }
    public class Position
    {
        public int X, Y, Z;

        public List<(long, Position)> DistanceList = new List<(long, Position)>();

        public Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public override string ToString()
        {
            return "(" + X + "," + Y + "," + Z + ")";
        }
        public (long, long, long, Position) Distance(Position other)
        {
            return (Squired(X, other.X), Squired(Y, other.Y), Squired(Z, other.Z), other);
        }

        public long DistanceNumber(Position other)
        {
            return Squired(X, other.X) + Squired(Y, other.Y) + Squired(Z, other.Z);
        }
        public static long Squired(int one, int two)
        {

            return (one - two) * (one - two);
        }
        public (bool, Position, Position) Match(Position otherPos)
        {
            int count = 0;
            int i = 0;
            int j = 0;
            while (i < DistanceList.Count && j < otherPos.DistanceList.Count)
            {
                var (distance1, posMate1) = DistanceList[i];
                var (distance2, posMate2) = otherPos.DistanceList[j];

                if (distance1 == distance2)
                {
                    count++;
                    if (count > 5)
                    {
                        // Console.WriteLine(count);
                        return (true, posMate1, posMate2);
                    }
                }

                if (distance1 < distance2) { i++; }
                else { j++; }

            }

            // Console.WriteLine(count);
            counts.Add(count);
            //Console.Write(count + " ");
            return (false, null, null);

        }
        public void ApplyGivePermutations(int p)
        {
            List<(int, int, int)> newlist = new List<(int, int, int)>()
            {
                   (X, Y, Z),
                   (Z, Y, -X),
                   (-X, Y, -Z),
                   (-Z, Y, X),
                   (-X, -Y, Z),
                   (-Z, -Y, -X),
                   (X, -Y, -Z),
                   (Z, -Y, X),
                   (X, -Z, Y),
                   (Y, -Z, -X),
                   (-X, -Z, -Y),
                   (-Y, -Z, X),
                   (X, Z, -Y),
                   (-Y, Z, -X),
                   (-X, Z, Y),
                   (Y, Z, X),
                   (Z, X, Y),
                   (Y, X, -Z),
                   (-Z, X, -Y),
                   (-Y, X, Z),
                   (-Z, -X, Y),
                   (Y, -X, Z),
                   (Z, -X, -Y),
                   (-Y, -X, -Z)};
            X = newlist[p].Item1;
            Y = newlist[p].Item2;
            Z = newlist[p].Item3;
        }

        public bool Equal(Position other)
        {
            return (X == other.X) && (Y == other.Y) && (Z == other.Z);
        }

    }

    public class Scanner
    {
        public int id = 0;
        public string Name = "";
        public string Coordssystem;
        public Position PositionScanner = new(int.MinValue, int.MinValue, int.MinValue);
        public bool HasPosition = false;
        List<Position> Positions = new();

        public Scanner(List<string> inputLines)
        {

            Name = inputLines[0];
            id = Name.Pattern("--- scanner {0} ---", int.Parse);
            Coordssystem = Name;
            Positions = inputLines.FindPatterns("{0},{1},{2}", int.Parse, int.Parse, int.Parse).Select(x => new Position(x.Item1, x.Item2, x.Item3)).ToList();
            SetDistance();
        }


        public void SetPosition(int perm, Position newPos)
        {
            PositionScanner = newPos;
            Positions.ForEach(x => x.ApplyGivePermutations(perm));
            HasPosition = true;
            // Console.WriteLine(this + " " + newPos);
        }

        public void Machtes(Scanner otherScanner, Queue<Scanner> Q)
        {
            //{
            //    var pos2 = otherScanner.Positions[j];
            //    var (suc, mate1, mate2) = Thing(otherScanner);
            //    if (suc)
            //    {
            //        // Console.WriteLine(this + " " + otherScanner);
            //        var (perm1, newPos1) = CheckLel(pos1, mate1, pos2, mate2);
            //        //var (perm2, newPos2) = CheckLel(pos1, mate1, mate2, pos2);
            //        if (newPos1 is not null)
            //        {
            //            otherScanner.SetPosition(perm1, newPos1);
            //            Q.Enqueue(otherScanner);
            //        }
            //        //else if (newPos2 is not null)
            //        //{
            //        //    otherScanner.SetPosition(perm2, newPos2);
            //        //    Q.Enqueue(otherScanner);
            //        //}
            //        else { throw new Exception(); }
            //        return;
            //    }
            //}

            for (int i = 0; i < Positions.Count; i++)
            {
                var pos1 = Positions[i];
                for (int j = 0; j < otherScanner.Positions.Count; j++)
                {

                    var pos2 = otherScanner.Positions[j];
                    var (suc, mate1, mate2) = pos1.Match(pos2);
                    if (suc)
                    {
                        // Console.WriteLine(this + " " + otherScanner);
                        var (perm1, newPos1) = CheckLel(pos1, mate1, pos2, mate2);
                        //var (perm2, newPos2) = CheckLel(pos1, mate1, mate2, pos2);
                        if (newPos1 is not null)
                        {
                            otherScanner.SetPosition(perm1, newPos1);
                            Q.Enqueue(otherScanner);
                        }
                        //else if (newPos2 is not null)
                        //{
                        //    otherScanner.SetPosition(perm2, newPos2);
                        //    Q.Enqueue(otherScanner);
                        //}
                        else { throw new Exception(); }
                        return;
                    }
                }
            }
        }



        private (bool, Position, Position) Thing(Scanner otherScanner)
        {
            int count = 0;
            int i = 0;
            int j = 0;
            while (i < DistanceList.Count && j < otherScanner.DistanceList.Count)
            {
                var (distance1, posMate1) = DistanceList[i];
                var (distance2, posMate2) = otherScanner.DistanceList[j];

                if (distance1 == distance2)
                {
                    count++;
                    if (count > 100)
                    {

                        return (true, posMate1, posMate2);
                    }
                }

                if (distance1 < distance2) { i++; }
                else { j++; }

            }
            // Console.WriteLine(count);
            //Console.Write(count + " ");
            return (false, null, null);
        }

        private (int, Position) CheckLel(Position pos1, Position pos1Mate, Position pos2, Position pos2Mate)
        {
            int xDelta = pos1.X - pos1Mate.X;
            int yDelta = pos1.Y - pos1Mate.Y;
            int zDelta = pos1.Z - pos1Mate.Z;



            var perms1 = GivePermutations(pos2.X, pos2.Y, pos2.Z);
            var perms2 = GivePermutations(pos2Mate.X, pos2Mate.Y, pos2Mate.Z);
            // Console.WriteLine("{0} {1} {2}", xDelta, yDelta, zDelta);
            for (int p = 0; p < perms1.Count; p++)
            {
                int x = perms1[p].Item1 - perms2[p].Item1;
                int y = perms1[p].Item2 - perms2[p].Item2;
                int z = perms1[p].Item3 - perms2[p].Item3;
                //Console.WriteLine("other:{0} {1} {2}", x, y, z);
                if (x == xDelta && y == yDelta && z == zDelta)
                {
                    var newPosition = new Position(PositionScanner.X + (pos1.X - perms1[p].Item1), PositionScanner.Y + (pos1.Y - perms1[p].Item2), PositionScanner.Z + (pos1.Z - perms1[p].Item3));
                    return (p, newPosition);
                }
            }

            return (-1, null);
        }

        private List<(int, int, int)> GivePermutations(int x, int y, int z)
        {
            List<(int, int, int)> newlist = new List<(int, int, int)>()
            {
                   (x, y, z),
                   (z, y, -x),
                   (-x, y, -z),
                   (-z, y, x),
                   (-x, -y, z),
                   (-z, -y, -x),
                   (x, -y, -z),
                   (z, -y, x),
                   (x, -z, y),
                   (y, -z, -x),
                   (-x, -z, -y),
                   (-y, -z, x),
                   (x, z, -y),
                   (-y, z, -x),
                   (-x, z, y),
                   (y, z, x),
                   (z, x, y),
                   (y, x, -z),
                   (-z, x, -y),
                   (-y, x, z),
                   (-z, -x, y),
                   (y, -x, z),
                   (z, -x, -y),
                   (-y, -x, -z)};
            return newlist;
        }


        public List<(long, Position)> DistanceList = new List<(long, Position)>();
        private void SetDistance()
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                var pos1 = Positions[i];
                for (int j = 0; j < Positions.Count; j++)
                {
                    if (i != j)
                    {
                        var pos2 = Positions[j];
                        pos1.DistanceList.Add((pos1.DistanceNumber(pos2), pos2));
                    }
                }
                pos1.DistanceList = pos1.DistanceList.OrderBy(x => x).ToList();
                DistanceList.AddRange(pos1.DistanceList);
            }
            DistanceList = DistanceList.OrderBy(x => x.Item1).ToList();
        }

        public override string ToString()
        {
            return "Scanner:" + Name + "_" + PositionScanner;
        }
    }

}
