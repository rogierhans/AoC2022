using Google.OrTools.LinearSolver;
using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AOCCore._2017;

class Day12 : Day
{
    public Day12()
    {
        GetInput("2017", "12");

    }

    public override void Part1(List<string> Lines)
    {
        var numberss = Lines.Select(x => x.GetInts());
        var DFS = new DFS<int>();
        HashSet<int> states = new HashSet<int>();

        foreach (var numbers in numberss)
        {
            foreach (var number in numbers)
            {
                states.Add(number);
            }
        }
        DFS.AddStateEct(states, numberss.Select(x => x.First()).ToList(), numberss.Select(x => x.Skip(1).ToList()).ToList());
        int counter = 0;
        while (true)
        {
            counter++;
            DFS.WalkDFS(DFS.GetUnvisited().First());
            if (DFS.GetUnvisited().Count() == 0)
            {
                Console.WriteLine(counter);
                break;
            }

        }
    }
}


public class DFS<T> where T : notnull
{
    public List<Node<T>> States = new();
    public Dictionary<Node<T>, List<Node<T>>> Neighbors = new();
    public Dictionary<T, Node<T>> Mapping = new Dictionary<T, Node<T>>();

    public List<Node<T>> GetVisited()
    {
        return States.Where(x => x.Visited).ToList();
    }

    public List<Node<T>> GetUnvisited()
    {
        return States.Where(x => !x.Visited).ToList();
    }
    public void AddStateEct(IEnumerable<T> states, List<T> from, List<List<T>> to)
    {
        var mapping = AddStates(states);
        AddNeighbors(from, to, mapping);
        Mapping = mapping;
    }

    private Dictionary<T, Node<T>> AddStates(IEnumerable<T> elements)
    {
        var mapping = new Dictionary<T, Node<T>>();
        foreach (var element in elements)
        {
            var node = new Node<T>(element);
            States.Add(node);
            mapping[element] = node;
        }
        return mapping;
    }

    private void AddNeighbors(List<T> from, List<List<T>> to, Dictionary<T, Node<T>> mapping)
    {
        for (int i = 0; i < from.Count; i++)
        {
            Neighbors.Add(mapping[from[i]], to[i].Select(x => mapping[x]).ToList());
        }
    }

    public List<Node<T>> WalkDFS(Node<T> start)
    {
        Stack<Node<T>> Stack = new Stack<Node<T>>();
        var vistedNodes = new List<Node<T>>(); 
        Stack.Push(start);
        while (Stack.Count > 0)
        {
            var currentElement = Stack.Pop();
            currentElement.Visited = true;
            foreach (var element in Neighbors[currentElement].Where(x => !x.Visited))
            {
                Stack.Push(element);
                vistedNodes.Add(element);
            }
        }
        return vistedNodes;
    }

}

public class Node<T> where T : notnull
{
    public bool Visited = false;
    public T Element;
    public Node(T element)
    {
        Element = element;
    }
}
