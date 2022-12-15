using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

class Day
{
    public string RootFolder = @"C:\Users\Rogier\Desktop\InputFiles\";
    private List<string> testLines = new List<string>();
    public List<string> inputLines = new List<string>();

    private string Folder = "";
    public void GetInput(string folder)
    {
        Folder = folder.Split('\\')[folder.Split('\\').Count() - 2].Split('_').Last();
        string name = "input.txt";
        string filename = folder + name;
        string filenameTest = folder + "test.txt";
        testLines = File.ReadAllLines(filenameTest).ToList();
        inputLines = File.ReadAllLines(filename).ToList();
    }
    public void GetInput(string year, string day)
    {
        string filename = RootFolder + year + "_" + day + @"\";
        testLines = File.ReadAllLines(filename + "test.txt").ToList();
        inputLines = File.ReadAllLines(filename + "input.txt").ToList();
    }




    public virtual void Main(List<string> inputLines)
    {
        throw new NotImplementedException();
    }


    public void Part1(bool withTest)
    {
        if (withTest) Part1(testLines);
        Part1(inputLines);
        "sadasd".P();
        return;
    }
    public void Part2(bool withTest)
    {
        if (withTest) Part2(testLines);
        Part2(inputLines);
        return;
    }

    public virtual void Part1(List<string> inputLines)
    {
        Console.WriteLine(String.Format("Day {0} {1} not implemented yet", Folder, "part 1"));
    }
    public virtual void Part2(List<string> inputLines)
    {
        Console.WriteLine(String.Format("Day {0} {1} not implemented yet", Folder, "part 2"));
    }


    public string PrintSolution(object answer, string expectedAnswer, string additionalInfo)
    {

        if (answer.ToString() == expectedAnswer)
        {
            return String.Format("Day {0} {1}:    {2} == {3}", Folder, additionalInfo, answer, expectedAnswer);
        }
        else
        {
            return String.Format("Failed {0}:    {1} != {2}", additionalInfo, answer, expectedAnswer);
        }
    }
    protected string FirstLine = "";
    protected List<int> Numbers = new List<int>();

    protected List<string> Rows = new List<string>();
    protected List<List<int>> NumberedRows = new List<List<int>>();
    protected List<List<string>> Blocks = new List<List<string>>();
    protected List<List<List<int>>> NumberedBlocks = new List<List<List<int>>>();
    protected List<List<string>> Grid = new List<List<string>>();

    public void TryParse(List<string> lines)
    {
        FirstLine = lines.Count > 0 ? lines.First() : "";
        Numbers = new List<int>();
        Rows = lines;
        Blocks = new List<List<string>>();
        NumberedBlocks = new List<List<List<int>>>();
        NumberedRows = lines.Select(line => GetNumbers(line)).ToList();
        Grid = new List<List<string>>();
        Console.WriteLine();
        List<string> subset = new List<string>();
        foreach (var line in lines)
        {
            if (line == "")
            {
                Blocks.Add(subset);
                subset = new List<string>();
            }
            else
            subset.Add(line);
        }
        Blocks.Add(subset);
        subset = new List<string>();


        try
        {
            NumberedBlocks = Blocks.Select(x => x.Select(x => GetNumbers(x)).ToList()).ToList();
            Numbers = NumberedBlocks.Select(x => x.Flat()).ToList().Flat();
        }
        catch { }

        if (Blocks.Count == 1)
        {
            Grid = Blocks[0].Select(x => x.List()).ToList();
        }
        if (Blocks.Count == 2)
        {
            Grid = Blocks[1].Select(x => x.List()).ToList();
        }
        //grid

        Console.WriteLine("Rows________:\t{0}", Rows.Count);
        Console.WriteLine("Numbers_____:\t{0}", Numbers.Count);
        Console.WriteLine("Blocks______:\t{0}", Blocks.Count);
        Console.WriteLine("Grid________:\t{0}x{1}", Grid.Count, Grid.Count > 0 ? Grid[0].Count : "0");
        //Console.WriteLine("NBlocks_____:\t{0} {1}", NumberedBlocks.Count, Caps(NumberedBlocks.Count > 1));
        Numbers.Take(10).ToList().Print(" ");
        Console.WriteLine("#############################");
        Console.WriteLine();

      //  var SM = new StreetMagic(lines);

    }

    public List<int> GetNumbers(string line)
    {
        // Console.WriteLine(line);
        List<int> list = new List<int>();
        string number = "";
        for (int i = 0; i < line.Length; i++)
        {
            //Console.WriteLine(number);
            if ((number == "") && (line[i] == '-'))
            {
                number = "-";
            }
            else if (line[i] >= '0' && line[i] <= '9')
            {
                number += line[i];
            }
            else if (number.Length > 0 && number != "-")
            {
                _ = int.TryParse(number, out int parsedNumber);
                list.Add(parsedNumber);
                number = "";
            }
            else
            {
                number = "";
            }
        }
        if (number.Length > 0 && number != "-")
        {

            _ = int.TryParse(number, out int parsedNumber);
            list.Add(parsedNumber);

        }
        //Console.ReadLine();
        return list;
        //return ..Where(x => !string.IsNullOrEmpty(x)).Where(x => x.Length < 9).Select(int.Parse).ToList();
    }

}
