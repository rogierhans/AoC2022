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
}
