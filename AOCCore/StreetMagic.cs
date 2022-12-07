using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StreetMagic
{
    public StreetMagic(List<string> lines)
    {
        FindPattern(lines);
    }

    public List<List<(string, string)>> FindPattern(List<string> lines, string after = "")
    {
        List<List<(string, string)>> patterns = new();
        int maxStringSize = lines.Max(x => x.Length);
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            for (int length = line.Length; length >= 2; length--)
            {
                for (int j = 0; j < line.Length - length; j++)
                {

                    var substring = line.Substring(j, length);

                    for (int k = i; k < lines.Count; k++)
                    {
                        if (lines[k].Contains(substring)) {
                            Console.WriteLine("{0}", substring);
                            Console.ReadLine();
                        }
                    }
                }
            }

            //for (int length = 1; length < maxStringSize; length++)
            //{

            //}
        }

        return patterns;
    }
}

