using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AOC2021;
class Day3 : Day
{
    public Day3()
    {
        GetInput(RootFolder + @"2021_03\");
    }


    public override void Part1(List<string> lines)
    {
        var array = lines.Parse2D(x => int.Parse(x));
        List<int> gammaRate = new List<int>();
        List<int> eplislonRate = new List<int>();
        for (int i = 0; i < array.First().Count; i++)
        {
            int commonBit = array.Transpose().Select(x => Round(x.Average())).ToList()[i];
            int leascommonBits = array.Transpose().Select(x =>1- Round(x.Average())).ToList()[i];
            gammaRate.Add(commonBit);
            eplislonRate.Add(leascommonBits);
        }
      PrintSolution(Bits2Number(gammaRate) * Bits2Number(eplislonRate), "2035764", "part 1");
    }
    public override void Part2(List<string> Lines)
    {
        var oxygenBits = ReduceToSingle(Lines, x => x);
        var CO2Bits = ReduceToSingle(Lines, x => 1 - x);
        var oxygen = Bits2Number(oxygenBits);
        var CO2 = Bits2Number(CO2Bits);
      PrintSolution((oxygen * CO2).ToString(), "2817661", "part 2");
    }

    private static List<int> ReduceToSingle(List<string> lines, Func<int, int> funcOnCommonBit)
    {
        var array = lines.Parse2D(x => int.Parse(x));
        for (int i = 0; i < array.First().Count; i++)
        {
            int commonBit = array.Transpose().Select(x => Round(x.Average())).ToList()[i];
            array = array.Where(bit => bit[i] == funcOnCommonBit(commonBit)).ToList();
            if (array.Count == 1) return array.First();
        }

        throw new Exception("");
    }
    static int Round(double number)
    {
        return (number == 0.5) ? 1 : (int)Math.Round(number);
    }
    private static long Bits2Number(List<int> list)
    {
        long number = 0;
        for (int i = 0; i < list.Count; i++)
        {
            number += list[list.Count - i - 1] << i;
        }
        return number;
    }
}
