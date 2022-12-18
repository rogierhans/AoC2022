//using Priority_Queue;
namespace AOCCore._2022;

class Day17 : Day
{
    public Day17()
    {
        GetInput("2022", "17");

    }

    public override void Part1(List<string> Lines)
    {

        string Rocksstrring = "####\r\n\r\n.#.\r\n###\r\n.#.\r\n\r\n..#\r\n..#\r\n###\r\n\r\n#\r\n#\r\n#\r\n#\r\n\r\n##\r\n##";
        var rocks = Rocksstrring.Split("\r\n").ToList();
        rocks.Print(" ");
        var CoolSet = new HashSet<(int, int)>();
        for (int j = 0; j < 7; j++)
        {
            CoolSet.Add((j, 0));
        }
        int jetIndex = 0;
        int shapeIndex = 0;
        List<List<(int, int)>> shapes = new List<List<(int, int)>>() {

            new List<(int, int)>() { (0,0), (1, 0), (2, 0), (3, 0) } ,
        new List<(int, int)>() { (1,0),(0, 1), (1, 1), (2, 1),(1,2)},
        new List<(int, int)>() { (2,0),(2, 1), (0, 2), (1, 2),(2,2) },
        new List<(int, int)>() { (0,0), (0, 1), (0, 2), (0, 3) },
        new List<(int, int)>() { (0,0), (1, 1), (0, 1), (1, 0) }};
        foreach (var shapeR in shapes)
        {
            int[,] test = new int[4, 4];
            foreach (var (x, y) in shapeR)
            {
                test[x, y] = 1;
            }
            Console.WriteLine();
            test.ToLists().GridSelect(x => x == 0 ? "." : "#").Print(" ");
        }
        // PrintSet(CoolSet);
        int RockNumber = 0;
        int Height = 0;
        List<int> HeightAtIteration = new List<int>();

        Dictionary<(int, int), int> cycle = new Dictionary<(int, int), int>();
        int cycleSize = 1000000;

        while (true)
        {

            HeightAtIteration.Add(Height);
            cycleSize = 345;
            if (RockNumber > cycleSize)
            {

                long iterations2Go = (1000000000000 - RockNumber);
                if (iterations2Go % cycleSize == 0)
                {
                    var rate = (double)(HeightAtIteration[RockNumber] - HeightAtIteration[RockNumber - cycleSize]) / (double)cycleSize;
                    Console.WriteLine((HeightAtIteration[RockNumber] + rate * iterations2Go)- 1566376811584);
                }
            }
            RockNumber++;
            var key = (shapeIndex, jetIndex);
            if (cycle.ContainsKey(key))
            {
                //Console.WriteLine(cycleSize);
               // Console.ReadLine();
                cycleSize = RockNumber - cycle[key];
                cycle[key] = RockNumber;
            }
            cycle[key] = RockNumber;



            var CurrentY = Height + 4 + shapes[shapeIndex].Max(x => x.Item2);
            var CurrentX = 2;
            //  CurrentY.P();
            PrintSet(CoolSet, shapes[shapeIndex], CurrentX, CurrentY, true);
            // Console.ReadLine();
            var shapeMaxX = shapes[shapeIndex].Max(x => x.Item1);
            bool CheckOverLap(HashSet<(int, int)> set, List<(int, int)> point, int nextX, int nextY)
            {

                foreach (var (x, y) in point)
                {
                    var pX = x + nextX;
                    if (pX < 0 || pX > 6) return true;
                    if (set.Contains((x + nextX, nextY - y))) return true;
                }
                return false;
            }
            while (true)
            {

                if (CheckOverLap(CoolSet, shapes[shapeIndex], CurrentX + (Lines[0][jetIndex] == '>' ? 1 : -1), CurrentY))
                {
                    // "N".P();
                }
                else
                {

                    // "Y".P();
                    CurrentX = CurrentX + (Lines[0][jetIndex] == '>' ? 1 : -1);
                }
                // "Yet".P(); Lines[0][i].P();
                // (CurrentX, CurrentY).P();
                PrintSet(CoolSet, shapes[shapeIndex], CurrentX, CurrentY, true);


                if (CheckOverLap(CoolSet, shapes[shapeIndex], CurrentX, CurrentY - 1))
                {

                    foreach (var (x, y) in shapes[shapeIndex])
                    {
                        CoolSet.Add((x + CurrentX, CurrentY - y));
                        Height = Math.Max(Height, CurrentY - y);
                    }
                    // "FallFailed".P(); Lines[0][i].P();
                    //(CurrentX, CurrentY).P();
                    PrintSet(CoolSet, shapes[shapeIndex], CurrentX, CurrentY, true);
                    jetIndex++;
                    break;
                }
                else
                {
                    CurrentY--;
                    PrintSet(CoolSet, shapes[shapeIndex], CurrentX, CurrentY, true);

                }

                jetIndex++;
                jetIndex = jetIndex % Lines[0].Length;
            }

            //{
            //    var Ys = CoolSet.Select(x => x.Item2).OrderByDescending(x => x).ToArray();
            //    Ys.ToList().Print(" ");
            //    int ycounter = 1;
            //    int currentY = Ys[0];
            //    for (int index = 1; index < Ys.Length; index++)
            //    {
            //        if (Ys[index] == currentY)
            //        {
            //            ycounter++;
            //            if (ycounter == 7)
            //            {
            //               // currentY.P();
            //                yBottom = currentY;
            //                break;
            //            }
            //        }
            //        else
            //        {
            //            //ycounter.P();
            //            ycounter = 1;
            //            currentY = Ys[index];
            //        }
            //    }
            //    yBottom.P();
            //    CoolSet = CoolSet.Where(x => x.Item2 >= yBottom).ToHashSet();

            //}
            //Console.ReadLine();
            shapeIndex = (shapeIndex + 1) % 5;

        }
        void PrintSet(HashSet<(int, int)> set, List<(int, int)> shape, int CurrentX, int CurrentY, bool include)
        {
            return;
            //Console.WriteLine();

            //int[,] test = new int[4, 4];
            //foreach (var (x, y) in shape)
            //{
            //    test[x, y] = 1;
            //}
            //Console.WriteLine();
            //test.ToLists().GridSelect(x => x == 0 ? "." : "#").Print(" ");
            //int maxYPrint = 20;
            //var array = new string[maxYPrint, 7].ToLists();
            //for (int x = 0; x < array.Count; x++)
            //{
            //    for (int y = 0; y < array[0].Count; y++)
            //    {
            //        array[x][y] = ".";
            //    }
            //}
            //foreach (var (x, y) in set)
            //{
            //    //   (x, y).P();
            //    if (y < maxYPrint && x >= 0 && x < 7)
            //        array[y][x] = "#";
            //}
            //if (include)
            //    foreach (var (x, y) in shape)
            //    {
            //        //(x, y).P();
            //        //(CurrentX + x, CurrentY - y).P();
            //        // "".P();
            //        if (CurrentY - y < maxYPrint)
            //            array[CurrentY - y][CurrentX + x] = "x";
            //        //array.Print("");
            //    }
            //array.Print("");
            //Console.ReadLine();
        }

    }

}

