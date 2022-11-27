using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Google.OrTools.Sat;
using AOCCore;
using AOCCore._2017;

class Program
{
    [STAThreadAttribute]
    static  void Main()
    {
        // Year2021();

        //  InputFetcher.GetFile("2017", "01");

        var day = new Day01();
        day.Part1(true);
    }
}

