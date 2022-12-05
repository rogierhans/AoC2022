
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore._2022;


class GOLDAY : Day
{
    public GOLDAY()
    {
        GetInput("2022", "03");

    }


    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        var grid = Lines.Select(x => x.List()).ToList();   



    }
}

