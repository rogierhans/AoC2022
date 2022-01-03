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
       var result =  RunAStar(firstState);
        return PrintSolution(result, "15322", "part 1");
    }
    public override string Part2(List<string> Lines)
    {
        var newLines = new List<string>();
        newLines.AddRange(Lines.Take(3));
        newLines.Add("  #D#C#B#A#");
        newLines.Add("  #D#B#A#C#");
        newLines.AddRange(Lines.Skip(3).Take(2));
        State firstState = new State(newLines, false);
        var result = RunAStar(firstState);
        return PrintSolution(result, "56324", "part 2");
    }
    private string RunAStar(State firstState) {
        Dictionary<ulong, State> keyValuePairs = new Dictionary<ulong, State>();
        var q = new SimplePriorityQueue<ulong, int>();
        {
            ulong key = firstState.ToKey();
            keyValuePairs.Add(key, firstState);
            q.Enqueue(key, firstState.GetCosts());
        }
        HashSet<ulong> done = new HashSet<ulong>();
        int counter = 0;
        int bestCost = int.MaxValue;

        while (q.Count > 0)
        {
            counter++;
            var currentKey = q.Dequeue();
            var state = keyValuePairs[currentKey];
            state.SetDone();

            if (state.IsDone())
            {
                Console.WriteLine(state);

                bestCost = Math.Min(state.GetCosts(),bestCost);
                //break;
            }
            foreach (var next in state.GetNextStates())
            {
                ulong key = next.ToKey();
                if (!done.Contains(key))
                {
                    if (keyValuePairs.ContainsKey(key))
                    {
                        var otherState = keyValuePairs[key];
                        if (otherState.GetCosts() > next.GetCosts())
                        {
                            keyValuePairs[key] = next;
                            q.UpdatePriority(key, next.GetCosts());
                        }
                    }
                    else
                    {
                        keyValuePairs[key] = next;
                        q.Enqueue(key, next.GetCosts());
                    }
                }
            }
          //  Console.WriteLine(currentKey);
            done.Add(currentKey);
        }
        return bestCost.ToString();
    }

    class State
    {



        private int Costs = new int();


        public int GetCosts()
        {
            int extraCost = 0;
            for (int col = 0; col < HallWay.Length; col++)
            {
             //  if (HallWay[col] != Empty) extraCost += ExtraCost(HallWay[col], Math.Abs(col - HallWay[col]));
            }

            return Costs + extraCost;
        }

        private static int ExtraCost(int letter, int length)
        {
            if (letter == 0) return 1 * length;
            if (letter == 1) return  10 * length;
            if (letter == 2) return  100 * length;
            if (letter == 3) return 1000 * length;
            return 0;
        }
        public ulong ToKey()
        {
            ulong number = 0;
            for (int col = 0; col < HallWay.Length; col++)
            {
                if(col != 2 || col != 4 || col != 6 || col != 8)
                number = number * 5 + (ulong) HallWay[col];
            }
            for (int row = 0; row < Home.GetLength(0); row++)
            {
                for (int col = 0; col < Home.GetLength(1); col++)
                {
                    number = number * 5 + (ulong)Home[row, col];

                }
            }
            return number;
        }
        int Empty = 5;
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
            else
            {
                Home = new int[4, 4];
                for (int i = 0; i < 4; i++)
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
            // Console.WriteLine(this);
            //  Console.ReadLine();
        }

        public void ForceState(List<string> lines)
        {

            var Grid = lines.Select(x => x.List()).ToList();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var col = 3 + j * 2;
                    var row = i + 2;
                    Home[i, j] = (Grid[row][col] == ".") ? Empty : Grid[row][col].ToCharArray().First() - 'A';
                }
            }
            for (int i = 0; i < 11; i++)
            {
                HallWay[i] = Grid[1][i + 1] == "." ? Empty : Grid[1][i + 1].ToCharArray().First() - 'A';
            }
            Console.WriteLine(this);
            SetDone();
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
            SetDone();
        }

        public State(State oldState)
        {
            Parent = oldState;
            Costs = oldState.Costs;

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
            SetDone();
            List<State> newStates = new List<State>();
            FromStack2HW(newStates);
            FromHW2Stack(newStates);
            return newStates;
        }
        private static void SetCosts(int letter, int length, State newState)
        {
            if (letter == 0) newState.Costs += 1 * length;
            if (letter == 1) newState.Costs += 10 * length;
            if (letter == 2) newState.Costs += 100 * length;
            if (letter == 3) newState.Costs += 1000 * length;
        }
        private void FromHW2Stack(List<State> newStates)
        {
            for (int col = 0; col < HallWay.Length; col++)
            {
                var letter = HallWay[col];
                if (letter == Empty) continue;
                var ((x, y), validSpot) = FindStackSpot(letter);
                //  Console.WriteLine(validSpot);
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
                    if (HallWay[c] != Empty || c == 2 || c == 4 || c == 6 || c == 8) continue;
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
                    p &= HallWay[col] == Empty;
                }
                return (p, toHallway - fromHallway);
            }
            else if (toHallway < fromHallway)
            {
                bool p = true;
                for (int col = toHallway; col < fromHallway; col++)
                {
                    p &= HallWay[col] == Empty;
                }
                return (p, fromHallway - toHallway);
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
                line += (HallWay[col] == Empty ? "." : ((char)(HallWay[col] + 'A')));
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


