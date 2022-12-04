//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;

//namespace AOCCore._2022;

//class Day01 : Day
//{
//    public Day01()
//    {
//        GetInput("2022", "01");

//    }

//    public override void Part1(List<string> Lines)
//    {
//        TryParse(Lines);
//        // first solution had a loop and this is far more [REDACTED].
//        NumberedBlocks.Select(x => x.GridSum(y => y)).Max().P();
//        NumberedBlocks.Select(x => x.GridSum(y => y)).OrderByDescending(x => x).Take(3).Sum().P();
//    }
//}
