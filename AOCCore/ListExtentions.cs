﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


public static class SL
{
    public static T MaxItem<T>(this IEnumerable<T> list, Func<T, double> f)
    {
        var item = list.First();
        var bestNumber = double.MinValue;
        foreach (var element in list)
        {
            var number = f(element);
            if (bestNumber < number)
            {
                bestNumber = number;
                item = element;
            }
        }
        return item;
    }

    public static List<List<R>> ZipGrid<T, P, R>(this List<List<T>> list, List<List<P>> list2, Func<T, P, R> f)
    {
        return list.Zip(list2, (row1, row2) => row1.Zip(row2, (cell1, cell2) => f(cell1, cell2)).ToList()).ToList();
    }
    public static List<List<T>> ReverseV<T>(this List<List<T>> list)
    {
        var newList = new List<List<T>>();

        for (int i = list.Count - 1; i >= 0; i--)
        {
            newList.Add(list[i]);
        }

        return newList;
    }

    public static List<List<T>> ReverseH<T>(this List<List<T>> list)
    {
        var newList = new List<List<T>>();

        foreach (var line in list)
        {
            var newline = new List<T>(line);
            line.Reverse();
            newList.Add(newline);
        }

        return newList;
    }
    public static List<R> Product<T1, T2, R>(this IEnumerable<T1> list1, IEnumerable<T2> list2, Func<T1, T2, R> f)
    {
        List<R> newList = new List<R>();
        foreach (var element1 in list1)
        {
            foreach (var element2 in list2)
            {
                newList.Add(f(element1, element2));
            }
        }
        return newList;
    }




    public static int Mod(int number, int max)
    {
        return (number % max + max) % max;
    }
    public static List<List<T>> DeepCopy<T>(this List<List<T>> oldCells)
    {
        return oldCells.Select(x => new List<T>(x)).ToList();

    }
    public static List<T[]> DeepCopy<T>(this List<T[]> oldCells)
    {
        return oldCells.Select(x =>
        {
            var newarray = new T[x.Length];
            Array.Copy(x, newarray, x.Length);
            return newarray;
        }).ToList();

    }

    public static void Log(string txt)
    {
        Console.WriteLine(txt);
    }
    public static void Log(object o)
    {
        Console.WriteLine(o);
    }
    public static List<List<T>> Transpose<T>(this List<List<T>> oldList)
    {
        List<List<T>> newlist = new List<List<T>>();

        for (int j = 0; j < oldList[0].Count; j++)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < oldList.Count; i++)
            {
                list.Add(oldList[i][j]);
            }
            newlist.Add(list);
        }

        return newlist;
    }

    public static List<List<I>> GridSelect<T, I>(this List<List<T>> oldList, Func<T, I> func)
    {
        return oldList.Select(x => x.Select(y => func(y)).ToList()).ToList();
    }

    public static int GridSum<T>(this List<List<T>> oldList, Func<T, int> func)
    {
        return oldList.Sum(x => x.Sum(y => func(y)));
    }
    public static List<int> GetNumbersInt(int from, int to)
    {
        List<int> list = new List<int>();
        for (int i = from; i < to; i++)
        {
            list.Add(i);
        }
        return list;
    }
    public static List<long> GetNumbers(long from, long to)
    {
        List<long> list = new List<long>();
        for (long i = from; i < to; i++)
        {
            list.Add(i);
        }
        return list;
    }

    public static List<string> List(this string line)
    {
        return line.ToCharArray().Select(x => x.ToString()).ToList();
    }



    public static List<List<T>> SubSelect<T>(this List<List<T>> list, List<(int, int)> neighbors, Func<T, T> f)
    {
        var newList = list.DeepCopy();
        foreach (var neighbor in neighbors)
        {
            newList[neighbor.Item1][neighbor.Item2] = f(list[neighbor.Item1][neighbor.Item2]);
        }
        return newList;
    }



    public static Value Get<T, Value>(this Dictionary<T, Value> dict, T key, Value baseValue) where T : notnull
    {
        if (!dict.ContainsKey(key)) dict[key] = baseValue;
        return dict[key];
    }


    public static LinkedListNode<T> CycleNext<T>(this LinkedListNode<T> currentNode)
    {
        if (currentNode.Next is null && currentNode.List is not null && currentNode.List.First is not null) return currentNode.List.First;
        if (currentNode.Next is not null) return currentNode.Next;
        throw new Exception("This never happends lol");
    }
    public static LinkedListNode<T>? CyclePrev<T>(this LinkedListNode<T> currentNode)
    {
        if (currentNode.Previous is null && currentNode.List is not null && currentNode.List.First is not null) return currentNode.List.First;
        if (currentNode.Previous is not null) return currentNode.Previous;
        throw new Exception("This never happends lol");
    }



    public static List<List<T>> ToLists<T>(this T[,] array)
    {
        List<List<T>> lists = new List<List<T>>();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            List<T> list = new List<T>();
            for (int j = 0; j < array.GetLength(1); j++)
            {
                list.Add(array[i, j]);
            }
            lists.Add(list);
        }
        return lists;

    }

    public static List<T> Flat<T>(this List<List<T>> lists)
    {
        var newList = new List<T>();
        foreach (var list in lists)
        {
            foreach (var element in list)
            {
                newList.Add(element);
            }
        }
        return newList;
    }

    public static (int, int) Sort(this (int, int) tuple) {
        var (x, y) = tuple;
        if (x > y) return (y, x);
        else return (x, y); 
    }
    public static string F<T>(this List<T> list, string seperator = "")
    {
        var newList = "";
        foreach (var a in list)
        {
            newList += a + seperator;
        }
        return newList;
    }

}
