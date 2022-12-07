﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static Microsoft.FSharp.Core.ByRefKinds;

public static class CustomParser
{

    public static List<List<T>> Parse2D<T>(this List<string> lines, Func<string, T> func)
    {
        return lines.Select(line => line.ToCharArray().Select(x => func(x.ToString())).ToList()).ToList();
    }

    public static R1 Pattern<R1>(this string input, string pattern, Func<string, R1> f, string splitString = @"{}")
    {
        var (values, success) = GetValues(input, pattern, splitString, 1);
        if (!success) _ = new Exception("couldn't parse");
        return f(values[0]);
    }
    public static (R1, R2) Pattern<R1, R2>(this string input, string pattern, Func<string, R1> f1, Func<string, R2> f2, string splitString = @"{}")
    {
        var (values, success) = GetValues(input, pattern, splitString, 2);
        if (!success) _ = new Exception("couldn't parse");
        return (f1(values[0]), f2(values[1]));
    }

    public static (R1, R2, R3) Pattern<R1, R2, R3>(this string input, string pattern, Func<string, R1> f1, Func<string, R2> f2, Func<string, R3> f3, string splitString = @"{}")
    {
        var (values, success) = GetValues(input, pattern, splitString, 3);
        if (!success) _ = new Exception("couldn't parse");
        return (f1(values[0]), f2(values[1]), f3(values[2]));
    }
    public static (R1, R2, R3, R4) Pattern<R1, R2, R3, R4>(this string input, string pattern, Func<string, R1> f1, Func<string, R2> f2, Func<string, R3> f3, Func<string, R4> f4, string splitString = @"{}")
    {
        var (values, success) = GetValues(input, pattern, splitString, 4);
        if (!success) _ = new Exception("couldn't parse");
        return (f1(values[0]), f2(values[1]), f3(values[2]), f4(values[3]));
    }
    public static (R1?, bool) TryPattern<R1>(this string input, string pattern, Func<string, R1> f, string splitString = @"{}")
    {
        var (values, success) = GetValues(input, pattern, splitString, 1);
        if (!success) return (default(R1), success);
        return (f(values[0]), true);
    }
    public static (R1?, R2?, bool) TryPattern<R1, R2>(this string input, string pattern, Func<string, R1> f1, Func<string, R2> f2, string splitString = @"{}")
    {
        var (values, success) = GetValues(input, pattern, splitString, 2);
        if (!success) return (default(R1), default(R2), success);
        return (f1(values[0]), f2(values[1]), true);
    }
    public static (R1?, R2?, R3?, bool) TryPattern<R1, R2, R3>(this string input, string pattern, Func<string, R1> f1, Func<string, R2> f2, Func<string, R3> f3, string splitString = @"{}")
    {
        var (values, success) = GetValues(input, pattern, splitString, 3);
        if (!success) return (default(R1), default(R2), default(R3), success);
        return (f1(values[0]), f2(values[1]), f3(values[2]), true);
    }
    public static (R1?, R2?, R3?, R4?, bool) TryPattern<R1, R2, R3, R4>(this string input, string pattern, Func<string, R1> f1, Func<string, R2> f2, Func<string, R3> f3, Func<string, R4> f4, string splitString = @"{}")
    {
        var (values, success) = GetValues(input, pattern, splitString, 4);
        if (!success) return (default(R1), default(R2), default(R3), default(R4), success);
        return (f1(values[0]), f2(values[1]), f3(values[2]), f4(values[3]), true);
    }
    private static (List<string>, bool) GetValues(string input, string pattern, string splitString, int number)
    {
        static string GetValue(Match match, int i) { return match.Groups["value" + i].Value; };
        var regex2 = new Regex(CreateRegex(pattern, splitString, number), RegexOptions.Singleline);
        var m = regex2.Match(input);
        if (!m.Success) return (new List<string>(), false);
        List<string> Values = new List<string>();
        for (int i = 0; i < number; i++)
        {
            Values.Add(GetValue(m, i));
        }
        return (Values, true);
    }

    public static List<R> FliterNull<R>(IEnumerable<R?> list)
    {
        var newList = new List<R>();
        foreach (var item in list)
        {
            if (item is not null) newList.Add(item);
        }
        return newList;
    }

