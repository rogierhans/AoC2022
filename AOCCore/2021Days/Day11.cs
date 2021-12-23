using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2
{
    class Day11 : Day
    {


        public Day11()
        {


            string folder = @"C:\Users\Rogier\Desktop\AOC\";
            string name = "input.txt";
            string filename = folder + name;
            string filenameTest = folder + "test.txt";
            var testLines = File.ReadAllLines(filenameTest).ToList();
            var inputLines = File.ReadAllLines(filename).ToList();
            Console.WriteLine("test:");
            ModeSelector(testLines);
            Console.WriteLine("input:");
            ModeSelector(inputLines);
        }


        private static void ModeSelector(List<string> Lines)
        {
            var grid = Lines.Parse2D(x => int.Parse(x));
            long count = 0;
            int k = 0;
            while (true)
            {
                var flashed = grid.GridSelect(x => false);
                grid = grid.GridSelect(x => x + 1);
                bool newFlash = true;
                while (newFlash)
                {

                    newFlash = false;
                    for (int i = 0; i < grid.Count; i++)
                    {
                        for (int j = 0; j < grid[0].Count; j++)
                        {
                            if (!flashed[i][j] && grid[i][j] > 9)
                            {
                                flashed[i][j] = true;
                                newFlash = true;
                                count++;
                                foreach (var offset in new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1), (-1, 1), (-1, -1), (1, 1), (1, -1) })
                                {
                                    int x = i + offset.Item1;
                                    int y = j + offset.Item2;
                                    bool outOfRow = x < 0 || x >= grid.Count;
                                    bool outOfColumn = y < 0 || y >= grid[0].Count;
                                    if (!outOfColumn && !outOfRow)
                                        grid[x][y]++;
                                }
                            }
                        }
                    }
                }

                grid = grid.GridSelect(x => x > 9 ? 0 : x);
                k++;
                if (grid.GridSum(x => x) == 0)
                    break;

            }
            Console.WriteLine(k);
            Console.ReadLine();
        }




        class Element
        {

            public Element(List<string> lines)
            {
                ///ParseSingle(lines.First());
                ParseMulti(lines);
            }
 
            private static void ParseMulti(List<string> lines)
            {
                SL.Line();
                for (int i = 0; i < lines.Count; i++)
                {
                    var line = lines[i];
                    SL.Log(line);

                    var sperator = ' ';

                    var input = line.Split(sperator).Select(x => long.Parse(x)).ToList();

                }
            }


        }

    }
}
