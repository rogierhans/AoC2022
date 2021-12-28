using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Google.OrTools.Sat;
class Program
{
    [STAThreadAttribute]
    static void Main()
    {

        while (true)
        {

          //  new Day15();
            var sw = new Stopwatch();
            sw.Start();
            //var test = new Day15();
            var test  =new Day24Alt();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            Console.ReadLine();
        }

    }
}

