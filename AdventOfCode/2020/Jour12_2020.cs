using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Jour12_2020 : IJour
    {
        Program program;

        internal class Operation
        {
            public string code { get; set; }
            public int arg { get; set; }
            public Operation(string _code, int _arg)
            {
                code = _code;
                arg = _arg;
            }
        }

        internal class Program
        {
            public Program(Operation[] _code)
            {
                code = _code;
                Reset();
            }

            public void Reset()
            {
                x = 0;
                y = 0;
                wx = 10;
                wy = -1;
                direction = 0;
            }

            private int x;
            private int y;
            private int direction;
            private Operation[] code;

            private int wx;
            private int wy;

            public void Run()
            {
                foreach (var op in code)
                {
                    switch (op.code)
                    {
                        case "N":
                            y -= op.arg;
                            break;
                        case "S":
                            y += op.arg;
                            break;
                        case "E":
                            x += op.arg;
                            break;
                        case "W":
                            x -= op.arg;
                            break;
                        case "L":
                            direction -= op.arg;
                            direction = direction % 360;
                            if (direction<0)
                            {
                                direction += 360;
                            }
                            break;
                        case "R":
                            direction += op.arg;
                            direction = direction % 360;
                            break;
                        case "F":
                            switch (direction)
                            {
                                case 0:
                                    x += op.arg;
                                    break;
                                case 90:
                                    y += op.arg;
                                    break;
                                case 180:
                                    x -= op.arg;
                                    break;
                                case 270:
                                    y -= op.arg;
                                    break;
                            }
                            break;
                    }
                }
            }

            public void Run2()
            {
                int tmp;
                foreach (var op in code)
                {
                    switch (op.code)
                    {
                        case "N":
                            wy -= op.arg;
                            break;
                        case "S":
                            wy += op.arg;
                            break;
                        case "E":
                            wx += op.arg;
                            break;
                        case "W":
                            wx -= op.arg;
                            break;
                        case "L":
                            switch(op.arg)
                            {
                                case 90:
                                    tmp = wx;
                                    wx = wy;
                                    wy = -tmp;
                                    break;
                                case 180:
                                    wx = -wx;
                                    wy = -wy;
                                    break;
                                case 270:
                                    tmp = wx;
                                    wx = -wy;
                                    wy = tmp;
                                    break;
                                default:
                                    Console.WriteLine($"Error!!!");
                                    break;
                            }
                            break;
                        case "R":
                            switch (op.arg)
                            {
                                case 90:
                                    tmp = wx;
                                    wx = -wy;
                                    wy = tmp;
                                    break;
                                case 180:
                                    wx = -wx;
                                    wy = -wy;
                                    break;
                                case 270:
                                    tmp = wx;
                                    wx = wy;
                                    wy = -tmp;
                                    break;
                                default:
                                    Console.WriteLine($"Error!!!");
                                    break;
                            }
                            break;
                        case "F":
                            x += wx * op.arg;
                            y += wy * op.arg;
                            break;
                    }
                }
            }

            public int GetDistance()
            {
                return Math.Abs(x) + Math.Abs(y);
            }
        }

        public void Init(string inputfile)
        {
            var rx = new Regex(@"(N|S|E|W|L|R|F)([0-9]+)", RegexOptions.Compiled);
            string[] lines = File.ReadAllLines(inputfile);
            List<Operation> code = new List<Operation>();
            foreach (var line in lines)
            {
                var r = rx.Match(line);
                code.Add(new Operation(r.Groups[1].Value, int.Parse(r.Groups[2].Value)));
            }
            program = new Program(code.ToArray());
        }

        public void ResolveFirstPart()
        {
            program.Run();
            Console.WriteLine($"Resultat = {program.GetDistance()}");
        }

        public void ResolveSecondPart()
        {
            program.Reset();
            program.Run2();
            Console.WriteLine($"Resultat = {program.GetDistance()}");
        }
    }
}
