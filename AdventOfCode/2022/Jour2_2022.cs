using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Jour2_2022 : IJour
    {
        List<(Game, int)> data;

        private enum Game
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            data = new List<(Game, int)>();
            foreach(var s in lines)
            {
                (Game, int) item = (0, 0);
                if (s[0]=='A')
                {
                    item.Item1 = Game.Rock;
                }
                if (s[0] == 'B')
                {
                    item.Item1 = Game.Paper;
                }
                if (s[0] == 'C')
                {
                    item.Item1 = Game.Scissors;
                }

                if (s[2] == 'X')
                {
                    item.Item2 = 1;
                }
                if (s[2] == 'Y')
                {
                    item.Item2 = 2;
                }
                if (s[2] == 'Z')
                {
                    item.Item2 = 3;
                }
                data.Add(item);
            }
        }

        public void ResolveFirstPart()
        {
            int score = 0;
            foreach(var d in data)
            {
                score += d.Item2;
                var mychoice = (Game)d.Item2;
                if (d.Item1 == mychoice)
                {
                    score += 3;
                }
                else if (mychoice == Game.Rock && d.Item1 == Game.Scissors
                    || mychoice == Game.Scissors && d.Item1 == Game.Paper 
                    || mychoice == Game.Paper && d.Item1 == Game.Rock)
                {
                    score += 6;
                }
            }
            Console.WriteLine($"Score = {score}");
        }

        private enum GameResult
        {
            Lose = 1,
            Draw = 2,
            Win  = 3
        }

        public void ResolveSecondPart()
        {
            int score = 0;
            foreach (var d in data)
            {
                var result = (GameResult)d.Item2;
                if (result == GameResult.Win)
                {
                    score += 6;
                    if (d.Item1 == Game.Rock)
                    {
                        // I play Paper to defeat
                        score += (int)Game.Paper;
                    }
                    if (d.Item1 == Game.Paper)
                    {
                        // I play Scissors to defeat
                        score += (int)Game.Scissors;
                    }
                    if (d.Item1 == Game.Scissors)
                    {
                        // I play Rock to defeat
                        score += (int)Game.Rock;
                    }
                }
                if (result == GameResult.Draw)
                {
                    score += 3;
                    score += (int)d.Item1;
                }
                if (result == GameResult.Lose)
                {
                    if (d.Item1 == Game.Rock)
                    {
                        // I play scissors to lose
                        score += (int)Game.Scissors;
                    }
                    if (d.Item1 == Game.Paper)
                    {
                        // I play Rock to lose
                        score += (int)Game.Rock;
                    }
                    if (d.Item1 == Game.Scissors)
                    {
                        // I play Paper to lose
                        score += (int)Game.Paper;
                    }
                }
            }
            Console.WriteLine($"Score = {score}");
        }
    }
}
