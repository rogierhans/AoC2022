using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StreetMagic
{
    public static List<string> FindPattern(List<string> linesInput, params string[] replaces)

    {

        List<string> lines = linesInput.ToList();
        if (lines.Count == 0) return lines;

        bool p = true;
        while (p)
        {
            var line = lines.First();
            p = false;
            bool breaker = false;
            for (int startIndex = 0; startIndex < line.Length; startIndex++)
            {
                if (breaker) break;
                for (int endIndex = line.Length - 1; endIndex >= startIndex + 2; endIndex--)
                {
                    string substring = line.Substring(startIndex, endIndex - startIndex);
                    if (lines.All(x => x.Contains(substring)))
                    {
                        p = true;
                        breaker = true;
                        lines = lines.Select(x => x.Replace(substring, ";")).ToList();
                        break;
                    }


                }
            }
        }
        foreach (var replace in replaces)
        {
            lines = lines.Select(x => x.Replace(replace, "")).ToList();
        }
        for (int i = 0; i < lines.Count; i++)
        {
            var input = lines[i].Split(";");

            lines[i] = string.Join(';', input.Where(x => x.Replace(" ", "") != "").ToList());
        }

        lines.Print("\n");
        return lines;
    }
}

