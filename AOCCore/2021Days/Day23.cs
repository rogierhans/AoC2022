using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Priority_Queue;
class Day23 : Day
{
    public Day23()
    {
        GetInput(RootFolder + @"2021_23\");
    }

    public override void Main(List<string> Lines)
    {
        State firstState = new State(Lines);

        //  State test = new State(Lines);
        //  test.ForceState(@"C:\Users\Rogier\Dropbox\AOC2\AOC2\InputFiles\2021_23\state5.txt");
        //// test.Print();
        //  test.GetNextStates().Where(state => true).ToList().ForEach(state => state.Print());
        //   Console.WriteLine(test.ToKey());
        //  Console.ReadLine();
        Dictionary<string, State> keyValuePairs = new Dictionary<string, State>();
        var q = new SimplePriorityQueue<string, int>();
        {
            string key = firstState.ToKey();
            keyValuePairs.Add(key, firstState);
            q.Enqueue(key, firstState.Costs);
        }
        HashSet<string> done = new HashSet<string>();

        while (q.Count > 0)
        {
            var currentKey = q.Dequeue();
            var state = keyValuePairs[currentKey];

            //  Console.Write(state.Costs + "\t");
            if (state.IsDone())
            {
                state.PrintHeritage();
                Console.ReadLine();
                return;

            }
            foreach (var next in state.GetNextStates())
            {
                string key = next.ToKey();


                if (!done.Contains(key))
                {
                    if (keyValuePairs.ContainsKey(key))
                    {
                        var otherState = keyValuePairs[key];
                        if (otherState.Costs > next.Costs)
                        {
                            keyValuePairs[key] = next;
                            q.UpdatePriority(key, next.Costs);
                        }
                    }
                    else
                    {
                        keyValuePairs[key] = next;
                        q.Enqueue(key, next.Costs);
                    }
                }
            }
            //Console.WriteLine(currentKey)
            done.Add(currentKey);
            //state.Print();
            //Console.ReadLine();
        }
        Console.ReadLine();
    }
    class State
    {
        public int Costs = new int();

        public int ACost = 0;
        public int BCost = 0;
        public int CCost = 0;
        public int DCost = 0;
        List<List<string>> Grid = new List<List<string>>();


        List<string> letters = new List<string>() { "A", "B", "C", "D" };

        List<(int, int)> CoordsHallway = new List<(int, int)>
        {
            (1,1),(1,2),(1,4),(1,6),(1,8),(1,10),(1,11)
        };
        int height = 0;
        int width = 0;

        public string ToKey()
        {
            return Grid.Skip(1).Take(5).Select(x => string.Join("", x)).ToList().Flat();
        }

        //public int MinimalDistance()
        //{
        //    for (int i = 0; i < height; i++)
        //    {
        //        for (int j = 0; j < width; j++)
        //        {

        //        }
        //    }
        //}

        public State(List<string> lines)
        {
            Grid = lines.Select(x => x.List()).ToList();
            height = Grid.Count;
            width = Grid[0].Count;
            //ForceState(@"C:\Users\Rogier\Dropbox\AOC2\AOC2\InputFiles\2021_23\state2.txt");
            //  Print();
            // Console.ReadLine();


            // GetNextStates().ForEach(x => x.Print());
            //  PathTest();

        }
        public State Parent;
        public State(State oldState)
        {
            Parent = oldState;
            Grid = oldState.Grid.DeepCopy();
            Costs = oldState.Costs;
            ACost = oldState.ACost;
            BCost = oldState.BCost;
            CCost = oldState.CCost;
            DCost = oldState.DCost;
            height = Grid.Count;
            width = Grid[0].Count;
        }


        public bool IsDone()
        {
            bool q = ColumnCorrect("A") && ColumnCorrect("B") && ColumnCorrect("C") && ColumnCorrect("D");
            if (!q) return q;
            bool p = true;
            foreach (var (x, y) in CoordsHallway)
            {
                p &= Grid[x][y] == ".";
            }

            return p;
        }

        private bool ColumnCorrect(string letter)
        {
            int index = Letter2Index(letter);
            bool p = true;
            for (int i = 0; i < height; i++)
            {
                p &= Grid[i][index] == letter || Grid[i][index] == "." || Grid[i][index] == "#";
            }
            return p;
        }


        public void ForceState(string filename)
        {
            Grid = File.ReadAllLines(filename).Select(x => x.List()).ToList();
        }



        public void PrintHeritage()
        {
            if (Parent is not null)
            {
                Parent.PrintHeritage();
            }
            Print();
        }

        public List<State> GetNextStates()
        {
            List<State> newStates = new List<State>();
            FromStack2HW(newStates);
            FromHW2Stack(newStates);
            foreach (var (letter, i, j) in FindStackLetters())
            {
                int targetColum = Letter2Index(letter);
                var ((x, y), validSpot) = FindStackSpot(letter);
                //  Console.WriteLine( letter +" " + j + " "+ y);
                if (validSpot && j != y)
                {

                    var (validPath, length) = IsPath((i, j), (x, y), Grid);
                    // Console.WriteLine(validPath + " " + letter);
                    if (validPath)
                    {
                        var newState = new State(this);
                        SetCosts(letter, length, newState);
                        newState.Grid[x][y] = letter;
                        newState.Grid[i][j] = ".";
                        //Console.WriteLine("#######################");
                        //Print();
                        //newState.Print();
                        newStates.Add(newState);
                    }
                }
            }


            //newStates.ForEach(

            //    x =>
            //    {

            //    });
            return newStates;
        }

        private static void SetCosts(string letter, int length, State newState)
        {
            //Console.WriteLine(letter);
            //Console.ReadLine();
            if (letter == "A")
            {
                newState.Costs += 1 * length;
                newState.ACost += 1 * length;
            }
            if (letter == "B")
            {
                newState.Costs += 10 * length;
                newState.BCost += 10 * length;
            }
            if (letter == "C")
            {
                newState.Costs += 100 * length;
                newState.CCost += 100 * length;
            }
            if (letter == "D")
            {
                newState.Costs += 1000 * length;
                newState.DCost += 1000 * length;
            }
        }

        public const string BLOCK = "\U00002588";
        private void FromHW2Stack(List<State> newStates)
        {
            foreach (var (i, j) in CoordsHallway.Where(x => Grid[x.Item1][x.Item2] != "."))
            {
                var letter = Grid[i][j];
                int targetColum = Letter2Index(letter);
                var ((x, y), validSpot) = FindStackSpot(letter);
                //  Console.WriteLine(validSpot+" "+ letter);
                if (validSpot)
                {
                    var (validPath, length) = IsPath((i, j), (x, y), Grid);
                    // Console.WriteLine(validPath + " " + letter);
                    if (validPath)
                    {
                        var newState = new State(this);
                        SetCosts(letter, length, newState);
                        newState.Grid[x][y] = letter;
                        newState.Grid[i][j] = ".";
                        newStates.Add(newState);
                    }
                }
            }
        }

        public int Letter2Index(string letter)
        {
            if (letter == "A") return 3;
            else if (letter == "B") return 5;
            else if (letter == "C") return 7;
            else if (letter == "D") return 9;
            else throw new Exception("daskklfajnw");
        }

        private ((int, int), bool) FindStackSpot(string letter)
        {
            int index = Letter2Index(letter);
            bool p = true;
            for (int i = 0; i < height; i++)
            {
                //  Console.WriteLine("{0} {1} {2} {3} {4}",letter, Grid[i][index] == letter , Grid[i][index] == "." , Grid[i][index] == "#" , Grid[i][index] == BLOCK);;
                p &= Grid[i][index] == letter || Grid[i][index] == "." || Grid[i][index] == "#" || Grid[i][index] == BLOCK;
            }
            if (!p) return ((-1, -1), false);
            for (int i = height - 1; i >= 0; i--)
            {
                //  Console.WriteLine(letter +" " + Grid[i][index]);
                if (Grid[i][index] == ".") return ((i, index), true);
            }
            // Console.WriteLine(letter + " " + p + " " + index);
            throw new Exception("");
        }

        private void FromStack2HW(List<State> newStates)
        {
            foreach (var (letter, x, y) in FindStackLetters())
            {
                foreach (var (i, j) in CoordsHallway)
                {
                    var (valid, length) = IsPath((x, y), (i, j), Grid);
                    if (Grid[i][j] == "." && valid)
                    {
                        var newState = new State(this);
                        SetCosts(letter, length, newState);
                        newState.Grid[i][j] = letter;
                        newState.Grid[x][y] = ".";
                        newStates.Add(newState);
                    }
                }
            }
        }

        private void PathTest()
        {
            Print();
            Grid[1][5] = "G";
            foreach (var (letter, x, y) in FindStackLetters())
            {

                var copy = Grid.DeepCopy();
                foreach (var (i, j) in CoordsHallway)
                {
                    var (valid, length) = IsPath((x, y), (i, j), Grid);
                    copy[i][j] = valid ? length.ToString() : "N";

                }
                copy.Print();
                Console.ReadLine();
            }
            FindStackLetters().Print("");
        }

        public List<(string, int, int)> FindStackLetters()
        {
            var lists = new List<(string, int, int)>();
            FindLetterInColumn(lists, 3);
            FindLetterInColumn(lists, 5);
            FindLetterInColumn(lists, 7);
            FindLetterInColumn(lists, 9);

            return lists;
        }

        private (bool, int) IsPath((int, int) start, (int, int) end, List<List<string>> grid)
        {

            Queue<((int, int), int)> q = new();
            var visited = grid.GridSelect(x => false);
            q.Enqueue((start, 0));
            if (start.Item2 < end.Item2)
                while (q.Count > 0)
                {
                    var ((i, j), length) = q.Dequeue();
                    foreach (var offset in new List<(int, int)>() { (-1, 0), (1, 0), (0, 1) })
                    {
                        int x = i + offset.Item1;
                        int y = j + offset.Item2;
                        bool outOfRow = x < 0 || x >= grid.Count;
                        bool outOfColumn = y < 0 || y >= grid[0].Count;
                        if (!outOfColumn && !outOfRow && !visited[x][y] && grid[x][y] == ".")
                        {
                            q.Enqueue(((x, y), length + 1));
                            visited[x][y] = true;

                            if (x == end.Item1 && y == end.Item2)
                                return (true, length + 1);
                        }
                    }
                }
            else
                while (q.Count > 0)
                {
                    var ((i, j), length) = q.Dequeue();
                    foreach (var offset in new List<(int, int)>() { (-1, 0), (1, 0), (0, -1) })
                    {
                        int x = i + offset.Item1;
                        int y = j + offset.Item2;
                        bool outOfRow = x < 0 || x >= grid.Count;
                        bool outOfColumn = y < 0 || y >= grid[0].Count;
                        if (!outOfColumn && !outOfRow && !visited[x][y] && grid[x][y] == ".")
                        {
                            q.Enqueue(((x, y), length + 1));
                            visited[x][y] = true;

                            if (x == end.Item1 && y == end.Item2)
                                return (true, length + 1);
                        }
                    }
                }
            return (visited[end.Item1][end.Item2], int.MaxValue);
        }

        private void FindLetterInColumn(List<(string, int, int)> lists, int j)
        {
            for (int i = 0; i < height; i++)
            {
                if (letters.Contains(Grid[i][j]))
                {
                    lists.Add((Grid[i][j], i, j));
                    break;
                }
            }
        }

        public State Copy()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            Console.WriteLine("{0}  A{1} B{2} C{3} D{4}", Costs, ACost, BCost, CCost, DCost);
            Grid.Print();

        }
    }
}


