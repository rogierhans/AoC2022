using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2
{

    //class Day4 : Day
    //{
    //    public Day4()
    //    {
    //        GetInput(RootFolder + @"2021_03\");
    //    }


    //    public override string Part1(List<string> lines)
    //    {

    //        return PrintSolution("", "2035764", "part 1");
    //    }
    //    public override string Part2(List<string> Lines)
    //    {

    //        return PrintSolution("", "2817661", "part 2");
    //    }
    //}
    class Day5 : Day
    {


        public Day5()
        {
    

            string folder = @"C:\Users\Rogier\Desktop\AOC\";
            string name = "input.txt";
            string filename = folder + name;
            string filenameTest = folder + "test.txt";
            var testLines = File.ReadAllLines(filenameTest).ToList();
            var inputLines = File.ReadAllLines(filename).ToList();
            Console.WriteLine("test:");
            IndexForLoop(testLines, 10);
            Console.WriteLine("input:");
            IndexForLoop(inputLines, 1000);
        }



        private static void IndexForLoop(List<string> Lines, int gridsize)
        {
            int[,] array = new int[gridsize, gridsize];
            for (int t = 0; t < Lines.Count; t++)
            {

                var line = Lines[t];
                var input = line.Replace(" -> ", ",").Split(',');
                int x1 = int.Parse(input[0]);
                int y1 = int.Parse(input[1]);
                int x2 = int.Parse(input[2]);
                int y2 = int.Parse(input[3]);


                int minx = Math.Min(x1, x2);
                int miny = Math.Min(y1, y2);

                int maxX = Math.Max(x1, x2);
                int maxY = Math.Max(y1, y2);
                if (x1 == x2 || y1 == y2)
                {
                    for (int x = minx; x <= maxX; x++)
                    {
                        for (int y = miny; y <= maxY; y++)
                        {
                            array[x, y]++;
                        }
                    }
                }
                else
                {
                    if (x1 < x2 && y1 < y2)
                    {
                        for (int i = 0; i <= x2 - x1; i++)
                        {
                            array[x1 + i, y1 + i]++;
                        }
                    }
                    else if (x1 < x2 && y1 > y2)
                    {
                        for (int i = 0; i <= x2 - x1; i++)
                        {
                            array[x1 + i, y1 - i]++;
                        }
                    }
                    else if (x1 > x2 && y1 < y2)
                    {
                        for (int i = 0; i <= x1 - x2; i++)
                        {
                            array[x1 - i, y1 + i]++;
                        }
                    }
                    else if (x1 > x2 && y1 > y2)
                    {
                        for (int i = 0; i <= x1 - x2; i++)
                        {
                            array[x1 - i, y1 - i]++;
                        }
                    }
                    else {
                        throw new Exception();
                    }

                }

                for (int x = 0; x < 10; x++)
                {
                    string strLine = "";
                    for (int y = 0; y < 10; y++)
                    {
                        strLine += array[x, y];
                    }
                    //Console.WriteLine(strLine);
                }
              //  Console.WriteLine("{0} {1} {2} {3}", x1, x2, y1, y2);
                //Console.ReadLine();
                //  
                //var key = input[0];
                //var value = input[1];
                // 

            }

            for (int x = 0; x < 10; x++)
            {
                string strLine = "";
                for (int y = 0; y < 10; y++)
                {
                    strLine += array[x, y];
                }
                Console.WriteLine(strLine);
            }
            int count = 0;
            for (int x = 0; x < gridsize; x++)
            {
                for (int y = 0; y < gridsize; y++)
                {
                    if (array[x, y] >= 2) count++;
                }

            }
        }
    }
}
