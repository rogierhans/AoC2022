//using Priority_Queue;
using Microsoft.FSharp.Core;
using ParsecSharp;
using ParsecSharp.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AOC2021.Day19Alt;
using static AOCCore._2022.Day13;
using static System.Reflection.Metadata.BlobBuilder;

namespace AOCCore._2022;

class Day16 : Day
{
    public Day16()
    {
        GetInput("2022", "16");

    }
    List<(string, int, List<int>)> RealLetters = new List<(string, int, List<int>)>();
    Dictionary<int, int> index2Oindex = new();
    Dictionary<int, int> Oindex2Index = new();
    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        int baseCount = "Valve GV has flow rate=23; tunnel leads to valve".Split(" ").Count();
        var fakeLetters = new List<(string, int, List<string>)>();
        for (int i = 0; i < Lines.Count; i++)
        {
            int flowRate = NumberedRows[i][0];
            string valve = Lines[i].Split(" ")[1];
            var toValves = Lines[i].Split(" ").Skip(baseCount).Select(x => x.Replace(",", "")).ToList();
            Console.WriteLine("{0} {1} {2}", valve, flowRate, string.Join(",", toValves));
            fakeLetters.Add((valve, flowRate, toValves));
        }
        fakeLetters = fakeLetters.OrderBy(x => x.Item1).ToList();
        var letters = fakeLetters.OrderBy(x => x.Item1).ToList();
        var letterDict = letters.ToDictionary(l => l.Item1, l => letters.IndexOf(l));
        RealLetters = fakeLetters.Select(x => (x.Item1, x.Item2, x.Item3.Select(y => letterDict[y]).ToList())).ToList();
        int Oindex = 0;
        index2Oindex = new Dictionary<int, int>();
        Oindex2Index = new Dictionary<int, int>();
        for (int index = 0; index < RealLetters.Count; index++)
        {
            if (RealLetters[index].Item2 > 0)
            {
                index2Oindex[index] = Oindex;
                Oindex2Index[Oindex] = index;
                Oindex++;
            }
        }
        for (int index = 0; index < RealLetters.Count; index++)
        {
            //     Console.WriteLine("{0} {1} {2} {3}", index,index2Oindex.ContainsKey(index)? index2Oindex[index] : "", index, index2Oindex.ContainsKey(index) ? RealLetters[Oindex2Index[ index2Oindex[index]]] : "", RealLetters[index]);
        }
        Mem = new Dictionary<ulong, int>();
        var currentPosistion = letters[0];
        var Opened = new int[index2Oindex.Count()];
        int minute = 1;
        Go(Opened, 0, 0, minute, true,-1,-1).P();
        Mem.MaxItem(x => x.Value).P();
        letters.Print(" ");
        Console.ReadLine();

    }
    int counter = 0;
    Dictionary<ulong, int> Mem = new();
    int max = 26;
    public int Go(int[] Opened, int position, int elepant, int minute, bool youTurn, int prev1, int preve2)
    {

        if (counter++ % 1000000 == 0)
        {
            counter.P();
            if (Mem.Count > 1)
                Mem.Values.Max().P();
        }
        //  Console.WriteLine(position +" " + minute);
        var key = Convert2Key(Opened, minute, position, elepant, youTurn);
        if (Mem.ContainsKey(key)) return Mem[key];
        // key.P();
        var flow = 0;

        if (minute == max)
        {
            Mem[key] = flow;
            return flow;
        }
        int maxflow = 0;
        if (youTurn)
        {
            //both Move
            foreach (var nextPosition in RealLetters[position].Item3)
            {
                if (RealLetters[position].Item3.Count() == 1 || nextPosition != prev1)
                {
                    maxflow = Math.Max(maxflow, (Go(Opened, nextPosition, elepant, minute, !youTurn, position, preve2)));
                }


            }

            if (index2Oindex.ContainsKey(position) && Opened[index2Oindex[position]] == 0)
            {
                var newOPen = Opened.Select(x => x).ToArray();
                newOPen[index2Oindex[position]] = 1;
                maxflow = Math.Max(maxflow, ((RealLetters[position].Item2 * (max-minute)) + Go(newOPen, position, elepant, minute, !youTurn, prev1, preve2)));
            }
        }
        else
        {
            // elemoves
            foreach (var nextPosition in RealLetters[elepant].Item3)
            {
                if (RealLetters[elepant].Item3.Count() == 1 || nextPosition != preve2)
                {
                    maxflow = Math.Max(maxflow, (Go(Opened, position, nextPosition, minute + 1, !youTurn, prev1, elepant)));
                }


            }

            // both open 

            if (index2Oindex.ContainsKey(elepant) && Opened[index2Oindex[elepant]] == 0)
            {
                var newOPen = Opened.Select(x => x).ToArray();
                newOPen[index2Oindex[elepant]] = 1;
                maxflow = Math.Max(maxflow, (RealLetters[elepant].Item2 * (max  - minute)) +Go(newOPen, position, elepant, minute + 1, !youTurn, prev1, preve2));
            }


        }
        var value = maxflow + (youTurn ? flow : 0);
        // key.P();
        Mem[key] = value;
        return Mem[key];
    }

    public int CalculateFlow(int[] Opened)
    {
        int sum = 0;
        for (int index = 0; index < Opened.Length; index++)
        {
            if (Opened[index] == 1) sum += RealLetters[Oindex2Index[index]].Item2;
        }
        return sum;
    }

    bool Readstate(long state)
    {
        return false;
    }

    //public static ulong Convert(int[] Opened, int minute)
    //{
    //    ulong acc = (ulong)minute;
    //    int i = 0;
    //    for (; i < Opened.Length; i++)
    //    {
    //        acc += (ulong)Opened[i] << (i + 6);
    //    }
    //    acc += (ulong)Opened[i] << (i + 5);
    //    return acc;
    //}
    //public static string Convert2Key(int[] Opened, int minute, int currentPosition, int elepant, bool youturn)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append(currentPosition + "_" + minute + "_" + elepant + "_" + youturn);

    //    for (int i = 0; i < Opened.Length; i++)
    //    {
    //        sb.Append(Opened[i]);
    //    }
    //    return sb.ToString();
    //}
    public ulong Convert2Key(int[] Opened, int minute, int currentPosition, int elepant, bool youturn)
    {
        int number = 8;
        ulong value = (ulong)currentPosition;
        value <<= number;
        value += (ulong)elepant;
        value <<= number;
        value += (ulong)minute;
        value <<= 32;
        for (int i = 0; i < Opened.Length; i++)
        {
            value += (ulong)Opened[i] << (i);
        }
        value <<= 1;
        value += youturn ? (ulong)1 : (ulong)0;
        return value;
    }

}
