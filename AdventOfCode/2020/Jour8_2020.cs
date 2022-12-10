using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Jour8_2020 : IJour
    {
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
                executed = new bool[code.Length];
            }
            private int acc = 0;
            private int position = 0;
            private Operation[] code;
            private bool[] executed;

            public void RunNext()
            {
                var op = code[position];
                switch (op.code)
                {
                    case "nop":
                        position++;
                        break;
                    case "acc":
                        acc += op.arg;
                        position++;
                        break;
                    case "jmp":
                        position += op.arg;
                        break;
                }
            }

            public bool CanSwitch(int i)
            {
                return code[i].code != "acc";
            }

            public void Switch(int i)
            {
                if (!CanSwitch(i))
                {
                    return;
                }
                var switched_op = new Operation(code[i].code, code[i].arg);
                if (switched_op.code == "jmp")
                {
                    switched_op.code = "nop";
                }
                else
                {
                    switched_op.code = "jmp";
                }
                code[i] = switched_op;
            }

            public int GetLength()
            {
                return code.Length;
            }

            public int GetAcc()
            {
                return acc;
            }

            public bool RunUntilExecutedOrFinished()
            {
                while (!executed[position])
                {
                    executed[position] = true;
                    RunNext();
                    if (position>=code.Length)
                    {
                        return true;
                    }
                }
                return false;
            }

            public Program Clone()
            {
                return new Program((Operation[])code.Clone());
            }
        }

        Program program;

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            var rx = new Regex(@"(acc|jmp|nop) ([+-][0-9]+)", RegexOptions.Compiled);
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
            program.RunUntilExecutedOrFinished();
            Console.WriteLine($"Resultat = {program.GetAcc()}");
        }

        public void ResolveSecondPart()
        {
            for (int i=0;i<program.GetLength();i++)
            {
                if (program.CanSwitch(i))
                {
                    var clone = program.Clone();
                    clone.Switch(i);
                    if (clone.RunUntilExecutedOrFinished())
                    {
                        Console.WriteLine($"Resultat = {clone.GetAcc()}");
                    }
                }

            }
        }
    }
}