    public static List<R1> FindPatterns<R1>(this List<string> lines, string pattern, Func<string, R1> f1, string splitString = @"{}")
    {
        var list = new List<R1>();
        foreach (var line in lines)
        {
            var (element, success) = line.TryPattern(pattern, f1, splitString);
            if (success && element is not null) list.Add(element);
        }
        return list;
    }
    public static List<(R1, R2)> FindPatterns<R1, R2>(this List<string> lines, string pattern, Func<string, R1> f1, Func<string, R2> f2, string splitString = @"{}")
    {
        var list = new List<(R1, R2)>();
        foreach (var line in lines)
        {
            var (element1, element2, success) = line.TryPattern(pattern, f1, f2, splitString);
            if (success && element1 is not null && element2 is not null) list.Add((element1, element2));
        }
        return list;
    }
    public static List<(R1, R2, R3)> FindPatterns<R1, R2, R3>(this List<string> lines, string pattern, Func<string, R1> f1, Func<string, R2> f2, Func<string, R3> f3, string splitString = @"{}")
    {

        var list = new List<(R1, R2, R3)>();
        foreach (var line in lines)
        {
            var (element1, element2, element3, success) = line.TryPattern(pattern, f1, f2, f3, splitString);
            if (element1 is not null && element3 is not null && element2 is not null)
            {
                if (success)
                    list.Add((element1, element2, element3));
            }
        }
        return list;
    }
    public static List<(R1, R2, R3, R4)> FindPatterns<R1, R2, R3, R4>(this List<string> lines, string pattern, Func<string, R1> f1, Func<string, R2> f2, Func<string, R3> f3, Func<string, R4> f4, string splitString = @"{}")
    {

        var list = new List<(R1, R2, R3, R4)>();
        foreach (var line in lines)
        {
            var (element1, element2, element3, element4, success) = line.TryPattern(pattern, f1, f2, f3, f4, splitString);
            if (element1 is not null && element3 is not null && element2 is not null && element4 is not null)
            {
                if (success)
                    list.Add((element1, element2, element3, element4));
            }
        }
        return list;
    }
    //public static List<R1> FindPatterns<R1>(List<string> lines, string pattern, Func<string, R1> f1, string splitString = @"{}")
    //{
    //    return lines.Select(line => line.TryPattern(pattern, f1, splitString)).Where(x => x.Item2).Select(x => x.Item1).ToList();
    //}
    //public static List<R1> FindPatterns<R1>(List<string> lines, string pattern, Func<string, R1> f1, string splitString = @"{}")
    //{
    //    return lines.Select(line => line.TryPattern(pattern, f1, splitString)).Where(x => x.Item2).Select(x => x.Item1).ToList();
    //}
    private static string CreateRegex(string examplePattern, string splitString, int number)
    {
        var specials = @"[$&+,:;=?@#|'<>.-^*()%!]".List();
        foreach (var special in specials)
        {
            examplePattern = examplePattern.Replace(special, @"\" + special);
        }
        string anyChar = @"[\s\S]+";
        string pattern(int s) { return @"(?<value" + s.ToString() + @">" + anyChar + ")"; };
        string variable(int s) { return splitString.Trim(0, 1) + s.ToString() + splitString.Trim(1, 0); };
        string regexString = examplePattern;
        for (int i = 0; i < number; i++)
        {
            regexString = regexString.Replace(variable(i), pattern(i));
        }
        regexString = "^" + regexString + "$";
         Console.WriteLine(regexString);
        return regexString;
    }

    public static string FromType<T>()
    {
        Type itemType = typeof(T);
        if (itemType == typeof(int))
        {
            return "[0-9]*";
        }
        return @"[\s\S]+";
    }

    public static List<List<string>> ClusterLines(this List<string> lines)
    {
        List<List<string>> List = new List<List<string>>();

        var currentList = new List<string>();
        foreach (var line in lines)
        {

            if (line == "")
            {
                List.Add(currentList);
                currentList = new List<string>();
            }
            else
            {
                currentList.Add(line);
            }
        }
        List.Add(currentList);
        return List;
    }


    public static List<int> GetInts(this string line)
    {
       return Regex.Split(line, @"\D+").Select(int.Parse).ToList();
    }

    public static List<double> GetDoubles(this string line)
    {
        return Regex.Split(line, @"\D+").Select(double.Parse).ToList();
    }
    public static string Trim(this string line, int front, int back)
    {
        return line.Substring(front, line.Length - front - back);
    }


    public static (string, string) Split_Ex(string line, int index)
    {
        return (line[..index], line[(index + 1)..]);
    }

    public static string PatternMatch(string line, string front, string back)
    {

        for (int i = 0; i < line.Length; i++)
        {
            var subline = line.Substring(i, front.Length);
            if (front == subline)
            {
                for (int j = i + front.Length; j < line.Length; j++)
                {
                    var subline2 = line.Substring(j, back.Length);
                    if (subline2 == back)
                    {
                        return line[(i + front.Length)..j];
                    }
                }
            }
        }
        return "";

    }

    public static List<string> SplitString(this string line, string seperator)
    {
        return line.Replace(seperator, ";").Split(';').ToList();
    }

    public static (string, string, string) ParrenthesisParser(string line, string front, string middle, string end)
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (LineMatch(line, front, i))
            {
                Console.WriteLine(line[i..]);
                int counter = 0;
                for (int j = i + 1; j < line.Length; j++)
                {
                    if (LineMatch(line, front, j))
                    {
                        counter++;

                    }
                    if (LineMatch(line, end, j))
                    {
                        if (counter == 0)
                        {
                            return (line[(i + front.Length)..(j + end.Length)], "", "");
                        }
                        counter--;
                    }
                    Console.WriteLine(counter);
                }
                throw new Exception("");
            }
        }

        throw new Exception("");
    }

    public static bool LineMatch(string line, string pattern, int offset)
    {
        if (pattern.Length + offset > line.Length) return false;
        var chars1 = line.ToCharArray();
        var chars2 = pattern.ToCharArray();
        bool p = true;
        for (int i = 0; i < chars2.Length; i++)
        {
            p &= chars1[i + offset] == chars2[i];
        }
        return p;
    }
}
