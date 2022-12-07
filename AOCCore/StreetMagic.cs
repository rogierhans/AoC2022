using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOCCore
{
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
                for (int length = line.Length; length >= 0; length--)
                {

                }
                for (int j = 0; j < lines.Count; j++)
                {
                    if (i == j) continue; 
                }
                //for (int length = 1; length < maxStringSize; length++)
                //{

                //}
            }

            return patterns;
        }
    }
}
