using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Google.OrTools.Sat;
using AOCCore;
using AOCCore._2022;

class Program
{
    [STAThreadAttribute]
    static void Main()
    {


        var day = new Day15();
        day.Part2(false);

        //if (false) {
        //    await InputFetcher.GetFile("2022", "02");
        //}
    }

}

