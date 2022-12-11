using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AOCCore._2022;

class Day07 : Day
{
    public Day07()
    {
        GetInput("2022", "07");

    }



    public override void Part1(List<string> Lines)
    {
        TryParse(Lines); ;
        var currentDirectory = new Dir();
        var root = currentDirectory;
        HashSet<Dir> dirs = new HashSet<Dir>();
        dirs.Add(root);
        foreach (var line in Lines.Skip(1))
        {
            if (line.Substring(0, 3) == "dir")
            {

            }
            else if (line.Split('.').Count() == 2)
            {
                currentDirectory.Files.Add(long.Parse(line.Split(' ')[0]));
            }
            else if (line.Split(' ').Count() == 2)
            {
                currentDirectory.Files.Add(long.Parse(line.Split(' ')[0]));
            }
            else if (line.Substring(0, 4) == "$ cd")
            {
                string folderName = line.Split(" ")[2];
                if (folderName == "..")
                {
                    currentDirectory = currentDirectory.Parent;
                }
                else
                {
                    var newDir = new Dir();
                    newDir.name = folderName;
                    newDir.Parent = currentDirectory;
                    currentDirectory = newDir;
                    newDir.Parent.Dirs.Add(newDir);
                    dirs.Add(newDir);
                    // Console.WriteLine("{0} {1}", currentDirectory.name, currentDirectory.Parent.name);
                    // Console.ReadLine();
                }
            }
        }
        // root.Print();
        var totalSize = root.Size();
        //  Console.WriteLine();
        Console.WriteLine(dirs.Select(x => x.Size()).Where(x => x <= 100000).Sum());

        foreach (var size in dirs.Select(x => x.Size()).OrderBy(x => x))
        {
            //Console.WriteLine("try {0} {1}",size, 70000000 - size);
            if ((70000000 - totalSize) + size >= 30000000)
            {
                Console.WriteLine(size);
                break;
            }
        }
        Console.ReadLine();
    }

}
public class Dir
{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    public Dir Parent = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    public List<Dir> Dirs = new List<Dir>();
    public List<long> Files = new List<long>();
    public string name = "";
    public void Print()
    {
        Console.WriteLine("{0} {1}", name, string.Join(",", Files));
        Dirs.ForEach(x => Dirs.Print());
    }
    public long Size()
    {
        return Dirs.Sum(x => x.Size()) + Files.Sum();
    }
}
