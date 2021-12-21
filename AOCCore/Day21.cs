using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using ParsecSharp;
//using FishLibrary;


class Day21 : Day
{



    public Day21()
    {
        SL.printParse = false;
        GetInput(RootFolder + @"2021_21\");
    }

    private readonly (ulong, ulong)[,,,,] State = new (ulong, ulong)[10, 10, 21, 21, 2];
    public override void Main(List<string> Lines)
    {
        Lines.Print("\n");
        var pos1 = Lines.First().Pattern("Player 1 starting position: {0}", int.Parse);
        var pos2 = Lines[1].Pattern("Player 2 starting position: {0}", int.Parse);
        var x = CalcWhoWins(pos1 - 1, pos2 - 1, 1, 0, 0);
        Console.WriteLine(x.Item1);
        Console.WriteLine(x.Item2);
        Console.ReadLine();
    }

    public (ulong, ulong) CalcWhoWins(int pos1, int pos2, int turnPlayer1, int score1, int score2)
    {
        if (score1 >= 21) return (1, (ulong)0);
        if (score2 >= 21) return (0, (ulong)1);
        if (State[pos1, pos2, score1, score2, turnPlayer1].Item1 != 0 || State[pos1, pos2, score1, score2, turnPlayer1].Item2 != 0)
        {
            return State[pos1, pos2, score1, score2, turnPlayer1];
        }
        ulong wins1 = 0;
        ulong wins2 = 0;
        for (int d1 = 1; d1 <= 3; d1++) for (int d2 = 1; d2 <= 3; d2++) for (int d3 = 1; d3 <= 3; d3++)
                {
                    if (turnPlayer1 == 1)
                    {
                        var Qpos1 = (pos1 + d1 + d3 + d2) % 10;
                        var Qscore1 = score1 + Qpos1 + 1;
                        var (Qwins1, Qwins2) = CalcWhoWins(Qpos1, pos2, 1 - turnPlayer1, Qscore1, score2);
                        wins1 += Qwins1;
                        wins2 += Qwins2;
                    }
                    else
                    {
                        var Qpos2 = (pos2 + d1 + d3 + d2) % 10;
                        var Qscore2 = score2 + Qpos2 + 1;
                        var (Qwins1, Qwins2) = CalcWhoWins(pos1, Qpos2, 1 - turnPlayer1, score1, Qscore2);
                        wins1 += Qwins1;
                        wins2 += Qwins2;
                    }
                }
        State[pos1, pos2, score1, score2, turnPlayer1] = (wins1, wins2);
        return State[pos1, pos2, score1, score2, turnPlayer1];
    }

}


