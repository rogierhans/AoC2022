using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;



static class Printer
{
    public static string Flat(this List<string> s, string seperator = "")
    {
        return string.Join(seperator, s);
    }
    public static void Print<T>(this List<T> oldList, string seperator = "")
    {
        string line = string.Join(seperator, oldList);
        Console.WriteLine(line);
    }

    public static void Print<T>(this List<List<T>> list, string seperator = "")
    {
        //if(reverse)
        for (int i = list.Count-1; i >=0; i--)
        {
            string line = string.Join(seperator, list[i]);
            Console.WriteLine(line);
        }
        //for (int i = 0; i < list.Count; i++)
        //{
        //    string line = string.Join(seperator, list[i]);
        //    Console.WriteLine(line);
        //}
    }

    public static void PrintPlusPlus<T>(this List<List<T>> list, string seperator = "")
    {
        List<string> lines = new List<string>();
        foreach (var oldLine in list)
        {
            string line = string.Join(seperator, oldLine);
            lines.Add(line);
        }
        File.WriteAllLines(@"C:\Users\Rogier\Desktop\Print.txt", lines);
        //  Process myProcess = new Process();
        //ProcessStartInfo info= new ProcessStartInfo("notepad++.exe", @"C:\Users\Rogier\Desktop\Print.txt");
    }


    public static void P(this object i)
    {
        Console.WriteLine(i);
    }
}