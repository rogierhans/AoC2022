using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Priority_Queue;
class Day23Alt : Day
{
    public Day23Alt()
    {
        GetInput(RootFolder + @"2021_23\");
    }

    public override string Part1(List<string> Lines)
    {


        State firstState = new State(Lines, true);

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
            Console.WriteLine(state);
            // Console.ReadLine();
            if (state.IsDone())
            {
                Console.WriteLine(currentKey);
                return PrintSolution(state.Costs, "15322", "part 1");
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
            Console.WriteLine(currentKey);
            done.Add(currentKey);
            // if (done.Count % 1000 == 0) Console.WriteLine(done.Count);
        }
        Console.WriteLine("DonewithoutSolutionlel");
        Console.ReadLine();
        throw new Exception();
    }
    //  public override string Part2(List<string> Lines)
    // {
    //var newLines = new List<string>();
    //newLines.AddRange(Lines.Take(3));
    //newLines.Add("  #D#C#B#A#");
    //newLines.Add("  #D#B#A#C#");
    //newLines.AddRange(Lines.Skip(3).Take(2));
    //State firstState = new State(newLines);

    ////  State test = new State(Lines);
    ////  test.ForceState(@"C:\Users\Rogier\Dropbox\AOC2\AOC2\InputFiles\2021_23\state5.txt");
    ////// test.Print();
    ////  test.GetNextStates().Where(state => true).ToList().ForEach(state => state.Print());
    ////   Console.WriteLine(test.ToKey());
    ////  Console.ReadLine();
    //Dictionary<string, State> keyValuePairs = new Dictionary<string, State>();
    //var q = new SimplePriorityQueue<string, int>();
    //{
    //    string key = firstState.ToKey();
    //    keyValuePairs.Add(key, firstState);
    //    q.Enqueue(key, firstState.Costs);
    //}
    //HashSet<string> done = new HashSet<string>();

    //while (q.Count > 0)
    //{
    //    var currentKey = q.Dequeue();
    //    var state = keyValuePairs[currentKey];

    //    //  Console.Write(state.Costs + "\t");
    //    if (state.IsDone())
    //    {
    //        return PrintSolution(state.Costs, "56324", "part 2");

    //        // return;

    //    }
    //    foreach (var next in state.GetNextStates())
    //    {
    //        string key = next.ToKey();


    //        if (!done.Contains(key))
    //        {
    //            if (keyValuePairs.ContainsKey(key))
    //            {
    //                var otherState = keyValuePairs[key];
    //                if (otherState.Costs > next.Costs)
    //                {
    //                    keyValuePairs[key] = next;
    //                    q.UpdatePriority(key, next.Costs);
    //                }
    //            }
    //            else
    //            {
    //                keyValuePairs[key] = next;
    //                q.Enqueue(key, next.Costs);
    //            }
    //        }
    //    }
    //    done.Add(currentKey);
    //    //state.Print();
    //    //Console.ReadLine();
    //}
    //throw new Exception();
    //   }
    //. . % . % . % . % . .
    //0 1 2 3 4 5 6 7 8 9 10

    class State
    {



        public int Costs = new int();

        public int ACost = 0;
        public int BCost = 0;
        public int CCost = 0;
        public int DCost = 0;

        public string ToKey()
        {
            return String.Join("", HallWay) + String.Join("", Home.ToLists().Select(x => String.Join("", x)));
        }
        int Empty = 8;
        int Illegal = 5;
        int[] HallWay = new int[7 + 4];
        int[,] Home = new int[2, 4];
        public State(List<string> lines, bool part1)
        {
            var Grid = lines.Select(x => x.List()).ToList();
            if (part1)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var col = 3 + j * 2;
                        var row = i + 2;
                        Home[i, j] = Grid[row][col].ToCharArray().First() - 'A';
                    }
                }
            }
            for (int i = 0; i < 11; i++)
            {
                HallWay[i] = Empty;

            }
            List<int> illegalHW = new List<int> { 2, 4, 6, 8 };
            foreach (var hw in illegalHW)
            {
                HallWay[hw] = Illegal;
            }
            Console.WriteLine(this);
            //  Console.ReadLine();
        }
        public State? Parent;


        public State(bool lel)
        {
            Home = new int[2, 4];
            for (int row = 0; row < Home.GetLength(0); row++)
            {
                for (int col = 0; col < Home.GetLength(1); col++)
                {
                    Home[row, col] = Empty;
                }
            }
            for (int col = 0; col < HallWay.Length; col++)
            {
                HallWay[col] = Empty;
            }
            HallWay[3] = 0;
            HallWay[5] = 0;
            List<int> illegalHW = new List<int> { 2, 4, 6, 8 };
            foreach (var hw in illegalHW)
            {
                HallWay[hw] = Illegal;
            }
            SetDone();
        }

        public State(State oldState)
        {
            Parent = oldState;
            Costs = oldState.Costs;
            ACost = oldState.ACost;
            BCost = oldState.BCost;
            CCost = oldState.CCost;
            DCost = oldState.DCost;

            Home = new int[oldState.Home.GetLength(0), oldState.Home.GetLength(1)];
            for (int row = 0; row < Home.GetLength(0); row++)
            {
                for (int col = 0; col < Home.GetLength(1); col++)
                {
                    Home[row, col] = oldState.Home[row, col];
                }
            }
            for (int col = 0; col < HallWay.Length; col++)
            {
                HallWay[col] = oldState.HallWay[col];
            }
            SetDone();
        }

        bool[] halfDone = new bool[4];
        bool[] done = new bool[4];
        public void SetDone()
        {
            for (int col = 0; col < Home.GetLength(1); col++)
            {
                var p = true;
                var q = true;
                for (int row = 0; row < Home.GetLength(0); row++)
                {
                    p &= Home[row, col] == col;
                    q &= Home[row, col] == col || Home[row, col] == Empty;
                }
                halfDone[col] = q;
                done[col] = p;
            }

        }

        public bool IsDone()
        {
            bool p = true;
            for (int col = 0; col < Home.GetLength(1); col++)
            {
                p &= done[col];
            }
            return p;
        }
        public List<State> GetNextStates()
        {
            List<State> newStates = new List<State>();
            FromStack2HW(newStates);
            FromHW2Stack(newStates);
            return newStates;
        }

        //private void FromStack2Stack(List<State> newStates)
        //{
        //    foreach (var (letter, i, j) in FindStackLetters())
        //    {
        //        var ((x, y), validSpot) = FindStackSpot(letter);
        //        if (!validSpot || j == y) return;
        //        var (validPath, length) = IsPath((i, j), (x, y), Grid);
        //        if (!validPath) return;
        //        var newState = new State(this);
        //        SetCosts(letter, length, newState);
        //        newState.Grid[x][y] = letter;
        //        newState.Grid[i][j] = ".";
        //        newStates.Add(newState);
        //    }
        //}

        private static void SetCosts(int letter, int length, State newState)
        {
            //Console.WriteLine(letter);
            //Console.ReadLine();
            if (letter == 0)
            {
                newState.Costs += 1 * length;
                newState.ACost += 1 * length;
            }
            if (letter == 1)
            {
                newState.Costs += 10 * length;
                newState.BCost += 10 * length;
            }
            if (letter == 2)
            {
                newState.Costs += 100 * length;
                newState.CCost += 100 * length;
            }
            if (letter == 3)
            {
                newState.Costs += 1000 * length;
                newState.DCost += 1000 * length;
            }
        }

        public const string BLOCK = "\U00002588";
        private void FromHW2Stack(List<State> newStates)
        {
            for (int col = 0; col < HallWay.Length; col++)
            {
                var letter = HallWay[col];
                if (letter == Empty || letter == Illegal) continue;
                var ((x, y), validSpot) = FindStackSpot(letter);
                if (validSpot)
                {
                    var (validPath, length) = PathFromHWToStack(col, (x, y));
                    if (validPath)
                    {
                        var newState = new State(this);
                        SetCosts(letter, length, newState);
                        newState.Home[x, y] = letter;
                        newState.HallWay[col] = Empty;
                        newStates.Add(newState);
                    }
                }
            }
        }
        private ((int, int), bool) FindStackSpot(int index)
        {
            bool p = true;
            for (int row = 0; row < Home.GetLength(0); row++)
            {
                p &= Home[row, index] == index || Home[row, index] == Empty;
            }
            if (!p) return ((-1, -1), false);
            for (int row = Home.GetLength(0) - 1; row >= 0; row--)
            {
                if (Home[row, index] == Empty) return ((row, index), true);
            }
            throw new Exception("");
        }

        private void FromStack2HW(List<State> newStates)
        {
            foreach (var (letter, x, y) in FindStackLetters())
            {
                for (int c = 0; c < HallWay.Length; c++)
                {
                    if (HallWay[c] != Empty || HallWay[c] == Illegal) continue;
                    var (valid, length) = PathFromStackToHW(c, (x, y));
                    if (valid)
                    {
                        var newState = new State(this);
                        SetCosts(letter, length, newState);
                        newState.HallWay[c] = letter;
                        newState.Home[x, y] = Empty;
                        newStates.Add(newState);
                    }
                }
            }
        }


        public List<(int, int, int)> FindStackLetters()
        {
            var lists = new List<(int, int, int)>();
            for (int col = 0; col < 4; col++)
            {
                if (!halfDone[col])
                    FindLetterInColumn(lists, col);
            }
            return lists;
        }
        private (bool, int) PathFromStackToHW(int hallway, (int, int) start)
        {

            var (row, col) = start;
            var from = 2 + col * 2;
            //Console.WriteLine("stack" + start);
            //Console.WriteLine("hallway" + hallway);
            //Console.WriteLine("stackHW" + from);
            var (suc, length1) = H2H(from, hallway);
            if (!suc) return (false, int.MaxValue);
            return (suc, length1 + 1 + row);
        }
        private (bool, int) PathFromHWToStack(int hallway, (int, int) start)
        {
            var (row, col) = start;
            var toHallway = 2 + col * 2;
            var (suc, length1) = H2H(hallway, toHallway);
            if (!suc || !halfDone[col]) return (false, int.MaxValue);
            return (suc, length1 + 1 + row);
        }
        private (bool, int) H2H(int fromHallway, int toHallway)
        {
            if (fromHallway < toHallway)
            {
                bool p = true;
                for (int col = fromHallway + 1; col <= toHallway; col++)
                {
                    p &= HallWay[col] == Empty || HallWay[col] == Illegal;
                }
                return (p, toHallway - fromHallway);
            }
            else if (toHallway < fromHallway)
            {
                bool p = true;
                for (int col = toHallway; col < fromHallway; col++)
                {
                    p &= HallWay[col] == Empty || HallWay[col] == Illegal;
                }
                return (p, fromHallway -toHallway );
            }
            else
            {
                throw new Exception();
            }
        }



        private void FindLetterInColumn(List<(int, int, int)> lists, int col
            )
        {
            for (int row = 0; row < Home.GetLength(0); row++)
            {
                if (Home[row, col] != Empty)
                {
                    lists.Add((Home[row, col], row, col));
                    break;
                }
            }
        }

        public override string ToString()
        {
            string line = "";
            for (int col = 0; col < HallWay.Length; col++)
            {
                line += (HallWay[col] == Empty || HallWay[col] == Illegal ? "." : ((char)(HallWay[col] + 'A')));
            }
            line += "\n" + "##";
            for (int row = 0; row < Home.GetLength(0); row++)
            {
                for (int col = 0; col < Home.GetLength(1); col++)
                {
                    line += (Home[row, col] == Empty ? "." : ((char)(Home[row, col] + 'A'))) + "#";
                }
                line += "\n" + "##";
            }
            line += String.Join("", halfDone) + "\n";
            line += String.Join("", done) + "\n";
            line += Costs + "\n";
            return line;
        }
    }
}


