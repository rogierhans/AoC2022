using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

class Day
{
    public string RootFolder = @"C:\Users\Rogier\Dropbox\AOC2\AOC2\InputFiles\";
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


    public virtual void Main(List<string> inputLines)
    {
        throw new NotImplementedException();
    }


    public string Part1(bool withTest)
    {
        if (withTest) Part1(testLines);
        return Part1(inputLines);
    }
    public string Part2(bool withTest)
    {
        if (withTest) Part2(testLines);
        return Part2(inputLines);
    }

    public virtual string Part1(List<string> inputLines)
    {
        return String.Format("Day {0} {1} not implemented yet", Folder, "part 1");
    }
    public virtual string Part2(List<string> inputLines)
    {
        return String.Format("Day {0} {1} not implemented yet", Folder, "part 2");
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
