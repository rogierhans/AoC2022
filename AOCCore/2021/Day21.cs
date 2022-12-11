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
        GetInput(RootFolder + @"2021_21\");
    }

    public override void Part1(List<string> Lines)
    {
        //Lines.Print("\n");
        var pos1 = Lines.First().Pattern("Player 1 starting position: {0}", int.Parse);
        var pos2 = Lines[1].Pattern("Player 2 starting position: {0}", int.Parse);
        long score1 = 0;
        long score2 = 0;
        int die = 0;

        int player1Space = pos1 - 1;
        int player2Space = pos2 - 1;
        bool turnPlayer1 = true;
        int turn =0;

        while (score1 < 1000 && score2 < 1000) {
            turn++;
            var number = 0;
            for (int i = 0; i < 3; i++)
            {
                number += (die++ % 100) + 1;

            }
            if (turnPlayer1)
            {
                player1Space = (player1Space + number) % 10;
                score1 += player1Space + 1;
            }
            else {
                player2Space = (player2Space + number) % 10;
                score2 += player2Space + 1;
            }
            turnPlayer1 = !turnPlayer1;
        }

      PrintSolution(turn * 3 * score1, "989352", "part 1");
    }


    private readonly (ulong, ulong)[,,,,] StateMem = new (ulong, ulong)[10, 10, 21, 21, 2];
    public override void Part2(List<string> Lines)
    {
        //Lines.Print("\n");
        var pos1 = Lines.First().Pattern("Player 1 starting position: {0}", int.Parse);
        var pos2 = Lines[1].Pattern("Player 2 starting position: {0}", int.Parse);

        var (p1win,p2wins) = CalcWhoWins(pos1 - 1, pos2 - 1, 1, 0, 0);
      PrintSolution(p1win>p2wins?p1win:p2wins, "430229563871565", "part 2");
    }
    public (ulong, ulong) CalcWhoWins(int pos1, int pos2, int turnPlayer1, int score1, int score2)
    {
        if (score1 >= 21) return (1, 0);
        if (score2 >= 21) return (0, 1);

        (ulong wins1, ulong wins2) memState = StateMem[pos1, pos2, score1, score2, turnPlayer1];
        if (memState.wins1 != 0 || memState.wins2 != 0)
        {
            return memState;
        }
        ulong wins1Total = 0;
        ulong wins2Total = 0;

        foreach (var (sum, count) in new List<(int, ulong)>() { (3, 1), (4, 3), (5, 6), (6, 7), (7, 6), (8, 3), (9, 1) })

            if (turnPlayer1 == 1)
            {
                var Qpos1 = (pos1 + sum) % 10;
                var Qscore1 = score1 + Qpos1 + 1;
                var (Qwins1, Qwins2) = CalcWhoWins(Qpos1, pos2, 1 - turnPlayer1, Qscore1, score2);
                wins1Total += Qwins1 * count;
                wins2Total += Qwins2 * count;
            }
            else
            {
                var Qpos2 = (pos2 + sum) % 10;
                var Qscore2 = score2 + Qpos2 + 1;
                var (Qwins1, Qwins2) = CalcWhoWins(pos1, Qpos2, 1 - turnPlayer1, score1, Qscore2);
                wins1Total += Qwins1 * count;
                wins2Total += Qwins2 * count;
            }

        StateMem[pos1, pos2, score1, score2, turnPlayer1] = (wins1Total, wins2Total);
        return StateMem[pos1, pos2, score1, score2, turnPlayer1];
    }

}


