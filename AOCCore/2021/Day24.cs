using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Priority_Queue;
class Day24 : Day
{
    public Day24()
    {
        GetInput(RootFolder + @"2021_24\");
    }

    public override void Main(List<string> Lines)
    {


        var setOfLines = new List<List<string>>();
        for (int i = 0; i < 14; i++)
        {
            setOfLines.Add(Lines.Skip(i * 18).Take(18).ToList());
        }

        var tuples = setOfLines.Select(x => (x[4].Pattern("div z {0}", int.Parse), x[5].Pattern("add x {0}", int.Parse), x[15].Pattern("add y {0}", int.Parse)));
        tuples.ToList().Print("\n");

        List<Func<int, int, int>> Functions = new List<Func<int, int, int>>();
        foreach (var (cz, c1, c2) in tuples)
        {

            Functions.Add((z, w) => DummyFunction(z, cz, c1, c2, w));
        }
        var current =new Dictionary<int, long>();
        current[0] = 0;
        for (int m = 13; m >= 0; m--)
        {
            current = GetMapping(Functions[m], current, 26 * 26 * 26 * 26 * 26);
            Console.WriteLine("Block "+m +" possible z values "+current.Count);
         //   current.ToList().Print("\n");
        }
        Console.WriteLine(current[0] / 10);
        Console.ReadLine();
    }


    public Dictionary<int, long> GetMapping(Func<int, int, int> f, Dictionary<int, long> fromLast, int maxZinput)
    {
        Dictionary<int, long> list = new();

        List<(int, int)> ZinputToDigit = new List<(int, int)>();
        for (int zInput = 0; zInput <= maxZinput; zInput++)
        {

            for (int digit = 9; digit > 0; digit--)
            {
                var value = f(zInput, digit);
                if (fromLast.ContainsKey(value))
                {
                    list[zInput] = long.Parse(digit.ToString() + fromLast[value]);
                    break;
                }
            }

        }
        return list;
    }

    public Dictionary<int, long> LastMapping(Func<int, int, int> f, int maxZinput)
    {
        Dictionary<int, long> dict = new();
        for (int zInput = 0; zInput <= maxZinput; zInput++)
        {
            for (int digit = 1; digit <= 9; digit++)
            {
                var value = f(zInput, digit);
                if (value == 0)
                {
                    dict[zInput] = digit;
                }
            }
        }
        return dict;
    }
    public int DummyFunction(int previousZ, int constZ, int const1, int const2, int w)
    {

        int x = 0;                  //mul x   0
        x = previousZ;              //add x   z
        x = x % 26;                 //mod x   26
        int z = previousZ / constZ; //div z   constZ
        x = x + const1;             //add x   const1
                                    //inp w
        x = x == w ? 1 : 0;         //eql x   w
        x = x == 0 ? 1 : 0;         //eql x   0
        int y = 0;                  //mul y   0
        y = y + 25;                 //add y   25
        y = y * x;                  //mul y   x
        y = y + 1;                  //add y   1
        z = z * y;                  //mul z   y
        y = 0;                      //mul y   0
        y = w + y;                  //add y   w
        y = y + const2;             //add y   const2
        y = y * x;                  //mul y   x
        return z + y;               //add z   y
    }

}


