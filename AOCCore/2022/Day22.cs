//using Priority_Queue;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;

namespace AOCCore._2022;

class Day22 : Day
{
    public Day22()
    {
        GetInput("2022", "22");

    }
    public class Squire
    {
        public (Squire, string) Left;
        public (Squire, string) Up;
        public (Squire, string) Down;
        public (Squire, string) Right;
        public string Content = "";
        public int x;
        public int y;

        public string Zone = "?";
        public Squire(string content, int x, int y)
        {
            Content = content;
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return (x, y).ToString();
        }
    }

    public override void Part1(List<string> Lines)
    {
        TryParse(Lines);
        //  var sm = StreetMagic.FindPattern(Lines);
        var pgrid = Blocks[0].Select(x => x.List()).ToList();
        int maxX = pgrid.Max(x => x.Count);
        int maxY = pgrid.Count();
        var grid = new Squire[maxX, maxY];

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var s = new Squire(" ", x, y);
                grid[x, y] = s;
            }
        }

        for (int y = 0; y < pgrid.Count(); y++)
        {
            for (int x = 0; x < pgrid[y].Count(); x++)
            {
                grid[x, y] = new Squire(pgrid[y][x], x, y);
                if (50 <= x && x < 100 && 0 <= y && y < 50)
                {
                    grid[x, y].Zone = "A";
                }
                if (100 <= x && x < 150 && 0 <= y && y < 50)
                {
                    grid[x, y].Zone = "B";
                }
                if (50 <= x && x < 100 && 50 <= y && y < 100)
                {
                    grid[x, y].Zone = "C";
                }
                if (50 <= x && x < 100 && 100 <= y && y < 150)
                {
                    grid[x, y].Zone = "D";
                }
                if (0 <= x && x < 50 && 100 <= y && y < 150)
                {
                    grid[x, y].Zone = "E";
                }
                if (0 <= x && x < 50 && 150 <= y && y < 200)
                {
                    grid[x, y].Zone = "F";
                }
            }
        }
        (int Ax, int Ay) = (50, 0);

        (int Bx, int By) = (100, 0);

        (int Cx, int Cy) = (50, 50);

        (int Dx, int Dy) = (50, 100);

        (int Ex, int Ey) = (0, 100);

        (int Fx, int Fy) = (0, 150);

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                //down
                var s = grid[x, y];
                if (s.Content == " ") continue;
                if (y + 1 < maxY && grid[x, y + 1].Content != " ")
                {
                    s.Down = (grid[x, y + 1], "Down");
                }
                else if (s.Zone == "B")
                {
                    s.Down = (grid[Cx + 49, Cy + (x % 50)], "Left");
                    if (s.Down.Item1.Zone != "C") { s.Down.Item1.Zone.P(); Console.ReadLine(); }

                }
                else if (s.Zone == "D")
                {
                    //D
                    s.Down = (grid[Fx + 49, Fy + (x % 50)], "Left");
                    if (s.Zone != "D") { "ZoneNotCorrect".P(); Console.ReadLine(); }
                    if (s.Down.Item1.Zone != "F") { s.Down.Item1.Zone.P(); Console.ReadLine(); }

                }
                else if (s.Zone == "F")
                {
                    //F
                    s.Down = (grid[Bx + (x % 50), 0], "Down");
                    if (s.Zone != "F") { "ZoneNotCorrect".P(); Console.ReadLine(); }
                    if (s.Down.Item1.Zone != "B") { s.Down.Item1.Zone.P(); Console.ReadLine(); }
                }
            }
        }


        for (int NOYPLS = 0; NOYPLS < maxY; NOYPLS++)
        {
            for (int x = 0; x < maxX; x++)
            {
                //up
                var s = grid[x, NOYPLS];
                if (s.Content == " ") continue;
                if (NOYPLS - 1 >= 0 && grid[x, NOYPLS - 1].Content != " ")
                {
                    s.Up = (grid[x, NOYPLS - 1], "Up");
                }
                else if (s.Zone == "A")
                {
                    s.Up = (grid[0, Fy + (x % 50)], "Right");
                    if (s.Up.Item1.Zone != "F") { s.Up.Item1.Zone.P(); Console.ReadLine(); }

                }
                else if (s.Zone == "B")
                {
                    s.Up = (grid[Fx + x % 50, Fy + 49], "Up");
                    if (s.Up.Item1.Zone != "F") { s.Up.Item1.Zone.P(); Console.ReadLine(); }

                }
                else if (s.Zone == "E")
                {
                    s.Up = (grid[Cx, Cy + x], "Right");
                    if (s.Up.Item1.Zone != "C") { s.Up.Item1.Zone.P(); Console.ReadLine(); }
                }
            }
        }
        for (int y = 0; y < maxY; y++)
        {
            for (int NOXPLS = 0; NOXPLS < maxX; NOXPLS++)
            {
                //Left
                var s = grid[NOXPLS, y];
                if (s.Content == " ") continue;
                if (NOXPLS - 1 >= 0 && grid[NOXPLS - 1, y].Content != " ")
                {
                    s.Left = (grid[NOXPLS - 1, y], "Left");
                }
                else if (s.Zone == "A")
                {

                    s.Left = (grid[0, Ey + (49 - (y % 50))], "Right");
                    if (s.Left.Item1.Zone != "E") { s.Left.Item1.Zone.P(); Console.ReadLine(); }
                }
                else if (s.Zone == "C")
                {

                    s.Left = (grid[0 + (y % 50), Ey], "Down");
                    if (s.Left.Item1.Zone != "E") { s.Left.Item1.Zone.P(); Console.ReadLine(); }
                }
                else if (s.Zone == "E")
                {

                    s.Left = (grid[Ax, Ay + (49 - (y % 50))], "Right");
                    if (s.Left.Item1.Zone != "A") { s.Left.Item1.Zone.P(); Console.ReadLine(); }
                }
                else if (s.Zone == "F")
                {

                    s.Left = (grid[Ax + (y % 50), 0], "Down");
                    if (s.Left.Item1.Zone != "A") { s.Left.Item1.Zone.P(); Console.ReadLine(); }
                }
            }
        }

        for (int y = 0; y < maxY; y++)
        {
            for (int NOXPLS = 0; NOXPLS < maxX; NOXPLS++)
            {
                //Right
                var s = grid[NOXPLS, y];
                if (s.Content == " ") continue;
                if (NOXPLS + 1 < maxX && grid[NOXPLS + 1, y].Content != " ")
                {
                    s.Right = (grid[NOXPLS + 1, y], "Right");
                }
                else if (s.Zone == "B")
                {
                    s.Right = (grid[Dx + 49, Dy + (49 - (y % 50))], "Left");
                    if (s.Right.Item1.Zone != "D") { s.Right.Item1.Zone.P(); Console.ReadLine(); }

                }
                else if (s.Zone == "C")
                {
                    s.Right = (grid[Bx + (y % 50), By + 49], "Up");
                    if (s.Right.Item1.Zone != "B") { s.Right.Item1.Zone.P(); Console.ReadLine(); }
                }
                else if (s.Zone == "D")
                {
                    s.Right = (grid[Bx + 49, By + (49 - (y % 50))], "Left");
                    if (s.Right.Item1.Zone != "B") { s.Right.Item1.Zone.P(); Console.ReadLine(); }

                }
                else if (s.Zone == "F")
                {
                    s.Right = (grid[Dx + (y % 50), Dy + 49], "Up");
                    if (s.Right.Item1.Zone != "D") { s.Right.Item1.Zone.P(); Console.ReadLine(); }
                }

            }
        }
        //  Console.ReadLine();
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var s = grid[x, y];
                if (s.Content == " ") continue;
                //   (s.x, s.y, s.Zone).P();
                if (s.Up.Item1.Zone == "?" || s.Down.Item1.Zone == "?" || s.Left.Item1.Zone == "?" || s.Right.Item1.Zone == "?") { "NOOOOOOOOOOOOOOOOO".P(); }
            }
        }


        var instructions = GetNumbers(Blocks[1].First());
        //  instructions.Print(" ");
        List<string> names = new List<string>() { "Right", "Down", "Left", "Up" };
        //List<(int, int)> dir = new List<(int, int)>() { (1, 0), (0, 1), (-1, 0), (0, -1) };
        // int dirIndex = 0;
        int CY = 0;
        int CX = 0;
        for (int i = 0; i < maxX; i++)
        {
            if (grid[i, CY].Content == ".") { CX = i; break; }
        }
        var currentLocation = (grid[CX, CY], "Right");

        {
            foreach (var direciotne in new List<string>() { "Up", "Down", "Right", "Left" })
                for (int x = 0; x < maxX; x++)
                {
                    for (int y = 0; y < maxY; y++)
                    {
                        if (grid[x, y].Zone == "?") continue;
                        currentLocation = (grid[x, y], direciotne);

                        var pos = currentLocation.Item1;
                        var dir = currentLocation.Item2;
                        for (int i = 0; i < 400; i++)
                        {
                            // Print(maxX, maxY, grid, currentLocation);
                            //  (currentLocation.Item1.x %50, currentLocation.Item1.y % 50).P();
                            var (position, currentDirection) = currentLocation;
                            var next = (new Squire("????", -1, -1), "faskdlsksa");
                            if (currentDirection == "Left")
                            {
                                next = position.Left;
                            }
                            if (currentDirection == "Right")
                            {
                                next = position.Right;
                            }
                            if (currentDirection == "Up")
                            {
                                next = position.Up;
                            }
                            if (currentDirection == "Down")
                            {
                                next = position.Down;
                            }
                            if (next.Item1.Content == "????") { throw new Exception(); }
                            // if (next.Item1.Content == ".")
                            {
                                currentLocation = next;
                            }
                        }
                        if (pos != currentLocation.Item1)
                        {
                            pos.P();

                        }
                        if (dir != currentLocation.Item2)
                        {
                            pos.P();

                        }
                    }
                }
        }


        foreach (var (steps, direction) in instructions)
        {
            // Print(maxX, maxY, grid, currentLocation.Item1.x, currentLocation.Item1.y);
            //currentLocation.P();
            //(steps, direction).P();

            //  Print(maxX, maxY, grid, currentLocation);
            //   currentLocation.P();
            // Console.ReadLine();
            for (int i = 0; i < steps; i++)
            {

                var (position, currentDirection) = currentLocation;
                var next = (new Squire("????", -1, -1), "faskdlsksa");
                if (currentDirection == "Left")
                {
                    next = position.Left;
                }
                if (currentDirection == "Right")
                {
                    next = position.Right;
                }
                if (currentDirection == "Up")
                {
                    next = position.Up;
                }
                if (currentDirection == "Down")
                {
                    next = position.Down;
                }
                if (next.Item1.Content == "????") { throw new Exception(); }
                if (next.Item1.Content == ".")
                {
                    currentLocation = next;
                }
                else { break; }
            }
            if (direction == "R")
            {
                int dirIndex = names.IndexOf(currentLocation.Item2);
                dirIndex = (dirIndex + 1) % 4;
                currentLocation = (currentLocation.Item1, names[dirIndex]);
            }
            if (direction == "L")
            {
                int dirIndex = names.IndexOf(currentLocation.Item2);
                dirIndex = (dirIndex + 3) % 4;
                currentLocation = (currentLocation.Item1, names[dirIndex]);
            }
            //names[dirIndex].P();
            //   Console.ReadLine();
        }
        {
            int dirIndex = names.IndexOf(currentLocation.Item2);
            currentLocation.P();
            (1000 * (currentLocation.Item1.y + 1) + 4 * (currentLocation.Item1.x + 1) + dirIndex).P();
            //((CY + 1), (CX + 1), dirIndex).P();
        }
        Console.ReadLine();
    }

    private static void Print(int maxX, int maxY, Squire[,] grid, (Squire, string) location)
    {
        location.P();
        var printGrind = new string[maxX, maxY];

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                if (grid[x, y].Content == "#")
                {
                    printGrind[x, y] = ".";
                }
                else if (grid[x, y].Content == " ")
                {
                    printGrind[x, y] = "$";
                }
                else
                    printGrind[x, y] = " ";
                //printGrind[x, y] = grid[x, y].Content == "." ? " " : grid[x, y].Content;
                //printGrind[x, y] = grid[x, y].Content == "#" ? "." : grid[x, y].Content;
                // grid[x, y].Content != " " ? grid[x, y].Left.Item1.Zone : "!";
                // printGrind[x, y] = grid[x, y].Content != " " ? grid[x, y].Down.Item2[0].ToString() : "!";
            }
        }
        printGrind[location.Item1.x, location.Item1.y] = "█";
        // printGrind.ToLists().GridSelect(x => x == " " ? "@" : x).Print("");
        printGrind.ToLists().Select(x => x.Take(160).ToList()).ToList().Print("");

        Console.ReadLine();
    }

    public List<(int, string)> GetNumbers(string line)
    {
        // Console.WriteLine(line);
        List<(int, string)> list = new List<(int, string)>();
        string number = "";
        for (int i = 0; i < line.Length; i++)
        {
            //Console.WriteLine(number);
            if ((number == "") && (line[i] == '-'))
            {
                number = "-";
            }
            else if (line[i] >= '0' && line[i] <= '9')
            {
                number += line[i];
            }
            else if (number.Length > 0)
            {
                _ = int.TryParse(number, out int parsedNumber);
                list.Add((parsedNumber, line[i].ToString()));
                number = "";
            }
            else
            {
                number = "";
            }
        }
        if (number.Length > 0 && number != "-")
        {

            _ = int.TryParse(number, out int parsedNumber);
            list.Add((parsedNumber, "Last"));

        }
        //Console.ReadLine();
        return list;
        //return ..Where(x => !string.IsNullOrEmpty(x)).Where(x => x.Length < 9).Select(int.Parse).ToList();
    }
}
