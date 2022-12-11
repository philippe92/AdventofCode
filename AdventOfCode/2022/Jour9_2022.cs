using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AdventOfCode
{
    public class Jour9_2022 : IJour
    {
        private class Move
        {
            public enum WayType
            {
                Left,
                Right,
                Down,
                Up
            }
            public WayType Way { get; set; }
            public int Steps { get; set; }

            public void SetWay(string s)
            {
                switch (s)
                {
                    case "U":
                        Way = WayType.Up;
                        break;
                    case "D":
                        Way = WayType.Down;
                        break;
                    case "L":
                        Way = WayType.Left;
                        break;
                    case "R":
                        Way = WayType.Right;
                        break;
                    default:
                        break;
                }
            }

            public void UpdatePos(ref Pos p)
            {
                switch (Way)
                {
                    case WayType.Up:
                        p.Y--;
                        break;
                    case WayType.Down:
                        p.Y++;
                        break;
                    case WayType.Left:
                        p.X--;
                        break;
                    case WayType.Right:
                        p.X++;
                        break;
                }
            }
        }

        public struct Pos
        {
            //  ------->
            //  |     X
            //  |
            //  |Y
            public int X { get; set; }
            public int Y { get; set; }
        }

        List<Move> moves;
        Regex rx;

        public void Init(string inputfile)
        {
            rx = new Regex(@"([A-Z]) ([0-9]+)", RegexOptions.Compiled);
            string[] lines = File.ReadAllLines(inputfile);
            moves = new List<Move>();

            foreach (var l in lines)
            {
                var r = rx.Match(l);
                if (r.Success)
                {
                    var move = new Move { Steps = int.Parse(r.Groups[2].Value) };
                    move.SetWay(r.Groups[1].Value);
                    moves.Add(move);
                }
            }
        }

        private void UpdateTail(Pos head, ref Pos tail)
        {
            int dx = head.X - tail.X;
            int dy = head.Y - tail.Y;

            if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
            {
                // Head and Tail touch, do nothing
                return;
            }

            if (dx == 2)
            {
                tail.X++;
            }

            if (dx == -2)
            {
                tail.X--;
            }

            if (dy == 2)
            {
                tail.Y++;
            }

            if (dy == -2)
            {
                tail.Y--;
            }

            ////////////////////
            
            if (dx == 1)
            {
                tail.X++;
            }

            if (dx == -1)
            {
                tail.X--;
            }

            if (dy == 1)
            {
                tail.Y++;
            }

            if (dy == -1)
            {
                tail.Y--;
            }
        }

        public void ResolveFirstPart()
        {
            Pos tail = new Pos();
            Pos head = new Pos();
            HashSet<Pos> visited = new HashSet<Pos> { tail };
            foreach (var m in moves)
            {
                for (int i = 0; i < m.Steps; i++)
                {
                    m.UpdatePos(ref head);
                    UpdateTail(head, ref tail);
                    if (!visited.Contains(tail))
                    {
                        visited.Add(tail);
                    }
                }
            }

            Console.WriteLine($"Resultat = {visited.Count}");
        }

        public void ResolveSecondPart()
        {
            Pos[] rope = new Pos[10];
            HashSet<Pos> visited = new HashSet<Pos> { rope[9] };
            foreach (var m in moves)
            {
                for (int i = 0; i < m.Steps; i++)
                {
                    m.UpdatePos(ref rope[0]);
                    for (int j = 1; j < 10; j++)
                    {
                        UpdateTail(rope[j-1], ref rope[j]);
                    }
                    if (!visited.Contains(rope[9]))
                    {
                        visited.Add(rope[9]);
                    }
                }
            }

            Console.WriteLine($"Resultat = {visited.Count}");
        }
    }
}
