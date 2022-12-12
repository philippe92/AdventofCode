using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Jour10_2022 : IJour
    {
        private enum OpType
        {
            Noop,
            Addx
        }

        private struct Op
        {
            public OpType Type { get; set; }
            public int Operand { get; set; }
        }

        Regex rx;
        List<Op> ops;
        List<int> signal;

        public void Init(string inputfile)
        {
            rx = new Regex(@"([a-z])+ ([-0-9]+)", RegexOptions.Compiled);
            string[] lines = File.ReadAllLines(inputfile);
            ops = new List<Op>();
            foreach (var l in lines)
            {
                Op o = new Op();
                if (l == "noop")
                {
                    o.Type = OpType.Noop;
                }
                else
                {
                    var r = rx.Match(l);
                    if (r.Success)
                    {
                        o.Type = OpType.Addx;
                        o.Operand = int.Parse(r.Groups[2].Value);
                    }
                }
                ops.Add(o);
            }

            signal = new List<int>();
            int current = 1;
            foreach (var o in ops)
            {
                if (o.Type == OpType.Noop)
                {
                    signal.Add(current);
                }
                if (o.Type == OpType.Addx)
                {
                    signal.Add(current);
                    signal.Add(current);
                    current += o.Operand;
                }
            }
        }

        private int GetStrength(List<int> signal, int i)
        {
            return i * signal[i-1];
        }

        public void ResolveFirstPart()
        {
            int result = GetStrength(signal, 20)
                         + GetStrength(signal, 60)
                         + GetStrength(signal, 100)
                         + GetStrength(signal, 140)
                         + GetStrength(signal, 180)
                         + GetStrength(signal, 220);

            Console.WriteLine($"Resultat = {result}");
        }

        public void ResolveSecondPart()
        {
            for (int i=0; i<6; i++)
            {
                StringBuilder line = new StringBuilder();
                for (int j=0;j<40;j++)
                {
                    if (Math.Abs(j - signal[i * 40 + j]) <= 1)
                    {
                        line.Append("█");
                    }
                    else
                    {
                        line.Append(" ");
                    }
                }
                Console.WriteLine($"{line}");
            }
        }
    }
}
