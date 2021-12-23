using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2
{
    class Day6 : Day
    {
        public Day6()
        {
            string folder = @"C:\Users\Rogier\Desktop\AOC\";
            string name = "input.txt";
            string filename = folder + name;
            string filenameTest = folder + "test.txt";
            var testLines = File.ReadAllLines(filenameTest).ToList();
            var inputLines = File.ReadAllLines(filename).ToList();
            Console.WriteLine("test:");
            IndexForLoop(testLines);
            Console.WriteLine("input:");
            IndexForLoop(inputLines);
        }

        private static void IndexForLoop(List<string> Lines)
        {
            var numbers = Lines.First().Split(',').Select(x => long.Parse(x));
            long[] fishCount = new long[9];
            foreach (var number in numbers)
            {
                fishCount[number]++;
            }
            for (int k = 0; k < 256;)
            {
                fishCount[(7 + k) % 9] += fishCount[k % 9];
                k++;
            }
        }
    }
}
